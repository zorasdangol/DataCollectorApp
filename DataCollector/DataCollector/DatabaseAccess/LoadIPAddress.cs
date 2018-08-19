using DataCollectorStandardLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollectorStandardLibrary.Helpers.Data;
using Xamarin.Forms;
using DataCollector.Interfaces;

namespace DataCollector.DatabaseAccess
{
    public class LoadIPAddress
    {
        public static void InitialIPAddress(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<API>();
                    var apiRow = conn.Table<API>().ToList();
                    if (apiRow.Count > 0)
                    {
                        Constants.ipAddress = apiRow.FirstOrDefault().IPAddress;
                        Constants.SetMainURL(Constants.ipAddress);                        
                    }
                    else
                    {
                        API obj = new API { IPAddress = Constants.ipAddress };
                        conn.Insert(obj);
                    }
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
            }
        }

        public static bool SetIPAddress(string DatabaseLocation, string IPAddress)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<API>();
                    API api = new API
                    {
                        IPAddress = IPAddress
                    };
                    conn.DeleteAll<API>();
                    int rows = conn.Insert(api);
                    return true;
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(e.Message);
                return false;
            }
        }
    }
}
