using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class Batch
    {
        public string LOCATIONNAME { get; set; }
        [PrimaryKey]
        public string BATCHNO { get; set; }
        public DateTime DATE { get; set; }
        public string USERNAME { get; set; }
        public int STATUS { get; set; }
        public string STAMP { get; set; }

        public Batch()
        {
            LOCATIONNAME = "";
            BATCHNO = "";
            DATE = new DateTime();
            USERNAME = "";
            STATUS = 0;
            STAMP = "";
        }

        public Batch(Batch batch)
        {
            LOCATIONNAME = batch.LOCATIONNAME;
            BATCHNO = batch.BATCHNO;
            DATE = batch.DATE;
            USERNAME = batch.USERNAME;
            STATUS = batch.STATUS;
            STAMP = batch.STAMP;
        }
    }
}
