using DataCollector.DatabaseAccess;
using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.Stock
{
    public class StockSummaryPageVM:BaseViewModel
    {
        private List<StockSummary> _StockSummaryList;
        public List<StockSummary> StockSummaryList
        {
            get { return _StockSummaryList; }
            set
            {
                _StockSummaryList = value;
                OnPropertyChanged("StockSummaryList");
            }
        }

        public List<StockTake> StockTakeList { get; set; }

        public StockSummaryPageVM()
        {
            try
            {
                StockTakeList = Helpers.Data.StockTakeList;
            }
            catch(Exception e)
            { }           
            
        }

        public void RefreshStockSummaryList()
        {
            try
            {
                StockSummaryList = new List<StockSummary>();
                StockTakeList = Helpers.Data.StockTakeList;
                StockSummaryList = StockTakeValidator.StockTakeToStockSummary(StockTakeList);


                //StockTakeList = LoadFromDB.LoadStockTake(App.DatabaseLocation);

                //if (StockTakeList != null)
                //{
                //    var filterStock = StockTakeList.GroupBy(x => x.DESCA).Select(y => y.FirstOrDefault()).ToList();

                //    foreach (var st in filterStock)
                //    {
                //        StockSummaryList.Add(new StockSummary() { DESCA = st.DESCA });
                //    }
                //    foreach (var stockSummary in StockSummaryList)
                //    {
                //        foreach (var stock in StockTakeList)
                //        {
                //            if (stock.DESCA == stockSummary.DESCA)
                //            {
                //                stockSummary.QUANTITY += stock.QUANTITY;
                //            }
                //        }
                //    }
                //}
            }
            catch(Exception e) { }
            
        }

    }
}
