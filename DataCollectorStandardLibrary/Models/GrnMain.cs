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
        public DateTime trnDate { get; set; }
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


    public class GrnEntry
    {
        public int ind { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }

        public string mcode { get; set; }
        public string barcode { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
        public DateTime expDate { get; set; }
        public string userName { get; set; }
        public string unit { get; set; }
        
        public string desca { get; set; }
        public string supplierName { get; set; }

        public string batchNo { get; set; }
        public string locationName { get; set; }
        public int sessionId { get; set; }


        public GrnEntry()
        {
            ind = 0;
            vchrNo = "";
            division = "";
            mcode = "";
            barcode = "";
            quantity = "";
            rate = "";
            expDate = DateTime.Today;
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
            try
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
            catch { }
        }

        public void SetInitialGrnData(GrnMain GrnMain)
        {
            try
            {
                this.vchrNo = GrnMain.vchrNo;
                this.division = GrnMain.division;
                this.desca = GrnMain.desca;
                this.supplierName = GrnMain.supplierName;
            }
            catch { }
        }
    }

}