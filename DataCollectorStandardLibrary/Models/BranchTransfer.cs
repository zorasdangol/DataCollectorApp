using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Models
{
    public class BranchTransferDetail
    {
        public int curNo { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }
        public string divisionTo { get; set; }
        public string divisionFrom { get; set; }
        public string billToAdd { get; set; }

        public DateTime trnDate { get; set; }
        public string wareHouse { get; set; }
        public string remarks { get; set; }

        public BranchTransferDetail()
        {
            curNo = 0;
            vchrNo = "";
            division = "";
            divisionTo = "";
            divisionFrom = "";
            billToAdd = "";
            trnDate = DateTime.Today;
            wareHouse = "";
            remarks = "";
        }
    }

    public class BranchItem
    {
        public int ind { get; set; }
        public string vchrNo { get; set; }
        public string division { get; set; }
        public string divisionTo { get; set; }
        public string divisionFrom { get; set; }
        public string mcode { get; set; }
        public string barcode { get; set; }
        public int quantity { get; set; }
        public string rate { get; set; }
        public string userName { get; set; }
        public string unit { get; set; }
        public string billToAdd { get; set; }

        public string desca { get; set; }

        public void SetBranchItem(BranchItem BranchItem)
        {
            try
            {
                ind = BranchItem.ind;
                vchrNo = BranchItem.vchrNo;
                division = BranchItem.division;
                divisionTo = BranchItem.divisionTo;
                divisionFrom = BranchItem.divisionFrom;
                mcode = BranchItem.mcode;
                barcode = BranchItem.barcode;
                quantity = BranchItem.quantity;
                rate = BranchItem.rate;
                userName = BranchItem.userName;
                unit = BranchItem.unit;

                desca = BranchItem.desca;
            }
            catch { }
        }

        public void SetInitialBranchItem(BranchTransferDetail BranchTransferDetail)
        {
            try
            {
                this.vchrNo = BranchTransferDetail.vchrNo;
                this.division = BranchTransferDetail.division;
                this.divisionTo = BranchTransferDetail.divisionTo;
                this.divisionFrom = BranchTransferDetail.divisionFrom;
                this.billToAdd = BranchTransferDetail.billToAdd;
            }
            catch { }
        }     
    }
}
