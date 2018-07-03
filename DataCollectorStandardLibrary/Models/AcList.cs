using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class AcList
    {
        public string ACID { get; set; }
        public string ACNAME { get; set; }
        public string PARENT { get; set; }
        public string ACCODE { get; set; }
        public DateTime EDATE { get; set; }
        public string PTYPE { get; set; }

        public AcList()
        {
            ACID = "";
            ACNAME = "";
            PARENT = "";
            ACCODE = "";
            EDATE = new DateTime();
            PTYPE = "";
        }

        public AcList(AcList ac)
        {
            ACID = ac.ACID;
            ACNAME = ac.ACNAME;
            PARENT = ac.PARENT;
            ACCODE = ac.ACCODE;
            EDATE = ac.EDATE;
            PTYPE = ac.PTYPE;
        }
    }
}
