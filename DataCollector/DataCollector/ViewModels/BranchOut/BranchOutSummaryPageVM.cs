using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.BranchOut
{
    public class BranchOutSummaryPageVM:BaseViewModel
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

        public List<BranchOutItem> BranchOutItemList { get; set; }

        public BranchOutSummaryPageVM()
        {
            RefreshStockSummaryList();
        }

        public void RefreshStockSummaryList()
        {
            try
            {
                StockSummaryList = new List<StockSummary>();
                //GrnDataList = LoadFromDB.LoadGrnDataList(App.DatabaseLocation, Helpers.Data.GrnMain);
                BranchOutItemList = Helpers.Data.BranchOutItemList;
                StockSummaryList = BranchOutDetailValidator.StockTakeToStockSummary(BranchOutItemList);
            }
            catch (Exception e) { }

        }
    }
}
