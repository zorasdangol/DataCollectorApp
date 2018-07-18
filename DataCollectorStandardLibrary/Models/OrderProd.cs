
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{ 
    public class OrderProd 
    {      
        public string VCHRNO { get; set; }
        public string MCODE { get; set; }
        public string BARCODE { get; set; }
        public float QUANTITY { get; set; }
        public double RATE { get; set; }
        public string SUPPLIERCODE { get; set; }

        public OrderProd(OrderProd orderProd)
        {
            this.VCHRNO = orderProd.VCHRNO;
            this.MCODE = orderProd.MCODE;
            this.BARCODE = orderProd.BARCODE;
            this.QUANTITY = orderProd.QUANTITY;
            this.RATE = orderProd.RATE;
            this.SUPPLIERCODE = orderProd.SUPPLIERCODE;
        }
        public OrderProd()
        {
            VCHRNO = "";
            MCODE = "";
            BARCODE = "";
            QUANTITY = 0;
            RATE = 0;
            SUPPLIERCODE = "";
        }
    }
}
