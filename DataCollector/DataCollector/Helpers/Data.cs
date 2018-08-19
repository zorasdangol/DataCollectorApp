using System;
using System.Collections.Generic;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views.MenuPages;

namespace DataCollector.Helpers
{
    public class Data
    {

        public static Batch SelectedBatch = new Batch();
        public static Session Session = new Session();
        public static List<Session> SessionList { get; set; }

        public static StockTake StockTake = new StockTake();
        public static List<StockTake> StockTakeList = new List<StockTake>();

        //public static LoadDataCollect StockTake = new LoadDataCollect();
        //public static List<LoadDataCollect> StockTakeList = new List<LoadDataCollect>();

        public static string SelectedMenuType = "";
        public static string EntryMode = "";
        
        public static GrnMain GrnMain = new GrnMain();
        public static LoadGrnCollect GrnMainData = new LoadGrnCollect();
        public static List<GrnProd> GrnEntryList = new List<GrnProd>();
        public static List<GrnMain> GrnMainList = new List<GrnMain>();

        //public static LoadBranchTransfer BranchMainData = new LoadBranchTransfer();
        //public static List<LoadBranchTransfer> BranchDataList = new List<LoadBranchTransfer>();
      
        public static List<BranchOutDetail> BranchOutDetailList = new List<BranchOutDetail>();
        public static List<BranchOutItem> BranchOutItemList = new List<BranchOutItem>();
        public static BranchOutDetail BranchOutDetail { get; set; }

        public static List<BranchInDetail> BranchInDetailList = new List<BranchInDetail>();
        public static List<BranchInItem> BranchInItemList = new List<BranchInItem>();
        public static BranchInDetail BranchInDetail { get; set; }

        public static List<BranchOutItem> SendBranchOutList = new List<BranchOutItem>();
        public static List<BranchInSummary> SendBranchOutSummaryList = new List<BranchInSummary>();

        #region initialList
        public static List<Division> DivisionList = new List<Division>();
        public static List<Warehouse> WarehouseList = new List<Warehouse>();
        public static List<BarCode> BarCodeList = new List<BarCode>();
        public static List<AcList> AcList = new List<AcList>();
        public static List<OrderProd> OrderProdList = new List<OrderProd>();
        public static List<Location> LocationList = new List<Location>();
        public static List<MenuItem> MenuItemsList = new List<MenuItem>();
        #endregion


        public static List<MasterMenu> MasterMenuList = new List<MasterMenu>()
        {            
            new MasterMenu(){Index = 1 , Name = "Main Settings", ImageSource = "MainSettings.png" },
            new MasterMenu(){Index = 2 , Name = "Stock Take", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 3 , Name = "Goods Receive", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 4, Name = "Branch Out", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 5, Name = "Branch In", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 6 , Name = "Batch",ImageSource = "Batch.png"},
            new MasterMenu(){Index = 7 , Name = "Change Location", ImageSource = "ChangeLocation.png"},
            new MasterMenu(){Index = 8 , Name = "Session Start",  ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 9 , Name = "Session End",ImageSource = "SessionEnd.png"},
            new MasterMenu(){Index = 10, Name = "Select Session", ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 11, Name = "Clear Batch", ImageSource = "ClearBatch.png"},
            new MasterMenu(){Index = 12, Name = "Data Sync", ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 13, Name = "View Data List", ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 14, Name = "Data Download", ImageSource = "SessionStart.png"},

            //new MasterMenu(){Index = 15, Name = "Clear Session", ImageSource = "ClearBatch.png"},
            
            //new MasterMenu(){Index = 9 , Name = "Clear StockTake", ImageSource = "ClearBatch.png"},
           // new MasterMenu(){Index = 11, Name = "Clear GrnData", ImageSource = "ClearBatch.png"},
            //new MasterMenu(){Index = 15, Name = "Clear BranchInData", ImageSource = "ClearBatch.png"},
            //new MasterMenu(){Index = 16, Name = "Clear BranchOutData", ImageSource = "ClearBatch.png"},
        };

        public static List<MasterMenu> TBMenuItems = new List<MasterMenu>()
        {
            new MasterMenu(){Index = 1 , Name = "Main Settings" },
            new MasterMenu(){Index = 2 , Name = "Developer Settings"},
            new MasterMenu(){Index = 3 , Name = "IP Settings"},
        };

        public static List<MasterMenu> GrnMenuList = new List<MasterMenu>()
        {
            new MasterMenu(){Index = 1 , Name = "Manual Quantity Mode",  ImageSource = "MainSettings.png" },
            new MasterMenu(){Index = 2 , Name = "Auto Quantity Mode", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 3 , Name = "Upload",  ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 4 , Name = "Main Menu",  ImageSource = "Batch.png"},          
        };     
        
        public static bool AutoModeEnabled = false;
        public static bool ActivityIndicatorEnabled = false;

        //public static PickingDetail SelectedPickingDetail { get; set; }
        //public static List<StockTakeMax> SelectedStockTakeMaxList { get; set; }
         }
}
