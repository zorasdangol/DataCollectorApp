using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollector.Views.BranchIn;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchIn
{
    public class BranchInTabbedPageVM : BaseViewModel
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

        public BranchInDetail BranchInDetail { get; set; }
        public List<BranchInMaster> BranchInMasterList { get; set; }


        public BranchInTabbedPageVM()
        {
            GrnMenuList = Helpers.Data.GrnMenuList;
            BranchInDetail = Helpers.Data.BranchInDetail;

        }

        public async void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.AutoModeEnabled = false;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchInTabbedPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                Helpers.Data.AutoModeEnabled = true;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchInTabbedPage());
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
                BranchInDetail = Helpers.Data.BranchInDetail;
                BranchInMasterList = new List<BranchInMaster>();

                BranchInMaster BranchInMaster = new BranchInMaster();
                BranchInMaster.BranchInMain = BranchInDetail;
                BranchInMasterList.Add(BranchInMaster);

                //Loading Prod for each Master
                foreach (var item in BranchInMasterList)
                {
                    item.BranchInProdList = Helpers.Data.BranchInItemList;
                }

                #endregion

                FunctionResponse<List<BranchInMaster>> functionResponse = await UploadBranchInData.PostBranchInMasterList(BranchInMasterList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != null)
                    {
                        bool sync = false;
                        string message = "" ;
                        var dataList = functionResponse.result;
                        foreach (var item in dataList)
                        {
                            if (item.BranchInMain.IsSaved == true)
                            {
                                sync = true;
                                ClearFromDB.UpdateSavedBranchInMain(App.DatabaseLocation, item.BranchInMain);
                            }
                            else
                            {
                                message = message + item.BranchInMain.remarks;
                            }
                        }
                        if (sync)
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Synced to Server Successfully");
                            App.Current.MainPage = new MasterPage();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot Sync to Server.." + message);
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
    