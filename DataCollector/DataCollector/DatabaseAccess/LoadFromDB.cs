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


        //public static List<LoadGrnCollect> LoadGrnDataList(string DatabaseLocation, GrnMain GrnMain)
        //{
        //    try
        //    {
        //        var list = new List<LoadGrnCollect>();
        //        using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
        //        {
        //            conn.CreateTable<LoadGrnCollect>();
        //            var rows = conn.Table<LoadGrnCollect>().ToList();
        //            if (rows.Count > 0)
        //            {
        //                list = Helpers.Data.GrnDataList = rows.Where(x => ((x.division == GrnMain.division) && (x.vchrNo == GrnMain.vchrNo))).ToList();
        //                return list;
        //            }
        //        }
        //        Helpers.Data.GrnDataList = null;
        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        Helpers.Data.GrnDataList = null;
        //        return null;
        //    }
        //}


        public static List<GrnEntry> LoadGrnDataList(string DatabaseLocation, GrnMain GrnMain)
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
                            Helpers.Data.GrnDataList = new List<GrnEntry>();
                        }
                        else
                        {
                            Helpers.Data.GrnDataList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return list;
                    }
                }
                Helpers.Data.GrnDataList = new List<GrnEntry>(); ;
                return null;
            }
            catch (Exception e)
            {
                Helpers.Data.GrnDataList = new List<GrnEntry>(); ;
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
                        return list;                     
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
                                Helpers.Data.Session = session;
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

        public static List<GrnMain> LoadGrnMainList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnMain>();
                    var rows = conn.Table<GrnMain>().ToList();

                    if (rows.Count > 0)
                    {
                        Helpers.Data.GrnMainList = rows;
                        return rows;
                    }
                    Helpers.Data.GrnMainList = new List<GrnMain>();
                    return null;
                }
            }
            catch (Exception e) { Helpers.Data.GrnMainList = new List<GrnMain>(); return null; }
        }


    }
}
