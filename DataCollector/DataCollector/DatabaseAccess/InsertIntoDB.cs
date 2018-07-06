using DataCollectorStandardLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.DatabaseAccess
{
    public class InsertIntoDB
    {
        public static bool InsertBatch(string DatabaseLocation, Batch SelectedBatch)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Batch>();
                    conn.DeleteAll<Batch>();
                    conn.Insert(SelectedBatch);
                    Helpers.Data.SelectedBatch = new Batch(SelectedBatch);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool InsertStockTake(string DatabaseLocation, StockTake StockTake)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<StockTake>();
                    
                    //var rows = conn.Table<GrnEntry>().ToList().Where(x => x.ind == StockTake.ind).FirstOrDefault();

                    if (Helpers.Data.StockTakeList == null)
                    {
                        StockTake.ind = 1;
                    }
                    else
                    {
                        var list = Helpers.Data.StockTakeList;
                        var maxCountData = Helpers.Data.StockTakeList.OrderByDescending(item => item.ind).FirstOrDefault();

                        if (maxCountData == null)
                            StockTake.ind = 1;
                        else
                            StockTake.ind = maxCountData.ind + 1;
                    }

                    var inp = conn.Insert(StockTake);
                    if (inp == 1)
                    {
                        var newData = new StockTake();
                        newData.SetStockTake(StockTake);
                        Helpers.Data.StockTakeList.Insert(0, newData);

                        var list = Helpers.Data.StockTakeList;
                        return true;
                    }
                    return false;
                }

            }
            catch (Exception e) { return false; }
        }
        
        public static bool InsertSession(string DatabaseLocation, Session Session)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Session>();
                    var rows = conn.Table<Session>().ToList();
                    Session.SESSIONID = rows.Count + 1;
                    var inp = conn.Insert(Session);
                    if (inp == 1) { Helpers.Data.Session = Session; return true; }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void InsertGrnMain(string DatabaseLocation, GrnMain grnMain)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnMain>();
                    conn.CreateTable<GrnEntry>();
                    if (Helpers.Data.EntryMode == "New")
                    {
                        conn.Insert(grnMain);
                    }
                    else if (Helpers.Data.EntryMode == "Edit")
                    {
                        Helpers.Data.GrnEntryList = conn.Query<GrnEntry>("Select * from GrnEntry where division = '" + grnMain.division + "' and vchrNo = '" + grnMain.vchrNo + "'");
                        
                        conn.Execute("Delete  from grnMain where division = '" + grnMain.division + "' and vchrNo = '" + grnMain.vchrNo + "'");
                        conn.Insert(grnMain);
                    }                              
                    var rows = conn.Table<GrnMain>().ToList();
                }
            }
            catch (Exception e)
            {
            }
        }

        public static bool InsertGrnEntry(string DatabaseLocation, GrnEntry GrnEntry)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnEntry>();
                    //var rows = conn.Table<GrnEntry>().ToList().Where(x => x.ind == GrnData.ind).FirstOrDefault();
                    if(Helpers.Data.GrnEntryList == null)
                    {
                        GrnEntry.ind = 1;
                    }
                    else
                    {
                        var list = Helpers.Data.GrnEntryList;
                        var maxCountData = Helpers.Data.GrnEntryList.OrderByDescending(item => item.ind).FirstOrDefault();

                        if (maxCountData == null)
                            GrnEntry.ind = 1;
                        else
                            GrnEntry.ind = maxCountData.ind + 1;
                    }                    
                    var inp = conn.Insert(GrnEntry);
                    if (inp == 1)
                    {
                        var newData = new GrnEntry();
                        newData.SetGrnEntry(GrnEntry);
                        Helpers.Data.GrnEntryList.Insert(0,newData);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static void InsertBranchOutDetail(string DatabaseLocation, BranchOutDetail BranchOutDetail)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutDetail>();
                    conn.CreateTable<BranchOutItem>();
                    if (Helpers.Data.EntryMode == "New")
                    {
                        conn.Insert(BranchOutDetail);
                    }
                    else if (Helpers.Data.EntryMode == "Edit")
                    {
                        Helpers.Data.BranchOutItemList = conn.Query<BranchOutItem>("Select * from BranchOutItem where division = '" + BranchOutDetail.division + "' and vchrNo = '" + BranchOutDetail.vchrNo + "'");
                        
                        conn.Execute("Delete  from BranchOutDetail where division = '" + BranchOutDetail.division + "' and vchrNo = '" + BranchOutDetail.vchrNo + "'");
                        conn.Insert(BranchOutDetail);
                    }
                    //var rows = conn.Table<BranchOutDetail>().ToList();
                }
            }
            catch (Exception e)
            {
            }
        }

        public static bool InsertBranchOutItem(string DatabaseLocation, BranchOutItem BranchOutItem)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutItem>();
                    //var rows = conn.Table<GrnEntry>().ToList().Where(x => x.ind == GrnData.ind).FirstOrDefault();
                    if (Helpers.Data.BranchOutItemList == null)
                    {
                        BranchOutItem.ind = 1;
                    }
                    else
                    {
                        var list = Helpers.Data.BranchOutItemList;
                        var maxCountData = Helpers.Data.BranchOutItemList.OrderByDescending(item => item.ind).FirstOrDefault();

                        if (maxCountData == null)
                            BranchOutItem.ind = 1;
                        else
                            BranchOutItem.ind = maxCountData.ind + 1;
                    }
                    var inp = conn.Insert(BranchOutItem);
                    if (inp == 1)
                    {
                        var newData = new BranchOutItem();
                        newData.SetBranchOutItem(BranchOutItem);
                        Helpers.Data.BranchOutItemList.Insert(0, newData);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
