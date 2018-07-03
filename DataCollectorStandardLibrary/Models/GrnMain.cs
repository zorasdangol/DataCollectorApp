using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Models
{
    public class GrnMain
    {        
        public string vchrNo { get; set; }
        public string division { get; set; }
        public string chalanNo { get; set; }
        public string trnDate { get; set; }
        public string trnAc { get; set; }
        public string ParAc { get; set; }
        public string trnMode { get; set; }
        public string refOrdBill { get; set; }
        public string remarks { get; set; }
        public string wareHouse { get; set; }
        public string isTaxInvoice { get; set; }
        
        public int curNo { get; set; }


        public string supplierName { get; set; }
        public string desca { get; set; }

        public string batchNo { get; set; }
        public string locationName { get; set; }
        public int sessionId { get; set; }

        //public void SetGrnMain(GrnMain GrnMain)
        //{
        //    vchrNo = GrnMain.vchrNo;
        //    division = GrnMain.division;
        //    chalanNo = GrnMain.chalanNo;

        //}

    }

}