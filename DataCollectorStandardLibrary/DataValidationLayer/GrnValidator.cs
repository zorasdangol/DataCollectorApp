using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
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
