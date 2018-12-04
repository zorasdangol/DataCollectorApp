using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class StockTake:BaseModel
    {
        public int ind { get; set; }
        public int curNo { get; set; }
        public string sid { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }
        public string wareHouse { get; set; }

        public string BCODE { get; set; }
        public string MCODE { get; set; }
        public string MENUCODE { get; set; }
        public string UNIT { get; set; }
        public string DESCA { get; set; }
        public decimal QUANTITY { get; set; }

        public string Supplier { get; set; }
        public double PRATE { get; set; }

        public string BATCHNO { get; set; }
        public string LOCATIONNAME { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public int SESSIONID { get; set; }
        public double RATE { get; set; }

        public string trnDate { get; set; }
        public string trnUser { get; set; }
        public string location { get; set; }
        public bool IsSaved { get; set; }

        private bool _IsUpload;
        public bool IsUpload
        {
            get { return _IsUpload; }
            set
            {
                _IsUpload = value;
                OnPropertyChanged("IsUpload");
            }
        }


        public StockTake()
        {
            ind = 0;
            curNo = 0;
            sid = "";
            vchrNo = "";
            division = "";
            wareHouse = "";

            BCODE = "";
            MCODE = "";
            DESCA = "";
            QUANTITY = 1;
            BATCHNO = "";
            LOCATIONNAME = "";
            TIMESTAMP = DateTime.Today;
            SESSIONID = 0;
            RATE = 0.0;

            trnDate = DateTime.Today.Date.ToString();
            trnUser = "";
            location = "";
            IsSaved = false;
            IsUpload = false;
        }

        public void SetStockTake(StockTake StockTake)
        {
            ind = StockTake.ind;
            curNo = StockTake.curNo;
            sid = StockTake.sid;
            vchrNo = StockTake.vchrNo;
            division = StockTake.division;
            wareHouse = StockTake.wareHouse;

            BCODE = StockTake.BCODE;
            MCODE = StockTake.MCODE;
            DESCA = StockTake.DESCA;
            QUANTITY = StockTake.QUANTITY;
            BATCHNO = StockTake.BATCHNO;
            LOCATIONNAME = StockTake.LOCATIONNAME;
            TIMESTAMP = StockTake.TIMESTAMP;
            SESSIONID = StockTake.SESSIONID;
            RATE = StockTake.RATE;

            trnDate = StockTake.trnDate;
            trnUser = StockTake.trnUser;
            location = StockTake.location;

            IsSaved = StockTake.IsSaved;
            IsUpload = StockTake.IsUpload;
        }
    }

    public class StockSummary
    {
        public string MCODE { get; set; }
        public String DESCA { get; set; }
        public decimal QUANTITY { get; set; }
    }
}
