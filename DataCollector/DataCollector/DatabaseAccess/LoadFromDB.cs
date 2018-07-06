using DataCollectorStandardLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.DatabaseAccess
{
    public class LoadFromDB
    {
        public static Batch LoadBatch(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Batch>();
                    //conn.DeleteAll<Batch>();
                    var rows = conn.Table<Batch>().ToList();
                    if (rows.Count > 0)
                    {
                        foreach (var batch in rows)
                        {
                            if (batch.STATUS == 0)
                            {
                                Helpers.Data.SelectedBatch = new Batch(batch);
                                return batch;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return (null);
            }
        }

        internal static List<Session> LoadSessionList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Session>();
                   
                    var rows = conn.Table<Session>().ToList();
                    if (rows.Count > 0)
                    {
                        Helpers.Data.SessionList = rows;
                        return rows;
                    }
                    return new List<Session>();
                    
                }
            }
            catch { return new List<Session>(); }
        }     

        public static List<GrnEntry> LoadGrnEntryList(string DatabaseLocation, GrnMain GrnMain)
        {
            try
            {
                var list = new List<GrnEntry>();
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnEntry>();
                    var rows = conn.Table<GrnEntry>().ToList();
                    if (rows.Count > 0)
                    {
                        list  = rows.Where(x => ((x.division == GrnMain.division) && (x.vchrNo == GrnMain.vchrNo) )).ToList();
                        
                        if(list == null)
                        {
                            Helpers.Data.GrnEntryList = new List<GrnEntry>();
                        }
                        else
                        {
                            Helpers.Data.GrnEntryList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return list;
                    }
                }
                Helpers.Data.GrnEntryList = new List<GrnEntry>(); 
                return null;
            }
            catch (Exception e)
            {
                Helpers.Data.GrnEntryList = new List<GrnEntry>(); 
                return null;
            }
        }

        public static List<BranchOutItem> LoadBranchOutItemList(string DatabaseLocation, BranchOutDetail BranchOutDetail)
        {
            try
            {
                var list = new List<BranchOutItem>();
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutItem>();
                    var rows = conn.Table<BranchOutItem>().ToList();
                    if (rows.Count > 0)
                    {
                        list = rows.Where(x => ((x.division == BranchOutDetail.division) && (x.vchrNo == BranchOutDetail.vchrNo))).ToList();

                        if (list == null)
                        {
                            Helpers.Data.BranchOutItemList = new List<BranchOutItem>();
                        }
                        else
                        {
                            Helpers.Data.BranchOutItemList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return list;
                    }
                }
                Helpers.Data.BranchOutItemList = new List<BranchOutItem>();
                return null;
            }
            catch (Exception e)
            {
                Helpers.Data.BranchOutItemList = new List<BranchOutItem>();
                return null;
            }
        }


        public static List<StockTake> LoadStockTake(string DatabaseLocation)
        {
            try
            {
                var list = new List<StockTake>();
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<StockTake>();
                    var rows = conn.Table<StockTake>().ToList();
                    var batch = Helpers.Data.SelectedBatch;
                    var session = Helpers.Data.Session;
                    if (rows.Count > 0)
                    {
                        list = rows.Where(x => ((x.BATCHNO == Helpers.Data.SelectedBatch.BATCHNO) && (x.SESSIONID == Helpers.Data.Session.SESSIONID))).ToList();

                        if (list == null)
                        {
                            Helpers.Data.StockTakeList = new List<StockTake>();
                        }
                        else
                        {
                            Helpers.Data.StockTakeList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return Helpers.Data.StockTakeList;                     
                    }
                    else if(rows == null)
                    {
                        Helpers.Data.StockTakeList = new List<StockTake>();
                        return null;
                    }
                }

                Helpers.Data.StockTakeList = new List<StockTake>();
                return null;
            }
            catch (Exception e)
            {
                Helpers.Data.StockTakeList = new List<StockTake>();
                return (null);
            }
        }


        public static Session LoadSession(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Session>();
                    //conn.DeleteAll<Session>();
                    var rows = conn.Table<Session>().ToList();

                    if (rows.Count > 0)
                    {
                        foreach (var session in rows)
                        {
                            if (session.STATUS == 0)
                            {
                                Helpers.Data.Session = new Session(session);
                                return session;
                            }
                        }
                    }
                }
                Helpers.Data.Session = new Session();
                return null;
            }
            catch (Exception e)
            {
                Helpers.Data.Session = new Session();
                return (null);
            }
        }

        public static List<BranchOutDetail> LoadBranchOutDetailList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutDetail>();
                    var rows = conn.Table<BranchOutDetail>().ToList();

                    if (rows.Count > 0)
                    {
                        Helpers.Data.BranchOutDetailList = rows;
                        return rows;
                    }
                    Helpers.Data.BranchOutDetailList = new List<BranchOutDetail>();
                    return Helpers.Data.BranchOutDetailList;
                }
            }
            catch (Exception e) { Helpers.Data.BranchOutDetailList = new List<BranchOutDetail>(); return Helpers.Data.BranchOutDetailList; }
        }


    }
}
