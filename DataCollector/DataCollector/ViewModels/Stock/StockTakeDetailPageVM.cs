using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views.Stock;
using DataCollectorStandardLibrary.Models;
using Xamarin.Forms;

namespace DataCollector.ViewModels.Stock
{
    public class StockTakeDetailPageVM:BaseViewModel
    {
        public List<Division> DivisionList { get; set; }

        private List<Warehouse> _WarehouseList;
        public List<Warehouse> WarehouseList
        {
            get { return _WarehouseList; }
            set
            {
                if (value == null)
                    return;
                _WarehouseList = value;
                OnPropertyChanged("WarehouseList");
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

        private StockTake _StockTake;
        public StockTake StockTake
        {
            get { return _StockTake; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        _StockTake = value;
                        if (!string.IsNullOrEmpty(StockTake.division))
                        {
                            SelectedWarehouse = WarehouseList.Find(x => x.NAME == StockTake.wareHouse);
                        }
                        OnPropertyChanged("StockTake");
                    }

                }
                catch { }
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
                    if (value != null && !string.IsNullOrEmpty(value.sid))
                    {
                        _SelectedStockTake = value;
                        StockTake = new StockTake();
                        StockTake = value;
                        OnPropertyChanged("SelectedStockTake");
                    }
                }
                catch { }
            }
        }

        private Division _SelectedStore;
        public Division SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                try
                {
                    _SelectedStore = value;

                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        StockTake = new StockTake();
                        StockTake.division = value.INITIAL;
                        WarehouseList = Helpers.Data.WarehouseList.Where(x => x.DIVISION == value.INITIAL).ToList();
                       // StockTake.sid = Helpers.Data.SelectedBatch.BATCHNO;                        
                        GrnEntrySet(value.INITIAL);
                    }
                    OnPropertyChanged("SelectedStore");
                }
                catch (Exception e)
                { }

            }
        }

        private Warehouse _SelectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get { return _SelectedWarehouse; }
            set
            {
                try
                {
                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        StockTake.wareHouse = value.NAME;
                        _SelectedWarehouse = value;
                        OnPropertyChanged("SelectedWarehouse");
                    }
                }
                catch (Exception e)
                { }

            }
        }

        public Command SetCommand { get; set; }

        public StockTakeDetailPageVM()
        {
            SetCommand = new Command(ExecuteSetCommand);
            Helpers.Data.StockTake = new StockTake();
            SelectedStore = new Division();            
            SelectedStockTake = new StockTake();
            StockTake = new StockTake();
            DivisionList = Helpers.Data.DivisionList;
            WarehouseList = Helpers.Data.WarehouseList;
            LoadFromDB.LoadBatch(App.DatabaseLocation);
            LoadFromDB.LoadSession(App.DatabaseLocation);
        }



        public void GrnEntrySet(string store)
        {
            try
            {
                LoadFromDB.LoadStockTakeList(App.DatabaseLocation);
                var list = Helpers.Data.StockTakeList.Where(x => (x.division == store) && (x.IsSaved == false)).ToList().OrderBy(x => x.ind).ToList();

                var query = (from m in list group m by new { m.sid, m.ind, m.division, m.wareHouse, m.trnDate } into mygroup select mygroup.FirstOrDefault()).Distinct();
                StockTakeList = query.ToList().Select(m => new StockTake { sid = m.sid, ind = m.ind, division = m.division, wareHouse = m.wareHouse, trnDate = m.trnDate }).ToList();

                var maxObject = Helpers.Data.StockTakeList.Where(x => x.division == store).OrderByDescending(item => item.ind).FirstOrDefault();

                if (Helpers.Data.EntryMode == "New")
                {
                    if (maxObject == null)
                    {
                        StockTake.sid = "ST" + 1;
                        StockTake.ind = 1;
                        // GrnCount = 1;
                    }
                    else
                    {
                        StockTake.sid = "ST" + (Convert.ToInt32(maxObject.ind) + 1);
                        var GrnCount = maxObject.ind + 1;
                        StockTake.ind = GrnCount;
                    }

                    StockTakeList.Add(StockTake);
                    SelectedStockTake = StockTakeList.Where(x => (x.sid == StockTake.sid) && (x.division == StockTake.division)).ToList().FirstOrDefault();
                }

                OnPropertyChanged("StockTake");

            }
            catch (Exception e)
            { }

        }

        public void ExecuteSetCommand()
        {
            try
            {
                if (String.IsNullOrEmpty(StockTake.division))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Division is not selected.");
                }
                else if (String.IsNullOrEmpty(StockTake.sid))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Batch is not set.");
                }
                else if (String.IsNullOrEmpty(StockTake.wareHouse))
                {
                    DependencyService.Get<IMessage>().ShortAlert("Warehouse is not selected.");
                }
                else
                {
                    Helpers.Data.StockTake = StockTake;
                    Helpers.Data.StockTakeList = new List<StockTake>();
                    App.Current.MainPage = new TabbedStockPage();
                }
            }
            catch (Exception e)
            { }
        }


    }
}
