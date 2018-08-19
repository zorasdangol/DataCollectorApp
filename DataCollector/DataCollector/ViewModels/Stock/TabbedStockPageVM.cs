using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollector.Views.Stock;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.Stock
{
    public class TabbedStockPageVM : BaseViewModel
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

        private List<StockTake> _StockTakeList;
        public List<StockTake> StockTakeList
        {
            get { return _StockTakeList; }
            set
            {
                _StockTakeList = value;
                OnPropertyChanged("StockTakeList");
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

        public TabbedStockPageVM()
        {
            GrnMenuList = Helpers.Data.GrnMenuList;
        }

        public async void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.AutoModeEnabled = false;
                (App.Current.MainPage as MasterDetailPage).Detail = (new TabbedStockPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                Helpers.Data.AutoModeEnabled = true;
                (App.Current.MainPage as MasterDetailPage).Detail = (new TabbedStockPage());
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

                StockTakeList = new List<StockTake>();
                UploadDataList = new List<LoadDataCollect>();

                StockTakeList = Helpers.Data.StockTakeList.Where(x => (x.sid == Helpers.Data.StockTake.sid) && (x.division == Helpers.Data.StockTake.division)).ToList();
                 
                foreach(var item in StockTakeList)
                {
                    item.IsUpload = true;
                    item.IsSaved = false;

                    LoadDataCollect UploadData = new LoadDataCollect();
                    UploadData.SetLoadDataCollect(item);
                    UploadDataList.Add(UploadData);
                }

                //foreach (var item in StockTakeList)
                //{
                //    if (item.IsUpload)
                //    {
                //            LoadDataCollect UploadData = new LoadDataCollect();
                //            UploadData.SetLoadDataCollect(item);
                //            UploadDataList.Add(UploadData);                        
                //    }
                //}

                FunctionResponse<string> functionResponse = await UploadStockTakeData.PostStockTakeList(UploadDataList);
                if (functionResponse.status == "ok")
                {
                    if (functionResponse.result != "no")
                    {
                        foreach(var item in StockTakeList)
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
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }
    }
}
