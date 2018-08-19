using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectorStandardLibrary.DataValidationLayer
{

    #region BranchOut
    public class BranchOutDetailValidator
    {
        public static FunctionResponse CheckInputData(BranchOutDetail BranchOutDetail)
        {
            if (string.IsNullOrEmpty(BranchOutDetail.division))
                return new FunctionResponse() { status = "error", Message = "Select Store" };
            else if (string.IsNullOrEmpty(BranchOutDetail.billToAdd))
                return new FunctionResponse() { status = "error", Message = "Select Division To" };
            else if (string.IsNullOrEmpty(BranchOutDetail.vchrNo))
                return new FunctionResponse() { status = "error", Message = "Entry No. not selected" };
            else if (string.IsNullOrEmpty(BranchOutDetail.wareHouse))
                return new FunctionResponse() { status = "error", Message = "Select Warehouse" };
            else if (BranchOutDetail.division == BranchOutDetail.billToAdd)
                return new FunctionResponse() { status = "error", Message = "Store and Division to cannot be same" };
            else
                return new FunctionResponse() { status = "ok", Message = "Correct" };
        }

         public static List<StockSummary> StockTakeToStockSummary(List<BranchOutItem> BranchOutItemList)
        {
            try
            {
                List<StockSummary> StockSummaryList = new List<StockSummary>();
                if (BranchOutItemList != null)
                {
                    var filterStock = BranchOutItemList.GroupBy(x => x.desca).Select(y => y.FirstOrDefault()).ToList();

                    foreach (var st in filterStock)
                    {
                        StockSummaryList.Add(new StockSummary() { MCODE = st.mcode, DESCA = st.desca , QUANTITY = 0});
                    }
                    foreach (var stockSummary in StockSummaryList)
                    {
                        foreach (var stock in BranchOutItemList)
                        {
                            if (stock.desca == stockSummary.DESCA)
                            {
                                stockSummary.QUANTITY += Convert.ToDecimal(stock.quantity);
                            }
                        }
                    }
                }
                return StockSummaryList;
            }
            catch { return new List<StockSummary>(); }
        }
    }
    #endregion BranchOut


    #region BranchIn
    public class BranchInDetailValidator
    {
        public static FunctionResponse CheckInputData(BranchInDetail BranchInDetail)
        {
            if (string.IsNullOrEmpty(BranchInDetail.division))
                return new FunctionResponse() { status = "error", Message = "Select Store" };
            else if (string.IsNullOrEmpty(BranchInDetail.billToAdd))
                return new FunctionResponse() { status = "error", Message = "Select Division From" };
            else if (string.IsNullOrEmpty(BranchInDetail.vchrNo))
                return new FunctionResponse() { status = "error", Message = "Entry No. not selected" };
            else if (string.IsNullOrEmpty(BranchInDetail.wareHouse))
                return new FunctionResponse() { status = "error", Message = "Select Warehouse" };
            else if (string.IsNullOrEmpty(BranchInDetail.chalanNo))
                return new FunctionResponse() { status = "error", Message = "Enter Ref No." };
            else if (BranchInDetail.division == BranchInDetail.billToAdd)
                return new FunctionResponse() { status = "error", Message = "Store and Division to cannot be same" };
            else
                return new FunctionResponse() { status = "ok", Message = "Correct" };
        }

        public static List<StockSummary> StockTakeToStockSummary(List<BranchInItem> BranchInItemList)
        {
            try
            {
                List<StockSummary> StockSummaryList = new List<StockSummary>();
                if (BranchInItemList != null)
                {
                    var filterStock = BranchInItemList.GroupBy(x => x.desca).Select(y => y.FirstOrDefault()).ToList();

                    foreach (var st in filterStock)
                    {
                        StockSummaryList.Add(new StockSummary() { MCODE = st.mcode, DESCA = st.desca , QUANTITY = 0});
                    }
                    foreach (var stockSummary in StockSummaryList)
                    {
                        foreach (var stock in BranchInItemList)
                        {
                            if (stock.desca == stockSummary.DESCA)
                            {
                                stockSummary.QUANTITY += Convert.ToDecimal(stock.quantity);
                            }
                        }
                    }
                }
                return StockSummaryList;
            }
            catch { return new List<StockSummary>(); }
        }

        public static List<BranchInSummary> ConvertToReceiveItemSummary(List<BranchInItem> BranchInItemList)
        {
            try
            {
                List<BranchInSummary> SendBranchInSummaryList = new List<BranchInSummary>();
                if (BranchInItemList != null)
                {
                    var filterStock = BranchInItemList.GroupBy(x => x.mcode).Select(y => y.FirstOrDefault()).ToList();

                    foreach (var st in filterStock)
                    {
                        SendBranchInSummaryList.Add(new BranchInSummary() { mcode = st.mcode, desca = st.desca , quantity = "0" , enteredQuantity = 0, difference = 0});
                    }
                    foreach (var stockSummary in SendBranchInSummaryList)
                    {
                        foreach (var stock in BranchInItemList)
                        {
                            if (stock.mcode == stockSummary.mcode)
                            {
                                stockSummary.quantity = (Convert.ToDecimal(stockSummary.quantity) + Convert.ToDecimal(stock.quantity)).ToString();
                            }
                        }
                    }
                }
                return SendBranchInSummaryList;
            }
            catch { return new List<BranchInSummary>(); }
        }

        public static List<BranchInSummary> BranchInToBranchInSummary(List<BranchInSummary> ReceiveSummaryList,List<BranchInItem> BranchInItemList)
        {
            try
            {
                foreach(var item in ReceiveSummaryList)
                {
                    item.enteredQuantity = 0;
                    item.difference = Convert.ToDecimal(item.quantity) - item.enteredQuantity;
                }

                //ReceiveSummaryList.Select(x => x.enteredQuantity = 0);

                //ReceiveSummaryList.ToList().ForEach();

                foreach (var receiveitem in ReceiveSummaryList)
                {
                    
                    foreach (var branchinitem in BranchInItemList)
                    {
                        if (receiveitem.mcode == branchinitem.mcode)
                        {
                            receiveitem.enteredQuantity = receiveitem.enteredQuantity + Convert.ToDecimal(branchinitem.quantity);
                            receiveitem.difference = Convert.ToDecimal(receiveitem.quantity) - Convert.ToDecimal(receiveitem.enteredQuantity);
                        }
                    }
                }
                return ReceiveSummaryList;
            }
            catch { return ReceiveSummaryList; }
        }
    }
    #endregion BranchIn
}
