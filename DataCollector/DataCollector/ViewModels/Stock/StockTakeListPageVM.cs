using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.Stock
{
    public class StockTakeListPageVM:BaseViewModel
    {
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

        private StockTake _SelectedStockTake;
        public StockTake SelectedStockTake
        {
            get { return _SelectedStockTake; }
            set
            {
                try
                {
                    _SelectedStockTake = value;
                    if (value != null && !string.IsNullOrEmpty(value.MCODE))
                    {
                         DataDeletion(value);
                        //_SelectedStockTake = new StockTake();
                    }
                    OnPropertyChanged("SelectedGrnDataList");
                }
                catch { }                
            }
        }

        public StockTakeListPageVM()
        {
            //StockTakeList = LoadFromDB.LoadStockTake(App.DatabaseLocation);
            StockTakeList = Helpers.Data.StockTakeList;
        }

        public void RefreshItem()
        {
            StockTakeList = Helpers.Data.StockTakeList;
            //StockTakeList = LoadFromDB.LoadStockTake(App.DatabaseLocation);
        }

        public async void DataDeletion(StockTake Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result =  ClearFromDB.DeleteStockTake(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Item Deleted Successfully");
                        Helpers.Data.StockTakeList.Remove(Selected);
                        StockTakeList = Helpers.Data.StockTakeList;
                        OnPropertyChanged("StockTakeList");
                        SelectedStockTake = new StockTake();

                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Error while Deleting");
                    }
                }
            }
            catch { }
            
        }
    }
}
