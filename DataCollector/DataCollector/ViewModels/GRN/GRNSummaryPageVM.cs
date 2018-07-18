using DataCollector.DatabaseAccess;
using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.GRN
{
    public class GRNSummaryPageVM:BaseViewModel
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

        public List<GrnEntry> GrnDataList { get; set; }

        public GRNSummaryPageVM()
        {
            try
            {
                RefreshStockSummaryList();
            }
            catch (Exception e)
            { }

        }

        public void RefreshStockSummaryList()
        {
            try
            {
                StockSummaryList = new List<StockSummary>();
                //GrnDataList = LoadFromDB.LoadGrnDataList(App.DatabaseLocation, Helpers.Data.GrnMain);
                GrnDataList = Helpers.Data.GrnEntryList;
                StockSummaryList = GrnValidator.StockTakeToStockSummary(GrnDataList);
                
            }
            catch (Exception e) { }

        }
    }
}
