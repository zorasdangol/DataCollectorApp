using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class MenuItem
    {
        
        public string MCODE { get; set; }
        public string MENUCODE { get; set; }
        public string DESCA { get; set; }
        public string BASEUNIT { get; set; }
        public double PRATE_A { get; set; }
        public double RATE_A { get; set; }
        public int VAT { get; set; }
        public string SUPCODE { get; set; }
    }
}
