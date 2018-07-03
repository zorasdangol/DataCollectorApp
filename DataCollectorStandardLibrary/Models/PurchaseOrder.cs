using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class PurchaseOrder
    {
        public string VCHRNO { get; set; }
        public string MCODE { get; set; }
        public string BARCODE { get; set; }
        public string QUANTITY { get; set; }
        public string RATE { get; set; }
        public string SUPPLIERCODE { get; set; }

    }
}
