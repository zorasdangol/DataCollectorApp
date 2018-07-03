using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.Helpers
{
    public class JsonData
    {
        public static List<MenuItem> MenuItemData = new List<MenuItem>()
        {
            new MenuItem(){MCODE= "M1000", MENUCODE = "5.2169", DESCA = "GAIA GREEN ICE TEA 360G LEMON", BASEUNIT = "PC", RATE_A = 440, PRATE_A = 353.98},
            new MenuItem(){MCODE= "M1001", MENUCODE = "5.2169", DESCA = "GAIA GREEN ICE TEA 360G PEACH", BASEUNIT = "PC", RATE_A = 440, PRATE_A = 353.98},
            new MenuItem(){MCODE= "M1002", MENUCODE = "5.2169", DESCA = "PEPSI DIET 250ML CAN (D & R)", BASEUNIT = "PC", RATE_A = 440, PRATE_A = 353.98},
            new MenuItem(){MCODE= "M1003", MENUCODE = "5.2169", DESCA = "GAIA GREEN TEA 25BAGS", BASEUNIT = "PC", RATE_A = 440, PRATE_A = 353.98},
            new MenuItem(){MCODE= "M1004", MENUCODE = "5.2169", DESCA = "GAIA GREEN TEA+GINGER 25BAGS", BASEUNIT = "PC", RATE_A = 440, PRATE_A = 353.98},
        };

        public static List<Location> LocationList = new List<Location>()
        {
            new Location(){LOCATIONID = 1, LOCATIONNAME = "Kathmandu" },
            new Location(){LOCATIONID = 2, LOCATIONNAME = "Bagmati" },
            new Location(){LOCATIONID = 3, LOCATIONNAME = "Jumla" },
            new Location(){LOCATIONID = 4, LOCATIONNAME = "Dolpa" },
            new Location(){LOCATIONID = 5, LOCATIONNAME = "Dang" },
            new Location(){LOCATIONID = 6, LOCATIONNAME = "Thankot" },
            new Location(){LOCATIONID = 7, LOCATIONNAME = "Dhankutta" },
            new Location(){LOCATIONID = 8, LOCATIONNAME = "Dharan" },
            new Location(){LOCATIONID = 9, LOCATIONNAME = "Ilam" },
            new Location(){LOCATIONID = 10, LOCATIONNAME = "Chitwan" },
            new Location(){LOCATIONID = 11, LOCATIONNAME = "Bara" },
            new Location(){LOCATIONID = 12, LOCATIONNAME = "Nawalparasi" },
            new Location(){LOCATIONID = 13, LOCATIONNAME = "Butwal" },
            new Location(){LOCATIONID = 14, LOCATIONNAME = "Birgunj" },
            new Location(){LOCATIONID = 15, LOCATIONNAME = "Biratnagar" },
            new Location(){LOCATIONID = 16, LOCATIONNAME = "Pachthar" },
            new Location(){LOCATIONID = 17, LOCATIONNAME = "Jhapa" },
        };

        public static List<BarCode> BarCodeList = new List<BarCode>()
        {
            new BarCode(){BCODE = "1234", MCODE = "M0123", DESCA="boots", RATE=123.123, UNIT = "PC" },
            new BarCode(){BCODE = "12333", MCODE = "M0124", DESCA="Bagss", RATE=12.123 , UNIT = "PC" },
            new BarCode(){BCODE = "OSM60108", MCODE = "M0125", DESCA="TEstd", RATE=1232.4 , UNIT = "PC" },
            new BarCode(){BCODE = "134535", MCODE = "M0126", DESCA="Shirt", RATE=124.123, UNIT = "PC" },
            new BarCode(){BCODE = "#A76EE", MCODE = "M0127", DESCA="coats", RATE=9125.13, UNIT = "PC" },
            new BarCode(){BCODE = "25123", MCODE = "M0128", DESCA="pants", RATE=1375.12, UNIT = "PC" },
            new BarCode(){BCODE = "2365123", MCODE = "M0129", DESCA="wersd", RATE=3232.00, UNIT = "PC" },
            new BarCode(){BCODE = "129873486513", MCODE = "M0130", DESCA="hatsd", RATE=923.123, UNIT = "PC" },
            new BarCode(){BCODE = "123344865123", MCODE = "M0131", DESCA="belts", RATE=573.123, UNIT = "PC" },
        };

        public static List<Division> DivisionList = new List<Division>()
        {
            new Division(){ INITIAL="HOF", NAME="KK SuperMart"},
            new Division(){ INITIAL = "BAN", NAME = "New Baneshwor"},
            new Division(){ INITIAL = "PAT", NAME = "Mangal Bazar"},
            new Division(){ INITIAL = "LAG", NAME = "LaganKhel"},
            new Division(){ INITIAL = "BHP", NAME = "Bhaisepati"},
            new Division(){ INITIAL = "BOU", NAME = "Bouddha"},
        };

        public static List<Warehouse> WarehouseList = new List<Warehouse>()
        {
            new Warehouse(){ NAME = "Main Store"},
            new Warehouse(){ NAME = "Baneswor"},
            new Warehouse(){ NAME = "Bouddha"},
            new Warehouse(){ NAME = "Bhaisepati"},
            new Warehouse(){ NAME = "Patan"},
            new Warehouse(){ NAME = "Kapan"},
            new Warehouse(){ NAME = "Chabahil"},
            new Warehouse(){ NAME = "LaganKhel"},

        };      

        public static List<AcList> AcList = new List<AcList>()
        {
            new AcList(){ ACID = "LBG9142V", ACNAME = "SHARE CAPITAL A/C" , ACCODE = "PC03",  PARENT = "LBG0002" , PTYPE = "C" },
            new AcList(){ ACID = "LBG9143V", ACNAME = "SUPPLIERS" , ACCODE = "PC03",  PARENT = "LBG0002" , PTYPE = "C" },
            new AcList(){ ACID = "LBG9144V", ACNAME = "COUNTRY FOODS PVT. LTD." , ACCODE = "PC03",  PARENT = "LBG0002" , PTYPE = "C" },
            new AcList(){ ACID = "LBG9145V", ACNAME = "CLIMAX TRADING PVT. LTD." , ACCODE = "PC03",  PARENT = "LBG0002" , PTYPE = "C" },
            new AcList(){ ACID = "LBG9146V", ACNAME = "ASSETS" , ACCODE = "PC03",  PARENT = "LBG0002" , PTYPE = "C" },
        };

        public static List<OrderProd> OrderProdList = new List<OrderProd>()
        {
            new OrderProd(){ VCHRNO = "PO1" , SUPPLIERCODE = "LBG9142V" },
            new OrderProd(){ VCHRNO = "PO2" , SUPPLIERCODE = "LBG9142V" },
            new OrderProd(){ VCHRNO = "PO4" , SUPPLIERCODE = "LBG9143V" },
            new OrderProd(){ VCHRNO = "PO5" , SUPPLIERCODE = "LBG9144V" },
            new OrderProd(){ VCHRNO = "PO3" , SUPPLIERCODE = "LBG9145V" },
            new OrderProd(){ VCHRNO = "PO6" , SUPPLIERCODE = "LBG9146V" },
            new OrderProd(){ VCHRNO = "PO7" , SUPPLIERCODE = "LBG9145V" },
            new OrderProd(){ VCHRNO = "PO8" , SUPPLIERCODE = "LBG9142V" },
            new OrderProd(){ VCHRNO = "PO9" , SUPPLIERCODE = "LBG9144V" },
        };
    }   
}
