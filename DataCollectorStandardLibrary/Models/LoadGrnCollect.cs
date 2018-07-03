using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class LoadGrnCollect
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

        public void SetInitialGrnData(GrnMain GrnMain)
        {
            this.vchrNo = GrnMain.vchrNo;
            this.division = GrnMain.division;      

            this.desca = GrnMain.desca;

            this.supplierName = GrnMain.supplierName;
        }
    }
}
