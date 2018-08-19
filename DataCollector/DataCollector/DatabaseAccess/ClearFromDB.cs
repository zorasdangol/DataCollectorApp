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

                    conn.CreateTable<GrnProd>();
                    conn.DeleteAll<GrnProd>();

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

        public static bool UpdateSavedGrnMain(string DatabaseLocation, GrnMain GrnMain)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnMain>();
                    var rows = conn.Table<GrnMain>().ToList().Where(x => ( (x.division == GrnMain.division) && (x.vchrNo == GrnMain.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from GrnMain where division = '" + GrnMain.division + "' and vchrNo = '" + GrnMain.vchrNo + "'");
                        conn.Insert(GrnMain);
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

        public static bool UpdateSavedStockTake(string DatabaseLocation, StockTake LoadDataCollect)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<StockTake>();
                    var rows = conn.Table<StockTake>().ToList().Where(x => ((x.division == LoadDataCollect.division) && (x.sid == LoadDataCollect.sid))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from StockTake where division = '" + LoadDataCollect.division + "' and sid = '" + LoadDataCollect.sid + "' and curNo = '" + LoadDataCollect.curNo + "'" );
                        conn.Insert(LoadDataCollect);
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

        public static bool UpdateSavedBranchOutMain(string DatabaseLocation, BranchOutDetail BranchOutMain)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutDetail>();
                    var rows = conn.Table<BranchOutDetail>().ToList().Where(x => ((x.division == BranchOutMain.division) && (x.vchrNo == BranchOutMain.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchOutDetail where division = '" + BranchOutMain.division + "' and vchrNo = '" + BranchOutMain.vchrNo + "'");
                        conn.Insert(BranchOutMain);
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
        
        public static bool UpdateSavedBranchInMain(string DatabaseLocation, BranchInDetail BranchInMain)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInDetail>();
                    var rows = conn.Table<BranchInDetail>().ToList().Where(x => ((x.division == BranchInMain.division) && (x.vchrNo == BranchInMain.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchInDetail where division = '" + BranchInMain.division + "' and vchrNo = '" + BranchInMain.vchrNo + "'");
                        conn.Insert(BranchInMain);
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

        public static bool DeleteGrnMain(string DatabaseLocation, GrnMain GrnMain)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnMain>();
                    conn.CreateTable<GrnProd>();
                    var rows = conn.Table<GrnMain>().ToList().Where(x => ((x.division == GrnMain.division) && (x.vchrNo == GrnMain.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from GrnMain where division = '" + GrnMain.division + "' and vchrNo = '" + GrnMain.vchrNo + "'");
                        conn.Execute("Delete from GrnProd where division = '" + GrnMain.division + "' and vchrNo = '" + GrnMain.vchrNo + "'"); 
                        //conn.Insert(GrnMain);
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

        public static bool DeleteGrnData(string DatabaseLocation, GrnProd GrnData)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnProd>();
                    var rows = conn.Table<GrnProd>().ToList().Where(x => ((x.ind == GrnData.ind) && (x.division == GrnData.division) && (x.vchrNo == GrnData.vchrNo) )).FirstOrDefault();

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
                    if (string.IsNullOrEmpty(StockTake.MCODE))
                    {
                        var rows = conn.Table<StockTake>().ToList().Where(x => ((x.sid == StockTake.sid) && (x.division == StockTake.division))).FirstOrDefault();

                        if (rows != null)
                        {
                            conn.Execute("Delete  from StockTake where sid = '" + StockTake.sid + "' and division = '" + StockTake.division + "'");

                            return true;
                        }
                    }
                    else
                    {
                        var rows = conn.Table<StockTake>().ToList().Where(x => ((x.sid == StockTake.sid) && (x.division == StockTake.division) && (x.curNo == StockTake.curNo))).FirstOrDefault();

                        if (rows != null)
                        {
                            conn.Execute("Delete  from StockTake where sid = '" + StockTake.sid + "' and division = '" + StockTake.division + "' and curNo = " + StockTake.curNo);

                            return true;
                        }

                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public static bool DeleteBranchOutDetail(string DatabaseLocation, BranchOutDetail BranchOutDetail)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchOutDetail>();
                    conn.CreateTable<BranchOutItem>();
                    var rows = conn.Table<BranchOutDetail>().ToList().Where(x => ((x.division == BranchOutDetail.division) && (x.vchrNo == BranchOutDetail.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchOutDetail where division = '" + BranchOutDetail.division + "' and vchrNo = '" + BranchOutDetail.vchrNo + "'");
                        conn.Execute("Delete from BranchOutItem where division = '" + BranchOutDetail.division + "' and vchrNo = '" + BranchOutDetail.vchrNo + "'");
                        //conn.Insert(GrnMain);
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

        public static bool DeleteBranchInDetail(string DatabaseLocation, BranchInDetail BranchInDetail)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BranchInDetail>();
                    conn.CreateTable<BranchInItem>();
                    var rows = conn.Table<BranchInDetail>().ToList().Where(x => ((x.division == BranchInDetail.division) && (x.vchrNo == BranchInDetail.vchrNo))).FirstOrDefault();

                    if (rows != null)
                    {
                        conn.Execute("Delete  from BranchInDetail where division = '" + BranchInDetail.division + "' and vchrNo = '" + BranchInDetail.vchrNo + "'");
                        conn.Execute("Delete from BranchInItem where division = '" + BranchInDetail.division + "' and vchrNo = '" + BranchInDetail.vchrNo + "'");
                       
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
