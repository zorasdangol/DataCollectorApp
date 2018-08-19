using DataCollectorStandardLibrary.DataValidationLayer;
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
        #region Batch
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
                Helpers.Data.SelectedBatch = new Batch();
                return Helpers.Data.SelectedBatch;
            }
            catch (Exception e)
            {
                Helpers.Data.SelectedBatch = new Batch();
                return Helpers.Data.SelectedBatch;
            }
        }
        #endregion

        #region Session
        public  static List<Session> LoadSessionList(string DatabaseLocation)
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
                    Helpers.Data.SessionList = new List<Session>();
                    return Helpers.Data.SessionList;
                }
            }
            catch
            {
                Helpers.Data.SessionList = new List<Session>();
                return Helpers.Data.SessionList;
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
                return Helpers.Data.Session;
            }
            catch (Exception e)
            {
                Helpers.Data.Session = new Session();
                return (Helpers.Data.Session);
            }
        }
        #endregion


        #region Grn
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
                    return Helpers.Data.GrnMainList;
                }
            }
            catch (Exception e)
            {
                Helpers.Data.GrnMainList = new List<GrnMain>();
                return Helpers.Data.GrnMainList;
            }
        }


        public static List<GrnProd> LoadGrnEntryList(string DatabaseLocation, GrnMain GrnMain)
        {
            try
            {
                var list = new List<GrnProd>();
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnProd>();
                    var rows = conn.Table<GrnProd>().ToList();
                    if (rows.Count > 0)
                    {
                        list  = rows.Where(x => ((x.division == GrnMain.division) && (x.vchrNo == GrnMain.vchrNo) )).ToList();
                        
                        if(list == null)
                        {
                            Helpers.Data.GrnEntryList = new List<GrnProd>();
                        }
                        else
                        {
                            Helpers.Data.GrnEntryList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return Helpers.Data.GrnEntryList;
                    }
                }
                Helpers.Data.GrnEntryList = new List<GrnProd>(); 
                return Helpers.Data.GrnEntryList;
            }
            catch (Exception e)
            {
                Helpers.Data.GrnEntryList = new List<GrnProd>(); 
                return Helpers.Data.GrnEntryList;
            }
        }

        #endregion

        #region BranchOut     

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
            catch (Exception e)
            {
                Helpers.Data.BranchOutDetailList = new List<BranchOutDetail>();
                return Helpers.Data.BranchOutDetailList;
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
                        return Helpers.Data.BranchOutItemList;
                    }
                }
                Helpers.Data.BranchOutItemList = new List<BranchOutItem>();
                return Helpers.Data.BranchOutItemList;
            }
            catch (Exception e)
            {
                Helpers.Data.BranchOutItemList = new List<BranchOutItem>();
                return Helpers.Data.BranchOutItemList;
            }
        }
        #endregion


        #region BranchIn

        public static List<BranchInDetail> LoadBranchInDetailList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInDetail>();
                    var rows = conn.Table<BranchInDetail>().ToList();

                    if (rows.Count > 0)
                    {
                        Helpers.Data.BranchInDetailList = rows;
                        return rows;
                    }
                    Helpers.Data.BranchInDetailList = new List<BranchInDetail>();
                    return Helpers.Data.BranchInDetailList;
                }
            }
            catch (Exception e)
            {
                Helpers.Data.BranchInDetailList = new List<BranchInDetail>();
                return Helpers.Data.BranchInDetailList;
            }
        }


        public static List<BranchOutItem> LoadSendBranchOutList(string DatabaseLocation, BranchInDetail BranchInDetail)
        {
            try
            {
                //var list = new List<BranchOutItem>();
                //using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                //{
                //    //conn.CreateTable<BranchOutItem>();
                //    //var rows = conn.Table<BranchOutItem>().ToList();

                //    //var rows = Helpers.JsonData.ReceivedBranchOutList;

                //    if (rows.Count > 0)
                //    {
                //        list = rows.Where(x => ((x.division == BranchInDetail.divisionFrom) && (x.divisionTo == BranchInDetail.division) && (x.vchrNo == BranchInDetail.billToAdd.ToUpper()))).ToList();

                //        if (list == null)
                //        {
                //            Helpers.Data.SendBranchOutSummaryList = new List<BranchInSummary>();
                //        }
                //        else
                //        {
                //            //Helpers.Data.ReceiveItemSummaryList = BranchInDetailValidator.ConvertToReceiveItemSummary(list);
                //            var receivelist = Helpers.Data.SendBranchOutList = list;
                //        }
                //        return Helpers.Data.SendBranchOutList;
                //    }
                //}
                Helpers.Data.SendBranchOutList = new List<BranchOutItem>();
                return Helpers.Data.SendBranchOutList;
            }
            catch (Exception e)
            {
                Helpers.Data.SendBranchOutList = new List<BranchOutItem>();
                return Helpers.Data.SendBranchOutList;
            }
        }

        public static List<BranchInItem> LoadBranchInItemList(string DatabaseLocation, BranchInDetail BranchInDetail)
        {
            try
            {
                var list = new List<BranchInItem>();
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInItem>();
                    var rows = conn.Table<BranchInItem>().ToList();
                    if (rows.Count > 0)
                    {
                        list = rows.Where(x => ((x.division == BranchInDetail.division) && (x.vchrNo == BranchInDetail.vchrNo))).ToList();

                        if (list == null)
                        {
                            Helpers.Data.BranchInItemList = new List<BranchInItem>();
                        }
                        else
                        {
                            Helpers.Data.BranchInItemList = list.OrderByDescending(x => x.ind).ToList(); ;
                        }
                        return Helpers.Data.BranchInItemList;
                    }
                }
                Helpers.Data.BranchInItemList = new List<BranchInItem>();
                return Helpers.Data.BranchInItemList;
            }
            catch (Exception e)
            {
                Helpers.Data.BranchInItemList = new List<BranchInItem>();
                return Helpers.Data.BranchInItemList;
            }
        }

        #endregion

        #region StockTake
        public static List<StockTake> LoadStockTakeList(string DatabaseLocation)
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
                    else if (rows == null)
                    {
                        Helpers.Data.StockTakeList = new List<StockTake>();
                        return Helpers.Data.StockTakeList;
                    }
                }

                Helpers.Data.StockTakeList = new List<StockTake>();
                return Helpers.Data.StockTakeList;
            }
            catch (Exception e)
            {
                Helpers.Data.StockTakeList = new List<StockTake>();
                return (Helpers.Data.StockTakeList);
            }
        }

        #endregion






    }
}
