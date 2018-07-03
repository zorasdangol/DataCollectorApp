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
                    conn.CreateTable<LoadGrnCollect>();
                    if (Helpers.Data.EntryMode == "New")
                    {
                        conn.Insert(grnMain);
                    }
                    else if (Helpers.Data.EntryMode == "Edit")
                    {
                        Helpers.Data.GrnDataList = conn.Query<GrnEntry>("Select * from LoadGrnCollect where division = '" + grnMain.division + "' and vchrNo = '" + grnMain.vchrNo + "'");
                        //foreach (var grn in Helpers.Data.GrnDataList)
                        //{
                        //    grn.vchrNo = grnMain.vchrNo;
                        //    grn.division = grnMain.division;
                        //    grn.chalanNo = grnMain.chalanNo;
                        //    grn.trnDate = grnMain.trnDate;
                        //    grn.trnAc = grnMain.trnAc;
                        //    grn.ParAc = grnMain.ParAc;
                        //    grn.trnMode = grnMain.trnMode;
                        //    grn.refOrdBill = grnMain.refOrdBill;
                        //    grn.remarks = grnMain.remarks;
                        //    grn.wareHouse = grnMain.wareHouse;
                        //    grn.isTaxInvoice = grnMain.isTaxInvoice;
                        //}
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

        //public static bool InsertGrnData(string DatabaseLocation, LoadGrnCollect GrnData)
        //{
        //    try
        //    {
        //        using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
        //        {
        //            conn.CreateTable<LoadGrnCollect>();
        //            var rows = conn.Table<LoadGrnCollect>().ToList();
        //            var inp = conn.Insert(GrnData);
        //            if (inp == 1) { return true; }
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        public static bool InsertGrnData(string DatabaseLocation, GrnEntry GrnData)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<GrnEntry>();
                    //var rows = conn.Table<GrnEntry>().ToList().Where(x => x.ind == GrnData.ind).FirstOrDefault();
                    if(Helpers.Data.GrnDataList == null)
                    {
                        GrnData.ind = 1;
                    }
                    else
                    {
                        var list = Helpers.Data.GrnDataList;
                        var maxCountData = Helpers.Data.GrnDataList.OrderByDescending(item => item.ind).FirstOrDefault();

                        if (maxCountData == null)
                            GrnData.ind = 1;
                        else
                            GrnData.ind = maxCountData.ind + 1;
                    }                    
                    var inp = conn.Insert(GrnData);
                    if (inp == 1)
                    {
                        var newData = new GrnEntry();
                        newData.SetGrnEntry(GrnData);
                        Helpers.Data.GrnDataList.Insert(0,newData);
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
