using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.Models
{
    public class Session
    {      
        [PrimaryKey]
        public int SESSIONID { get; set; }
        public DateTime DATE { get; set; }
        public string USERNAME { get; set; }
        public int STATUS { get; set; }

        public Session()
        {
            DATE = DateTime.Today;
            USERNAME = "";
            STATUS = 0;
        }
    }
}
