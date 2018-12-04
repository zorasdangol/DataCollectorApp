using DataCollectorStandardLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.DataAccessLayer
{
    public class BaseDataAccess
    {
        public static async Task<FunctionResponse<List<Division>>> getDivisionList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.DivisionListURL);
                //String url = Helpers.Data.Constants.DivisionListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var DivisionList = JsonConvert.DeserializeObject<List<Division>>(json);
                    if(DivisionList == null)
                    {
                        DivisionList = new List<Division>();
                    }
                    return new FunctionResponse<List<Division>>() { status = "ok", result = DivisionList };

                }                
            }
            catch(Exception ex) {
               // return new List<Division>();
               return new FunctionResponse<List<Division>>(){ status = "error", Message = "Division Couldnot Connect to Server" };
            }
        }

        public static async Task<FunctionResponse<List<Warehouse>>> getWarehouseList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.WarehouseListURL);
                //String url = Helpers.Data.Constants.WarehouseListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var warehouseList = JsonConvert.DeserializeObject<List<Warehouse>>(json);
                    if(warehouseList == null)
                    {
                        warehouseList = new List<Warehouse>();
                    }
                    return new FunctionResponse<List<Warehouse>>() { status = "ok", result = warehouseList };
                }
            }
            catch(Exception ex) { return new FunctionResponse<List<Warehouse>>() { status = "error", Message = "WareHouse Couldnot Connect to Server:" + ex.Message }; }
        }

        public static async Task<FunctionResponse<List<BarCode>>> getBarCodeList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.BarcodeListURL);
               // String url = Helpers.Data.Constants.BarcodeListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var barcodeList = JsonConvert.DeserializeObject<List<BarCode>>(json);
                    if(barcodeList == null)
                    {
                        barcodeList = new List<BarCode>();
                    }
                    return new FunctionResponse<List<BarCode>>() { status = "ok", result = barcodeList };
                }
            }
            catch (Exception ex){ return new FunctionResponse<List<BarCode>>() { status = "error", Message = "BarCode Couldnot Connect to Server:"+ ex.Message }; }
        }

        public static async Task<FunctionResponse<List<Location>>> getLocationList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.LocationListURL);
                //String url = Helpers.Data.Constants.LocationListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var LocationList = JsonConvert.DeserializeObject<List<Location>>(json);
                    if(LocationList == null)
                    {
                        LocationList = new List<Location>();
                    }
                    return new FunctionResponse<List<Location>>() { status = "ok", result = LocationList };
                }
            }
            catch (Exception ex){ return new FunctionResponse<List<Location>>() { status = "error", Message = "Loaction Couldnot Connect to Server:"+ ex.Message }; }
        }

        public static async Task<FunctionResponse<List<AcList>>> getAcList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.AcListURL);
                //String url = Helpers.Data.Constants.AcListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var AcList = JsonConvert.DeserializeObject<List<AcList>>(json);
                    if(AcList == null)
                    {
                        AcList = new List<AcList>();
                    }
                    return new FunctionResponse<List<AcList>>() { status = "ok", result = AcList };
                }
            }
            catch(Exception ex)
            { return new FunctionResponse<List<AcList>>() { status = "error", Message = "AcList Couldnot Connect to Server:" + ex.Message }; }
        }

        public static async Task<FunctionResponse<List<MenuItem>>> getMenuItemsList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.MenuItemsListURL);
                //String url = Helpers.Data.Constants.MenuItemsListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var MenuItemsList = JsonConvert.DeserializeObject<List<MenuItem>>(json);
                    if(MenuItemsList == null)
                    {
                        MenuItemsList = new List<MenuItem>();
                    }
                    return new FunctionResponse<List<MenuItem>>() { status = "ok", result = MenuItemsList };
                }
            }
            catch(Exception ex)
            { return new FunctionResponse<List<MenuItem>>() { status = "error", Message = "MenuItems Couldnot Connect to Server:" + ex.Message }; }
        }

        public static async Task<FunctionResponse<List<OrderProd>>> getOrderProdList()
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.OrderProdListURL);
                //String url = Helpers.Data.Constants.OrderProdListURL;
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    var OrderProdList = JsonConvert.DeserializeObject<List<OrderProd>>(json);
                    if (OrderProdList == null)
                    {
                        OrderProdList = new List<OrderProd>();
                    }
                    return new FunctionResponse<List<OrderProd>>() { status = "ok", result = OrderProdList };
                }
            }
            catch (Exception ex){ return new FunctionResponse<List<OrderProd>>() { status = "error", Message = "OrderProd Couldnot Connect to Server:"+ ex.Message }; }
        }

    }
}
