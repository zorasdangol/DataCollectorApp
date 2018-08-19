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
    public class BranchInSyncPageVM:BaseViewModel
    {
        private List<BranchInDetail> _DataList;
        public List<BranchInDetail> DataList
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

        List<BranchInMaster> MasterList { get; set; }

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

        public BranchInSyncPageVM()
        {
            try
            {
                DataList = LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
                DataList = DataList.Where(x => x.IsSaved == false).ToList();
                IsAll = false;
                CancelCommand = new Command(ExecuteCancelCommand);
                SyncCommand = new Command(ExecuteSyncCommand);
                ActivityIndicatorEnabled = false;
                MasterList = new List<BranchInMaster>();
            }
            catch (Exception e)
            { DependencyService.Get<IMessage>().ShortAlert(e.Message); }
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
                if (MasterList.Count == 0)
                {
                    ActivityIndicatorEnabled = false;
                    return;
                }
                FunctionResponse<List<BranchInMaster>> functionResponse = await UploadBranchInData.PostBranchInMasterList(MasterList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != null)
                    {
                        bool sync = false;
                        string message = "";
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
                            DependencyService.Get<IMessage>().ShortAlert("Synced Successfully");
                            DataList = LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
                            DataList = DataList.Where(x => x.IsSaved == false).ToList();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot Sync to Server.." + message);
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
                MasterList = new List<BranchInMaster>();
                foreach (var item in DataList)
                {
                    if (item.IsUpload)
                    {
                        BranchInMaster Master = new BranchInMaster();
                        Master.BranchInMain = item;
                        MasterList.Add(Master);
                    }
                }
                //Loading GrnProd for each Master
                foreach (var item in MasterList)
                {
                    item.BranchInProdList = LoadFromDB.LoadBranchInItemList(App.DatabaseLocation, item.BranchInMain);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }
    }
}
