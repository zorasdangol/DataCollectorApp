using DataCollectorStandardLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.DatabaseAccess
{
    public class ClearFromDB
    {

        public static void ClearBatchAll(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Batch>();
                    var res = conn.DeleteAll<Batch>();
                    conn.CreateTable<Session>();
                    var res1 = conn.DeleteAll<Session>();
                    if (res > 0 && res1 > 0)
                    {
                        Helpers.Data.SelectedBatch = null;
                        Helpers.Data.Session = null;
                    }
                }
            }
            catch (Exception e)
            { }

        }

        public static void ClearSessionAll(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Session>();
                    var res = conn.DeleteAll<Session>();
                    if (res > 0) { Helpers.Data.Session = null; }
                }
            }
            catch (Exception e) { }

        }

        public static void ClearStockTakeAll(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<StockTake>();
                    var res = conn.DeleteAll<StockTake>();
                    if (res > 0) { }
                }
            }
            catch (Exception e) { }

        }

        public static void ClearGrnDataList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<LoadGrnCollect>();
                    var res = conn.DeleteAll<LoadGrnCollect>();

                    conn.CreateTable<GrnMain>();
                    conn.DeleteAll<GrnMain>();

                    conn.CreateTable<GrnEntry>();
                    conn.DeleteAll<GrnEntry>();

                    if (res > 0) { }
                }
            }
            catch (Exception e) { }

        }

        public static void ClearBranchOutList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutDetail>();
                    var res = conn.DeleteAll<BranchOutDetail>();

                    conn.CreateTable<BranchOutItem>();
                    conn.DeleteAll<BranchOutItem>();
                    
                    if (res > 0) { }
                }
            }
            catch (Exception e) { }

        }

        public static void ClearBranchInList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInDetail>();
                    var res = conn.DeleteAll<BranchInDetail>();

                    conn.CreateTable<BranchInItem>();
                    conn.DeleteAll<BranchInItem>();

                    if (res > 0) { }
                }
            }
            catch (Exception e) { }

        }


        public static bool DeleteGrnData(string DatabaseLocation, GrnEntry GrnData)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnEntry>();
                    var rows = conn.Table<GrnEntry>().ToList().Where(x => ((x.ind == GrnData.ind) && (x.division == GrnData.division) && (x.vchrNo == GrnData.vchrNo) )).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from GrnEntry where division = '" + GrnData.division + "' and vchrNo = '" + GrnData.vchrNo + "' and ind = " + GrnData.ind );
                       
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

        public static bool DeleteStockTake(string DatabaseLocation, StockTake StockTake)
        {
            try
            {
                var s = StockTake;
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<StockTake>();

                    var rows = conn.Table<StockTake>().ToList().Where(x => ((x.ind == StockTake.ind) && (x.SESSIONID == StockTake.SESSIONID) && (x.BATCHNO == StockTake.BATCHNO))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from StockTake where BATCHNO = '" + StockTake.BATCHNO + "' and SESSIONID = " + StockTake.SESSIONID + " and ind = " + StockTake.ind);

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

        public static bool DeleteBranchOutItem(string DatabaseLocation, BranchOutItem BranchOutItem)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutItem>();
                    var rows = conn.Table<BranchOutItem>().ToList().Where(x => ((x.ind == BranchOutItem.ind) && (x.division == BranchOutItem.division) && (x.vchrNo == BranchOutItem.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchOutItem  where division = '" + BranchOutItem.division + "' and vchrNo = '" + BranchOutItem.vchrNo + "' and ind = " + BranchOutItem.ind);

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


        public static bool DeleteBranchInItem(string DatabaseLocation, BranchInItem BranchInItem)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInItem>();
                    var rows = conn.Table<BranchInItem>().ToList().Where(x => ((x.ind == BranchInItem.ind) && (x.division == BranchInItem.division) && (x.vchrNo == BranchInItem.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchInItem  where division = '" + BranchInItem.division + "' and vchrNo = '" + BranchInItem.vchrNo + "' and ind = " + BranchInItem.ind);

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
