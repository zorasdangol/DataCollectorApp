using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectorStandardLibrary.DataValidationLayer
{
    public class GrnValidator
    {
        public static FunctionResponse CheckInputData(StockTake StockTake, BarCode SelectedBarCode)
        {
            if (StockTake == null || string.IsNullOrEmpty(SelectedBarCode.BCODE))
                return new FunctionResponse() { status = "error", Message = "InCorrect BarCode First" };
            else if (string.IsNullOrEmpty(StockTake.MCODE))
                return new FunctionResponse() { status = "error", Message = "InCorrect BarCode First" };
            else if ((StockTake.QUANTITY) == 0)
                return new FunctionResponse() { status = "error", Message = "Insert Quantity First" };
            else
                return new FunctionResponse() { status = "ok", Message = "Correct input" };
        }

        public static List<StockSummary> StockTakeToStockSummary(List<GrnProd> GrnDataList)
        {
            try
            {
                List<StockSummary> StockSummaryList = new List<StockSummary>();

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
                return StockSummaryList;

            }
            catch { return new List<StockSummary>(); }
        }
    }

    public class GrnMainValidator
    {
        public static FunctionResponse CheckInputData(GrnMain GrnMain)
        {
            if (string.IsNullOrEmpty(GrnMain.division))
            {
                return new FunctionResponse() { status = "error", Message = "Select store" };
            }

            else if (string.IsNullOrEmpty(GrnMain.vchrNo))
            {
                return new FunctionResponse() { status = "error", Message = "Entry No. not selected." };
            }

            else if (string.IsNullOrEmpty(GrnMain.trnAc))
            {
                return new FunctionResponse() { status = "error", Message = "Select supplier" };
            }

            else if (string.IsNullOrEmpty(GrnMain.wareHouse))
            {
                return new FunctionResponse() { status = "error", Message = "Select warehouse" };
            }

            else
                return new FunctionResponse() { status = "ok", Message = "Correct" };
        }
    }
}
