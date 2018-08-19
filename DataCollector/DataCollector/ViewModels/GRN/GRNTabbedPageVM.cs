using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollector.Views.GRN;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.GRN
{
    public class GRNTabbedPageVM : BaseViewModel
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
        
        public GrnMain GrnMain  { get; set; }
        public List<GrnMaster> GrnMasterList { get; set; }
        
        public GRNTabbedPageVM()
        {
            GrnMain = new GrnMain();
            GrnMenuList = Helpers.Data.GrnMenuList;
        }

        public async void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.AutoModeEnabled = false;
                (App.Current.MainPage as MasterDetailPage).Detail = (new GRNTabbedPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                Helpers.Data.AutoModeEnabled = true;
                (App.Current.MainPage as MasterDetailPage).Detail = (new GRNTabbedPage());
            }
            else if (SelectedMenuItem.Index == 3)
            {
                var result = await App.Current.MainPage.DisplayAlert("Confirm","Are your sure to upload?","Yes","No");
                if(result)
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
                #region GrnMasterList Generation
                GrnMain = Helpers.Data.GrnMain;
                GrnMasterList = new List<GrnMaster>();
               
                GrnMaster grnMaster = new GrnMaster();
                grnMaster.GrnMain = GrnMain;
                GrnMasterList.Add(grnMaster);
                            
                //Loading GrnProd for each Master
                foreach (var item in GrnMasterList)
                {
                    item.GrnProdList = Helpers.Data.GrnEntryList; 
                }

#endregion 

                FunctionResponse<List<GrnMaster>> functionResponse = await UploadGrnData.PostGrnMasterList(GrnMasterList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != null)
                    {
                        bool sync = false;
                        var dataList = functionResponse.result;
                        foreach (var item in dataList)
                        {
                            if (item.GrnMain.IsSaved == true)
                            {
                                sync = true;
                                ClearFromDB.UpdateSavedGrnMain(App.DatabaseLocation, item.GrnMain);
                                //ClearFromDB.DeleteGrnData(App.DatabaseLocation, item );
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
            catch (Exception e) { }
        }
    }
}
