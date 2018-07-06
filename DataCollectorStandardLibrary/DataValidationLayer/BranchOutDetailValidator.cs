using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.DataValidationLayer
{
    public class BranchOutDetailValidator
    {
        public static FunctionResponse CheckInputData(BranchOutDetail BranchOutDetail)
        {
            if (string.IsNullOrEmpty(BranchOutDetail.division))
                return new FunctionResponse() { status = "error", Message = "Select Store" };
            else if (string.IsNullOrEmpty(BranchOutDetail.divisionTo))
                return new FunctionResponse() { status = "error", Message = "Select Division To" };
            else if (string.IsNullOrEmpty(BranchOutDetail.vchrNo))
                return new FunctionResponse() { status = "error", Message = "Entry No. not selected" };
            else if (string.IsNullOrEmpty(BranchOutDetail.wareHouse))
                return new FunctionResponse() { status = "error", Message = "Select Warehouse" };
            else if (BranchOutDetail.division == BranchOutDetail.divisionTo)
                return new FunctionResponse() { status = "error", Message = "Store and Division to cannot be same" };
            else
                return new FunctionResponse() { status = "ok", Message = "Correct" };
        }
    }
}
