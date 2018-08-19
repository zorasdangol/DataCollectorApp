using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollector.Views.BranchOut;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchOut
{
    public class BranchOutTabbedPageVM:BaseViewModel
    {
        private List<MasterMenu> _GrnMenuList;
        public List<MasterMenu> GrnMenuList
        {
            get { return _GrnMenuList; }
            set
            {
                _GrnMenuList = value;
                OnPropertyChanged("GrnMenuList");
            }
        }

        private MasterMenu _SelectedMenuItem;
        
        public MasterMenu SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set
            {
                if (SelectedMenuItem != value && value != null && value.Index > 0)
                {
                    _SelectedMenuItem = value;
                    NavigateToPage();
                    OnPropertyChanged("SelectedMenuItem");
                }
                else
                {
                    _SelectedMenuItem = null;
                }
            }
        }

        public BranchOutDetail BranchOutDetail { get; set; }
        public List<BranchOutMaster> BranchOutMasterList { get; set; }

        public BranchOutTabbedPageVM()
        {
            GrnMenuList = Helpers.Data.GrnMenuList;
            BranchOutDetail = Helpers.Data.BranchOutDetail;
        }

        public async void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.AutoModeEnabled = false;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchOutTabbedPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                Helpers.Data.AutoModeEnabled = true;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchOutTabbedPage());
            }
            else if (SelectedMenuItem.Index == 3)
            {
                var result = await App.Current.MainPage.DisplayAlert("Confirm", "Are your sure to upload?", "Yes", "No");
                if (result)
                    GenerateList();
            }
            else if (SelectedMenuItem.Index == 4)
            {
                var result = await App.Current.MainPage.DisplayAlert("Confirm", "Are your to go to Main HomePage?", "Yes", "No");
                if (result)
                    App.Current.MainPage = new MasterPage();
            }

        }
        public async void GenerateList()
        {
            try
            {
                DependencyService.Get<IMessage>().LongAlert("Uploading Data Please wait for a while...");
                
                #region BranchOutList Generation
                BranchOutDetail = Helpers.Data.BranchOutDetail;
                BranchOutMasterList = new List<BranchOutMaster>();

                BranchOutMaster BranchOutMaster = new BranchOutMaster();
                BranchOutMaster.BranchOutMain = BranchOutDetail;
                BranchOutMasterList.Add(BranchOutMaster);

                //Loading GrnProd for each Master
                foreach (var item in BranchOutMasterList)
                {
                    item.BranchOutProdList = Helpers.Data.BranchOutItemList;
                }

                #endregion

                FunctionResponse<List<BranchOutMaster>> functionResponse = await UploadBranchOutData.PostBranchOutMasterList(BranchOutMasterList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != null)
                    {
                        bool sync = false;
                        var dataList = functionResponse.result;
                        foreach (var item in dataList)
                        {
                            if (item.BranchOutMain.IsSaved == true)
                            {
                                sync = true;
                                ClearFromDB.UpdateSavedBranchOutMain(App.DatabaseLocation, item.BranchOutMain);                                
                            }
                        }
                        if (sync)
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Synced to Server Successfully");
                            App.Current.MainPage = new MasterPage();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot Sync to Server");
                    }
                }
                else if (functionResponse.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(functionResponse.Message);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }
    }
}
