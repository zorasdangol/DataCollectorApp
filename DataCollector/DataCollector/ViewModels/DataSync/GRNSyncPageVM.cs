using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DataCollector.ViewModels.DataSync
{
    public class GRNSyncPageVM:BaseViewModel
    {
        private List<GrnMain> _DataList;
        public List<GrnMain> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        private bool _IsAll;
        public bool IsAll
        {
            get { return _IsAll; }
            set
            {
                _IsAll = value;
                CheckAllTrue(value);
                OnPropertyChanged("IsAll");
            }
        }

        public Command CancelCommand { get; set; }
        public Command SyncCommand { get; set; }
        
        List<GrnMaster> GrnMasterList { get; set; }

        private bool _ActivityIndicatorEnabled;
        public bool ActivityIndicatorEnabled
        {
            get { return _ActivityIndicatorEnabled; }
            set
            {
                _ActivityIndicatorEnabled = value;
                OnPropertyChanged("ActivityIndicatorEnabled");
            }
        }

        public GRNSyncPageVM()
        {
            try
            {
                DataList = LoadFromDB.LoadGrnMainList(App.DatabaseLocation);
                DataList = DataList.Where(x => x.IsSaved == false).ToList();
                IsAll = false;
                CancelCommand = new Command(ExecuteCancelCommand);
                SyncCommand = new Command(ExecuteSyncCommand);
                ActivityIndicatorEnabled = false;
                GrnMasterList = new List<GrnMaster>();
            }
            catch (Exception e) { }
        }

        public void CheckAllTrue(bool value)
        {
            if (value)
            {
                DataList.ForEach(x => x.IsUpload = true);
            }
            else
            {
                DataList.ForEach(x => x.IsUpload = false);
            }
        }

        public void ExecuteCancelCommand()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
        }

        public async void ExecuteSyncCommand()
        {
            try
            {   
                ActivityIndicatorEnabled = true;
                GenerateList();
                if(GrnMasterList.Count == 0)
                {
                    ActivityIndicatorEnabled = false;
                    return;
                }
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
                            DependencyService.Get<IMessage>().ShortAlert("Synced Successfully");
                            DataList = LoadFromDB.LoadGrnMainList(App.DatabaseLocation);
                            DataList = DataList.Where(x => x.IsSaved == false).ToList();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot Sync to Server");
                    }
                }
                else if (functionResponse.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(functionResponse.Message);
                }
                ActivityIndicatorEnabled = false;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
                ActivityIndicatorEnabled = false;
            }
        }

        public void GenerateList()
        {
            try
            {
                GrnMasterList = new List<GrnMaster>();
                foreach (var item in DataList)
                {
                    if (item.IsUpload)
                    {
                        GrnMaster grnMaster = new GrnMaster();
                        grnMaster.GrnMain = item;
                        GrnMasterList.Add(grnMaster);
                    }
                }
                //Loading GrnProd for each Master
                foreach (var item in GrnMasterList)
                {
                    item.GrnProdList = LoadFromDB.LoadGrnEntryList(App.DatabaseLocation, item.GrnMain);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }
    }
}
