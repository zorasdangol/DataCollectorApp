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
    public class StockSyncPageVM:BaseViewModel
    {
        private List<StockTake> _DataList;
        public List<StockTake> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        private List<LoadDataCollect> _UploadDataList;
        public List<LoadDataCollect> UploadDataList
        {
            get { return _UploadDataList; }
            set
            {
                _UploadDataList = value;
                OnPropertyChanged("UploadDataList");
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

        List<StockTake> StockTakeList { get; set; }

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

        public StockSyncPageVM()
        {
            try
            {
                var list = LoadFromDB.LoadStockTakeList(App.DatabaseLocation);

                var query = (from m in list group m by new { m.sid, m.ind, m.division, m.wareHouse, m.trnDate, m.IsSaved } into mygroup select mygroup.FirstOrDefault()).Distinct();
                DataList = query.ToList().Select(m => new StockTake { sid = m.sid, ind = m.ind, division = m.division, wareHouse = m.wareHouse, trnDate = m.trnDate, IsSaved = m.IsSaved }).ToList();
                
                DataList = DataList.Where(x => x.IsSaved == false).ToList();
                IsAll = false;
                CancelCommand = new Command(ExecuteCancelCommand);
                SyncCommand = new Command(ExecuteSyncCommand);
                ActivityIndicatorEnabled = false;
                StockTakeList = new List<StockTake>();
                UploadDataList = new List<LoadDataCollect>();
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
                DependencyService.Get<IMessage>().LongAlert("Uploading Data Please wait for a while...");
                ActivityIndicatorEnabled = true;
                GenerateList();
                if (UploadDataList.Count == 0)
                {
                    ActivityIndicatorEnabled = false;
                    return;
                }
               
                FunctionResponse<string> functionResponse = await UploadStockTakeData.PostStockTakeList(UploadDataList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != "no")
                    {
                        foreach (var item in StockTakeList)
                        {
                            item.IsSaved = true;
                            item.IsUpload = false;
                            item.vchrNo = functionResponse.result;
                            ClearFromDB.UpdateSavedStockTake(App.DatabaseLocation, item);
                        }
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Synced to Server Successfully");
                            App.Current.MainPage = new MasterPage();
                        }
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Couldnot sync to Server");
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
                StockTakeList = new List<StockTake>();
                UploadDataList = new List<LoadDataCollect>();
                foreach (var item in DataList)
                {
                    if (item.IsUpload)
                    {
                        StockTakeList = Helpers.Data.StockTakeList.Where(x=> ( x.sid == item.sid) && (x.division == item.division)).ToList();
                        foreach(var data in StockTakeList)
                        {
                            LoadDataCollect UploadData = new LoadDataCollect();
                            UploadData.SetLoadDataCollect(data);
                            UploadDataList.Add(UploadData);
                        }
                    }
                }
            }
            catch (Exception e) { }
        }
    }
}
