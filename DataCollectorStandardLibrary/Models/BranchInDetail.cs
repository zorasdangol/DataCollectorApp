using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Models
{
    public class BranchInDetail : BranchTransferDetail
    {
    }

    public class BranchInItem : BranchItem
    {
    }

    public class BranchInMaster
    {
        public BranchInDetail BranchInMain { get; set; }
        public List<BranchInItem> BranchInProdList { get; set; }

        public BranchInMaster()
        {
            BranchInMain = new BranchInDetail();
            BranchInProdList = new List<BranchInItem>();
        }
    }

    public class BranchInSummary: BranchItem
    {
        public decimal enteredQuantity { get; set; }
        public decimal difference { get; set; }
    }
    
}

