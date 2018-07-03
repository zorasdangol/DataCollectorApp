using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class Warehouse
    {
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string REMARKS { get; set; }
        public string ISDEFAULT { get; set; }
        public string IsAdjustment { get; set; }
        public string AdjustmentAcc { get; set; }
        public string ISVIRTUAL { get; set; }
        public string VIRTUAL_PARENT { get; set; }
        public string DIVISION { get; set; }
    }
}
