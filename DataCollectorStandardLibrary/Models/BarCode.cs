using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class BarCode
    {
        public string BCODE { get; set; }
        public string DESCA { get; set; }
        public string MCODE { get; set; }
        public string UNIT { get; set; }
        public double RATE { get; set; }

        public BarCode()
        {
            BCODE = "";
            DESCA = "";
            MCODE = "";
            UNIT = "";
            RATE = 0;
        }

        public BarCode(BarCode BarCode)
        {
            BCODE = BarCode.BCODE;
            DESCA = BarCode.DESCA;
            MCODE = BarCode.MCODE;
            UNIT = BarCode.UNIT;
            RATE = BarCode.RATE;
        }
    }
}
