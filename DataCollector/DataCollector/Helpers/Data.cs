using System;
using System.Collections.Generic;
using DataCollectorStandardLibrary.Models;

namespace DataCollector.Helpers
{
    public class Data
    {
        public static Batch SelectedBatch = new Batch();
        public static Session Session = new Session();
        public static string SelectedMenuType = "";
        public static string EntryMode = "";


        public static GrnMain GrnMain = new GrnMain();
        public static LoadGrnCollect GrnMainData = new LoadGrnCollect();
        public static LoadBranchTransfer BranchMainData = new LoadBranchTransfer();

        public static List<LoadBranchTransfer> BranchDataList = new List<LoadBranchTransfer>();
        public static List<GrnEntry> GrnEntryList = new List<GrnEntry>();
        public static List<GrnMain> GrnMainList = new List<GrnMain>();

        public static List<BranchOutDetail> BranchOutDetailList = new List<BranchOutDetail>();
        public static List<BranchOutItem> BranchOutItemList = new List<BranchOutItem>();
        public static BranchOutDetail BranchOutDetail { get; set; }

        public static List<AcList> AcList = new List<AcList>();

        public static List<MasterMenu> MasterMenuList = new List<MasterMenu>()
        {
            
            new MasterMenu(){Index = 1 , Name = "Main Settings", ImageSource = "MainSettings.png" },
            new MasterMenu(){Index = 2 , Name = "Stock Take", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 10 , Name = "Goods Receive", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 13, Name = "Branch Out", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 3 , Name = "Batch",ImageSource = "Batch.png"},
            new MasterMenu(){Index = 4 , Name = "Change Location", ImageSource = "ChangeLocation.png"},
            new MasterMenu(){Index = 5 , Name = "Session Start",  ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 6 , Name = "Session End",ImageSource = "SessionEnd.png"},
            new MasterMenu(){Index = 12, Name = "Select Session", ImageSource = "SessionStart.png"},
            new MasterMenu(){Index = 7 , Name = "Clear Batch", ImageSource = "ClearBatch.png"},
            new MasterMenu(){Index = 8 , Name = "Clear Session", ImageSource = "ClearBatch.png"},
            new MasterMenu(){Index = 9 , Name = "Clear StockTake", ImageSource = "ClearBatch.png"},
            new MasterMenu(){Index = 11, Name = "Clear GrnData", ImageSource = "ClearBatch.png"},
        };
        
        public static List<MasterMenu> GrnMenuList = new List<MasterMenu>()
        {
            new MasterMenu(){Index = 1 , Name = "Manual Quantity Mode",  ImageSource = "MainSettings.png" },
            new MasterMenu(){Index = 2 , Name = "Auto Quantity Mode", ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 3 , Name = "Upload",  ImageSource = "StockTake.png"},
            new MasterMenu(){Index = 4 , Name = "Main Menu",  ImageSource = "Batch.png"},
          
        };     
        
        public static bool AutoModeEnabled = false;

        public static List<StockTake> StockTakeList = new List<StockTake>();

        public static List<Session> SessionList { get; set; }
        public static PickingDetail SelectedPickingDetail { get; set; }
        public static List<StockTakeMax> SelectedStockTakeMaxList { get; set; }
         }
}
