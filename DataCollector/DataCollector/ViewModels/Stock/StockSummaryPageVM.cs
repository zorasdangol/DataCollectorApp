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
                
            }
            catch(Exception e) { }
            
        }

    }
}
