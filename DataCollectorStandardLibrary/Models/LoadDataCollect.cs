using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class LoadDataCollect
    {
        public  int ind { get; set; }
        public int curNo { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }
        public string menucode { get; set; }
        public string mcode { get; set; }
        public string bcode { get; set; }
        public string qty { get; set; }
        public string sid { get; set; }
        public string trnDate { get; set; }
        public string warehouse { get; set; }
        public string trnUser { get; set; }
        public string trnTime { get; set; }
        public string location { get; set; }

        public bool IsUpload { get; set; }
        public bool IsSaved { get; set; }
        public string remarks { get; set; }

        public LoadDataCollect()
        {
            ind = 0;
            curNo = 0;
            vchrNo = "";
            division = "";
            menucode = "";
            mcode = "";
            bcode = "";
            qty = "";
            sid = "";
            trnDate = DateTime.Today.Date.ToString();
            warehouse = "";
            trnUser = "";
            trnTime = "";
            location = "";

            IsUpload = false;
            IsSaved = false;
            remarks = "";
        }

        public void SetLoadDataCollect(StockTake StockTake)
        {
            ind = StockTake.ind;
            curNo = StockTake.curNo;
            vchrNo = StockTake.vchrNo;
            division = StockTake.division;
            menucode = StockTake.MENUCODE;
            mcode = StockTake.MCODE;
            bcode = StockTake.BCODE;
            qty = StockTake.QUANTITY.ToString();
            sid = StockTake.sid;
            trnDate = StockTake.trnDate;
            warehouse = StockTake.wareHouse;
            trnUser = StockTake.trnUser;
            //trnTime = "";
            location = StockTake.location;

            IsUpload = StockTake.IsUpload;
            IsSaved = StockTake.IsSaved;
            remarks = "";

        }
    }
}
