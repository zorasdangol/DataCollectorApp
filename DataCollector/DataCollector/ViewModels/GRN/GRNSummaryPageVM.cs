using DataCollector.DatabaseAccess;
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
                GrnDataList = Helpers.Data.GrnDataList;
                if (GrnDataList != null)
                {
                    var filterStock = GrnDataList.GroupBy(x => x.desca).Select(y => y.FirstOrDefault()).ToList();

                    foreach (var st in filterStock)
                    {
                        StockSummaryList.Add(new StockSummary() { DESCA = st.desca });
                    }
                    foreach (var stockSummary in StockSummaryList)
                    {
                        foreach (var stock in GrnDataList)
                        {
                            if (stock.desca == stockSummary.DESCA)
                            {
                                stockSummary.QUANTITY += Convert.ToInt16(stock.quantity);
                            }
                        }
                    }
                }
            }
            catch (Exception e) { }

        }
    }
}
