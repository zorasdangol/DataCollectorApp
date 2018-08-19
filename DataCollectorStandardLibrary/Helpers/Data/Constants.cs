using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollectorStandardLibrary.Helpers.Data
{
    public class Constants
    {
        public static string ipAddress = "192.168.1.105";
        public static string MainURL = "http://" + ipAddress + "/DC/api/";

        public static string WarehouseListURL = "getwarehouse?";
        public static string DivisionListURL =  "getdivision?";
        public static string BarcodeListURL = "getbarcode?settingIndex=1";
        public static string AcListURL = "getAcList?";
        public static string LocationListURL = "getlocation?";
        public static string MenuItemsListURL = "getMenuItem?settingIndex=1";
        public static string OrderProdListURL = "getOrderProd?settingIndex=1";

        public static string SaveDataCollectURL = "saveDataCollect";
        public static string SaveGrnDataURL = "saveGrnData";
        public static string SaveBoutCollectURL = "saveDataCollect";


        //public static string SaveStockTakeSyncDataURL = "SaveStockTakeSync";
        public static string SaveGrnSyncDataURL =  "SaveGrnSync";
        public static string SaveBranchOutSyncDataURL =  "SaveBranchOutSync";
        public static string SaveBranchInSyncDataURL = "SaveBranchInSync";

        public static string getTransferItem = "getbranchInProd";

        public static void SetMainURL(string ipAddress)
        {
            MainURL = "http://" + ipAddress + "/DC/api/" ;
        }

        public static string SetApiURL(string apiUrl)
        {
            return (MainURL + apiUrl);
        }

    }
}
