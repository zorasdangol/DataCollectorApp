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
        public string BATCHID { get; set; }
        public DateTime DATE { get; set; }
        public string USERNAME { get; set; }
        public int STATUS { get; set; }
        public string STAMP { get; set; }

        public Batch()
        {
            LOCATIONNAME = "";
            BATCHNO = "";
            BATCHID = "";
            DATE = new DateTime();
            USERNAME = "";
            STATUS = 0;
            STAMP = "";
        }

        public Batch(Batch batch)
        {
            LOCATIONNAME = batch.LOCATIONNAME;
            BATCHNO = batch.BATCHNO;
            BATCHID = batch.BATCHID;
            DATE = batch.DATE;
            USERNAME = batch.USERNAME;
            STATUS = batch.STATUS;
            STAMP = batch.STAMP;
        }
    }

    public class Session
    {
        [PrimaryKey]
        public int SESSIONID { get; set; }
        public string SESSIONNAME { get; set; }
        public DateTime DATE { get; set; }
        public string USERNAME { get; set; }
        public int STATUS { get; set; }

        public Session()
        {
            SESSIONNAME = "";
            DATE = DateTime.Today;
            USERNAME = "";
            STATUS = 0;
        }

        public Session(Session Session)
        {
            SESSIONNAME = Session.SESSIONNAME;
            SESSIONID = Session.SESSIONID;
            DATE = Session.DATE;
            USERNAME = Session.USERNAME;
            STATUS = Session.STATUS;
        }
    }

    public class Location
    {
        public string NAME { get; set; }
        public int LOC_ID { get; set; }
        public string WAREHOUSE { get; set; }
        public int PARENT { get; set; }
        public int LEVELS { get; set; }

        public Location()
        {
            NAME = "";
            LOC_ID = 0;
            WAREHOUSE = "";
            PARENT = 0;
            LEVELS = 0;
        }
    }
}
