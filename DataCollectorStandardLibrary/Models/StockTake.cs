using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class StockTake
    {
        public int ind { get; set; }
        public string BCODE { get; set; }
        public string MCODE { get; set; }
        public string DESCA { get; set; }
        public int QUANTITY { get; set; }
        public string BATCHNO { get; set; }
        public string LOCATIONNAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public int SESSIONID { get; set; }

        public StockTake()
        {
            ind = 0;
            BCODE = "";
            MCODE = "";
            DESCA = "";
            QUANTITY = 0;
            BATCHNO = "";
            LOCATIONNAME = "";
            TIMESTAMP = DateTime.Today;
            SESSIONID = 0;
        }

        public void SetStockTake(StockTake StockTake)
        {
            ind = StockTake.ind;
            BCODE = StockTake.BCODE;
            MCODE = StockTake.MCODE;
            DESCA = StockTake.DESCA;
            QUANTITY = StockTake.QUANTITY;
            BATCHNO = StockTake.BATCHNO;
            LOCATIONNAME = StockTake.LOCATIONNAME;
            TIMESTAMP = StockTake.TIMESTAMP;
            SESSIONID = StockTake.SESSIONID;

        }
    }

    public class StockSummary
    {
        public string MCODE { get; set; }
        public String DESCA { get; set; }
        public int QUANTITY { get; set; }
    }
}
