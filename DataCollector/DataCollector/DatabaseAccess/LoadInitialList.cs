using DataCollectorStandardLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.DatabaseAccess
{
    public class LoadInitialList
    {
        public LoadInitialList(string DatabaseLocation)
        {
            try
            {
                LoadLocationList(DatabaseLocation);
                LoadDivisionList(DatabaseLocation);
                LoadAcList(DatabaseLocation);
                LoadMenuItemsList(DatabaseLocation);
                LoadBarCodeList(DatabaseLocation);
                LoadWarehouseList(DatabaseLocation);
                LoadOrderProdList(DatabaseLocation);
            }
            catch { }
        }

        #region locationlist
        public static List<Location> LoadLocationList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Location>();
                    var rows = conn.Table<Location>().ToList();
                    if(rows == null)
                    {
                        Helpers.Data.LocationList = new List<Location>();
                    }
                    else 
                    {
                        Helpers.Data.LocationList = rows;
                    }                   
                }
                return Helpers.Data.LocationList;
            }
            catch (Exception e)
            {
                Helpers.Data.LocationList = new List<Location>();
                return Helpers.Data.LocationList;
            }
        }
        #endregion

        #region Divisionlist
        public static List<Division> LoadDivisionList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Division>();
                    var rows = conn.Table<Division>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.DivisionList = new List<Division>();
                    }
                    else
                    {
                        Helpers.Data.DivisionList = rows;
                    }
                }
                return Helpers.Data.DivisionList;
            }
            catch (Exception e)
            {
                Helpers.Data.DivisionList = new List<Division>();
                return Helpers.Data.DivisionList;
            }
        }
        #endregion

        #region BarCodelist
        public static List<BarCode> LoadBarCodeList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<BarCode>();
                    var rows = conn.Table<BarCode>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.BarCodeList = new List<BarCode>();
                    }
                    else
                    {
                        Helpers.Data.BarCodeList = rows;
                    }
                }
                return Helpers.Data.BarCodeList;
            }
            catch (Exception e)
            {
                Helpers.Data.BarCodeList = new List<BarCode>();
                return Helpers.Data.BarCodeList;
            }
        }
        #endregion
        
        #region Warehouselist
        public static List<Warehouse> LoadWarehouseList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<Warehouse>();
                    var rows = conn.Table<Warehouse>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.WarehouseList = new List<Warehouse>();
                    }
                    else
                    {
                        Helpers.Data.WarehouseList = rows;
                    }
                }
                return Helpers.Data.WarehouseList;
            }
            catch (Exception e)
            {
                Helpers.Data.WarehouseList = new List<Warehouse>();
                return Helpers.Data.WarehouseList;
            }
        }
        #endregion

        #region MenuItemslist
        public static List<MenuItem> LoadMenuItemsList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<MenuItem>();
                    var rows = conn.Table<MenuItem>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.MenuItemsList = new List<MenuItem>();
                    }
                    else
                    {
                        Helpers.Data.MenuItemsList = rows;
                    }
                }
                return Helpers.Data.MenuItemsList;
            }
            catch (Exception e)
            {
                Helpers.Data.MenuItemsList = new List<MenuItem>();
                return Helpers.Data.MenuItemsList;
            }
        }
        #endregion
        
        #region AcList
        public static List<AcList> LoadAcList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<AcList>();
                    var rows = conn.Table<AcList>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.AcList = new List<AcList>();
                    }
                    else
                    {
                        Helpers.Data.AcList = rows;
                    }
                }
                return Helpers.Data.AcList;
            }
            catch (Exception e)
            {
                Helpers.Data.AcList = new List<AcList>();
                return Helpers.Data.AcList;
            }
        }
        #endregion

        #region OrderProdList
        public static List<OrderProd> LoadOrderProdList(string DatabaseLocation)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
                {
                    conn.CreateTable<OrderProd>();
                    var rows = conn.Table<OrderProd>().ToList();
                    if (rows == null)
                    {
                        Helpers.Data.OrderProdList = new List<OrderProd>();
                    }
                    else
                    {
                        Helpers.Data.OrderProdList = rows;
                    }
                }
                return Helpers.Data.OrderProdList;
            }
            catch (Exception e)
            {
                Helpers.Data.OrderProdList = new List<OrderProd>();
                return Helpers.Data.OrderProdList;
            }
        }
        #endregion
    }
}
