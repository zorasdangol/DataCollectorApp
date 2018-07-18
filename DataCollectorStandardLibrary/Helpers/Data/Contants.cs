using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Helpers.Data
{
    public class Contants
    {
        public static string MainURL = "http://192.168.1.112/DC/api/";

        public static string WarehouseListURL = MainURL + "getwarehouse?";
        public static string DivisionListURL = MainURL + "getdivision?";
        public static string BarcodeListURL = MainURL + "getbarcode?settingIndex=1";
        public static string AcListURL = MainURL + "getAcList?";
        public static string LocationListURL = MainURL + "getlocation?";
        public static string MenuItemsListURL = MainURL + "getMenuItem?settingIndex=1";
        public static string OrderProdListURL = MainURL + "getOrderProd?settingIndex=1";

        public static string SaveDataCollectURL = MainURL + "saveDataCollect";
        public static string SaveGrnDataURL = MainURL + "saveGrnData";
        public static string SaveBoutCollectURL = MainURL + "saveDataCollect";
    }
}
