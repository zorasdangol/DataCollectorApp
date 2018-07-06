using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Models
{
    public class PickingDetail
    {
        public string pickNo { get; set; }
        public string division { get; set; }
        public string wareHouse { get; set; }
    }

    public class StockTakeMax:StockTake
    {        
       public string pickNo { get; set; }

    }
}
