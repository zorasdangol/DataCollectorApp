using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.DataList
{
    public class StockDataListPageVM:BaseViewModel
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

        private StockTake _Selected;
        public StockTake Selected
        {
            get { return _Selected; }
            set
            {
                _Selected = value;
                if (value != null && !string.IsNullOrEmpty(value.sid))
                {
                    DataDeletion(value);
                }
                OnPropertyChanged("Selected");
            }
        }

        public Command DeleteCommand { get; set; }

        public StockDataListPageVM()
        {
            RefreshItem();
            DeleteCommand = new Command(ExecuteDeleteCommand);
        }

        public void RefreshItem()
        {
            LoadFromDB.LoadBatch(App.DatabaseLocation);
            LoadFromDB.LoadSession(App.DatabaseLocation);
            var list = LoadFromDB.LoadStockTakeList(App.DatabaseLocation);
            
            var query = (from m in list group m by new { m.sid, m.ind, m.division, m.wareHouse, m.trnDate, m.IsSaved ,m.vchrNo} into mygroup select mygroup.FirstOrDefault()).Distinct();
            DataList = query.ToList().Select(m => new StockTake { sid = m.sid, ind = m.ind, division = m.division, wareHouse = m.wareHouse, trnDate = m.trnDate, IsSaved = m.IsSaved, vchrNo =m.vchrNo }).ToList();

        }

        public async void ExecuteDeleteCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
            if (result)
            {
                Helpers.Data.StockTakeList = null;
                ClearFromDB.ClearStockTakeAll(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All StockTake Cleared Successfully");
                this.RefreshItem();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
        }

        public async void DataDeletion(StockTake Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteStockTake(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        RefreshItem();
                        Selected = new StockTake();
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Error while Deleting");
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }

    }
}
