using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollectorStandardLibrary.Models;

namespace DataCollector.DatabaseAccess
{
    public class DBAccess
    {        

        //public static int GRNCount(string DatabaseLocation,string div)
        //{
        //    try
        //    {
        //        using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
        //        {
        //            conn.CreateTable<Sequence>();
        //            var count = conn.Query<int>("Select max(curNo) from Sequence where division= ? ",div).FirstOrDefault();
        //            return count + 1;
        //            //return conn.ExecuteScalar<int>("Select max(RIGHT(vchrNo,LEN(vchrNo)-3)) from LoadGrnCollect where division = '" + div + "'");
        //        }

        //    }catch(Exception e)
        //    { return 0; }          

        //}
               

        public static bool LocationChange(string DatabaseLocation, Batch SelectedBatch)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Batch>();
                    var res = conn.DeleteAll<Batch>();
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

        public static bool EndSession(string DatabaseLocation, Session Session)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Session>();

                    var res = conn.Delete<Session>(Session.SESSIONID);

                    Session.STATUS = 1;
                    var output = conn.Insert(Session);

                    //var output = conn.Execute("UPDATE Session SET STATUS = 1  Where SESSIONID = "+ Session.SESSIONID);
                    //var rows = conn.Table<Session>().ToList();
                   
                    // output = conn.Query<Session>("UPDATE Session SET STATUS = 1 Where SESSIONID = 0", 1, Session.SESSIONID);

                    if (output > 0) { Helpers.Data.Session = null; return true; }

                    return false ;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SelectedSession(string DatabaseLocation, Session Session)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Session>();

                    var res = conn.Delete<Session>(Session.SESSIONID);

                    Session.STATUS = 0;
                    var output = conn.Insert(Session);
                    
                    if (output > 0) { Helpers.Data.Session = Session; return true; }

                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }






        //public static List<LoadGrnCollect> LoadGrnDataList(string DatabaseLocation)
        //{
        //    try
        //    {
        //        using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
        //        {
        //            conn.CreateTable<LoadGrnCollect>();
        //            var rows = conn.Table<LoadGrnCollect>().ToList();

        //            if (rows.Count > 0)
        //            {
        //                Helpers.Data.GrnDataList = rows;
        //                return rows;
        //            }
        //            Helpers.Data.GrnDataList = new List<LoadGrnCollect>();
        //            return null;
        //        }
        //    }
        //    catch (Exception e) { Helpers.Data.GrnDataList = new List<LoadGrnCollect>(); return null; }
        //}




    }
}
