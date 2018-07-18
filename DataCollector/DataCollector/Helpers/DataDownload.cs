using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.Helpers
{
    public class DataDownload
    {
        public async void DownloadInitialData()
        {
            try
            {
                await LocationDownload();
                await Task.Delay(5000);

                await DivisionDownload();
                await Task.Delay(5000);

                await BarCodeDownload();
                await Task.Delay(5000);

                await WarehouseDownload();
                await Task.Delay(5000);
                
                await AcListDownload();
                await Task.Delay(5000);

                await MenuItemsDownload();
                await Task.Delay(5000);

                await OrderProdDownload();
                await Task.Delay(5000);

                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch { }
        }

        public async Task LocationDownload()
        {
            try
            {
                var LocationRes = await BaseDataAccess.getLocationList();
                if (LocationRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, LocationRes.result);
                    if (res)
                    {
                        Helpers.Data.LocationList = LocationRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("Location List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Location List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(LocationRes.Message);
                }
            }
            catch { }
        }

        public async Task DivisionDownload()
        {
            try
            {
                var DivisionRes = await BaseDataAccess.getDivisionList();
                if (DivisionRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, DivisionRes.result);
                    if (res)
                    {
                        Helpers.Data.DivisionList = DivisionRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("Division List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Division List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(DivisionRes.Message);
                }
            }
            catch { }
        }

        public async Task BarCodeDownload()
        {
            try
            {
                var BarCodeRes = await BaseDataAccess.getBarCodeList();
                if (BarCodeRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, BarCodeRes.result);
                    if (res)
                    {
                        Helpers.Data.BarCodeList = BarCodeRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("BarCode List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("BarCode List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(BarCodeRes.Message);
                }

            }
            catch { }
        }
        
        public async Task WarehouseDownload()
        {
            try
            {
                var WarehouseRes = await BaseDataAccess.getWarehouseList();
                if (WarehouseRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, WarehouseRes.result);
                    if (res)
                    {
                        Helpers.Data.WarehouseList = WarehouseRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("Warehouse List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Warehouse List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(WarehouseRes.Message);
                }
            }
            catch { }
        }                   

        public async Task AcListDownload()
        {
            try
            {
                var AcListRes = await BaseDataAccess.getAcList();
                if (AcListRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, AcListRes.result);
                    if (res)
                    {
                        Helpers.Data.AcList = AcListRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("AcList List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("AcList List could not be  loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(AcListRes.Message);
                }
            }
            catch { }
        }

        public async Task MenuItemsDownload()
        {
            try
            {
                var MenuItemsRes = await BaseDataAccess.getMenuItemsList();
                if (MenuItemsRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, MenuItemsRes.result);
                    if (res)
                    {
                        Helpers.Data.MenuItemsList = MenuItemsRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("MenuItems List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("MenuItems List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(MenuItemsRes.Message);
                }

            }
            catch { }
        }

        public async Task OrderProdDownload()
        {
            try
            {
                var OrderProdRes = await BaseDataAccess.getOrderProdList();
                if (OrderProdRes.status == "ok")
                {
                    var res = InsertIntoDB.InsertList(App.DatabaseLocation, OrderProdRes.result);
                    if (res)
                    {
                        Helpers.Data.OrderProdList = OrderProdRes.result;
                        DependencyService.Get<IMessage>().ShortAlert("OrderProd List loaded");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("OrderProd List could not be loaded");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert(OrderProdRes.Message);
                }

            }
            catch { }
        }

    }
}
