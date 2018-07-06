using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectorStandardLibrary.DataValidationLayer
{
    public class StockTakeValidator
    {

        public static FunctionResponse CheckInputData(StockTake StockTake, BarCode SelectedBarCode)
        {
            if (StockTake == null || string.IsNullOrEmpty(SelectedBarCode.BCODE))
                return new FunctionResponse() { status = "error", Message = "Empty stockTake" };
            else if (string.IsNullOrEmpty(StockTake.MCODE))
                return new FunctionResponse() { status = "error", Message = "Enter Correct BarCode First" };
            else if ((StockTake.QUANTITY) == 0)
                return new FunctionResponse() { status = "error", Message = "Insert Quantity First" };
            else
                return new FunctionResponse() { status = "ok", Message = "Correct input" };

        }

        public static List<StockSummary> StockTakeToStockSummary(List<StockTake> StockTakeList)
        {
            List<StockSummary> StockSummaryList = new List<StockSummary>();
            if (StockTakeList != null)
            {
                var filterStock = StockTakeList.GroupBy(x => x.DESCA).Select(y => y.FirstOrDefault()).ToList();

                foreach (var st in filterStock)
                {
                    StockSummaryList.Add(new StockSummary() { DESCA = st.DESCA });
                }
                foreach (var stockSummary in StockSummaryList)
                {
                    foreach (var stock in StockTakeList)
                    {
                        if (stock.DESCA == stockSummary.DESCA)
                        {
                            stockSummary.QUANTITY += stock.QUANTITY;
                        }
                    }
                }
            }
            return StockSummaryList;
        }



    }
}
