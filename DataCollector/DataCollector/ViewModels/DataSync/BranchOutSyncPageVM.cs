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
    public class BranchOutSyncPageVM:BaseViewModel
    {
        private List<BranchOutDetail> _DataList;
        public List<BranchOutDetail> DataList
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

        List<BranchOutMaster> MasterList { get; set; }

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

        public BranchOutSyncPageVM()
        {
            try
            {
                DataList = LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);
                DataList = DataList.Where(x => x.IsSaved == false).ToList();
                IsAll = false;
                CancelCommand = new Command(ExecuteCancelCommand);
                SyncCommand = new Command(ExecuteSyncCommand);
                ActivityIndicatorEnabled = false;
                MasterList = new List<BranchOutMaster>();
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
                FunctionResponse<List<BranchOutMaster>> functionResponse = await UploadBranchOutData.PostBranchOutMasterList(MasterList);
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
                            DependencyService.Get<IMessage>().ShortAlert("Synced Successfully");
                            DataList = LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);
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
                MasterList = new List<BranchOutMaster>();
                foreach (var item in DataList)
                {
                    if (item.IsUpload)
                    {
                        BranchOutMaster Master = new BranchOutMaster();
                        Master.BranchOutMain = item;
                        MasterList.Add(Master);
                    }
                }
                //Loading GrnProd for each Master
                foreach (var item in MasterList)
                {
                    item.BranchOutProdList = LoadFromDB.LoadBranchOutItemList(App.DatabaseLocation, item.BranchOutMain);
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }

    }
}
