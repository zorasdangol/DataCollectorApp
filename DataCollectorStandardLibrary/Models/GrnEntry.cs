using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Models
{
    public class GrnEntry
    {
        public int ind { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }

        public string mcode { get; set; }
        public string barcode { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
        public string expDate { get; set; }
        public string userName { get; set; }
        public string unit { get; set; }
        public string batchNo { get; set; }
        public string locationName { get; set; }
        public int sessionId { get; set; }

        public string desca { get; set; }

        public string supplierName { get; set; }

        public GrnEntry()
        {
            ind = 0;
            vchrNo = "";
            division = "";
            mcode = "";
            barcode = "";
            quantity = "";
            rate = "";
            expDate = "";
            userName = "";
            unit = "";
            batchNo = "";
            locationName = "";
            sessionId = 0;
            desca = "";
            supplierName = "";

        }

        public void SetGrnEntry(GrnEntry GrnMain)
        {
            ind = GrnMain.ind;
            vchrNo = GrnMain.vchrNo;
            division = GrnMain.division;
            mcode = GrnMain.mcode;
            barcode = GrnMain.barcode;
            quantity = GrnMain.quantity;
            rate = GrnMain.rate;
            expDate = GrnMain.expDate;
            userName = GrnMain.userName;
            unit = GrnMain.unit;
            batchNo = GrnMain.batchNo;
            locationName = GrnMain.locationName;
            sessionId = GrnMain.sessionId;
            desca = GrnMain.desca;
            supplierName = GrnMain.supplierName;

        }

        public void SetInitialGrnData(GrnMain GrnMain)
        {
            this.vchrNo = GrnMain.vchrNo;
            this.division = GrnMain.division;

            this.desca = GrnMain.desca;

            this.supplierName = GrnMain.supplierName;
        }
    }
}
