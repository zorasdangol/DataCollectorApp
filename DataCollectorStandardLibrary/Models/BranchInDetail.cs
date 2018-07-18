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

    public class BranchInSummary: BranchItem
    {
        public int enteredQuantity { get; set; }
        public int difference { get; set; }
    }
    
}

