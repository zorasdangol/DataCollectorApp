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
                String url = Helpers.Data.Contants.DivisionListURL;
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
               return new FunctionResponse<List<Division>>(){ status = "error", Message = "Couldnot Connect to Server" };
            }
        }

        public static async Task<FunctionResponse<List<Warehouse>>> getWarehouseList()
        {
            try
            {
                String url = Helpers.Data.Contants.WarehouseListURL;
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
            catch { return new FunctionResponse<List<Warehouse>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

        public static async Task<FunctionResponse<List<BarCode>>> getBarCodeList()
        {
            try
            {
                String url = Helpers.Data.Contants.BarcodeListURL;
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
            catch { return new FunctionResponse<List<BarCode>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

        public static async Task<FunctionResponse<List<Location>>> getLocationList()
        {
            try
            {
                String url = Helpers.Data.Contants.LocationListURL;
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
            catch { return new FunctionResponse<List<Location>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

        public static async Task<FunctionResponse<List<AcList>>> getAcList()
        {
            try
            {
                String url = Helpers.Data.Contants.AcListURL;
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
            { return new FunctionResponse<List<AcList>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

        public static async Task<FunctionResponse<List<MenuItem>>> getMenuItemsList()
        {
            try
            {
                String url = Helpers.Data.Contants.MenuItemsListURL;
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
            catch { return new FunctionResponse<List<MenuItem>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

        public static async Task<FunctionResponse<List<OrderProd>>> getOrderProdList()
        {
            try
            {
                String url = Helpers.Data.Contants.OrderProdListURL;
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
            catch (Exception ex){ return new FunctionResponse<List<OrderProd>>() { status = "error", Message = "Couldnot Connect to Server" }; }
        }

    }
}
