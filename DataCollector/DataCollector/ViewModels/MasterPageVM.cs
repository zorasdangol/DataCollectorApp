
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views;
using DataCollector.Views.GRN;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollector.DatabaseAccess;
using DataCollector.Views.Stock;
using DataCollector.Views.SessionPages;
using DataCollector.Views.BranchOut;
using DataCollector.Views.BranchIn;
using DataCollector.Helpers;
using DataCollector.Views.DataSync;
using DataCollector.Views.DataList;
using DataCollector.Views.ViewDownloads;

namespace DataCollector.ViewModels
{
    public class MasterPageVM:BaseViewModel
    {
        private List<MasterMenu> _MasterMenuList;
       

        public List<MasterMenu> MasterMenuList
        {
            get { return _MasterMenuList; }
            set
            {
                _MasterMenuList = value;
                OnPropertyChanged("MasterMenuList");
            }
        }

        private MasterMenu _SelectedMenuItem;
        public MasterMenu SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set
            {
                if (SelectedMenuItem != value && value != null && value.Index > 0)
                {
                    _SelectedMenuItem = value;
                    NavigateToPage();

                    OnPropertyChanged("SelectedMenuItem");
                }
                else
                {
                    _SelectedMenuItem = null;

                }
            }
        }

        public MasterPageVM()
        {
            MasterMenuList = Helpers.Data.MasterMenuList;
        }

        public async void NavigateToPage()
        {
            try
            {
                (App.Current.MainPage as MasterDetailPage).IsPresented = false;
                if (SelectedMenuItem.Index == 1)
                {
                    Helpers.Data.SelectedMenuType = "1";
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainSettingsPage());
                }
                else if (SelectedMenuItem.Index == 2)
                {                    
                    CheckBatchAndSession("2", new StockTakeDetailPage());
                }
                else if (SelectedMenuItem.Index == 3)
                {
                    await CheckEditOrNew("3", new GoodsReceivePage());
                }

                else if (SelectedMenuItem.Index == 4)
                {
                    await CheckEditOrNew("4", new BranchOutDetailPage());
                }
                else if (SelectedMenuItem.Index == 5)
                {
                    await CheckEditOrNew("5", new BranchInDetailPage());
                }
                else if (SelectedMenuItem.Index == 6)
                {
                    Helpers.Data.SelectedMenuType = "6";
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new BatchEntryPage());
                }
                else if (SelectedMenuItem.Index == 7)
                {
                    Helpers.Data.SelectedMenuType = "7";
                    if (Helpers.Data.SelectedBatch != null && !string.IsNullOrEmpty(Helpers.Data.SelectedBatch.BATCHNO))
                    {
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new LocationChangePage());
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("First Set Batch ");
                    }
                }
                else if (SelectedMenuItem.Index == 8)
                {
                    Helpers.Data.SelectedMenuType = "8";
                    LoadFromDB.LoadBatch(App.DatabaseLocation);
                    if (Helpers.Data.SelectedBatch != null && !string.IsNullOrEmpty(Helpers.Data.SelectedBatch.BATCHNO))
                    {
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionStartPage());
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("First Set Batch ");
                    }
                }
                else if (SelectedMenuItem.Index == 9)
                {
                    Helpers.Data.SelectedMenuType = "9";
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionEndPage());
                }
                else if (SelectedMenuItem.Index == 10)
                {
                    Helpers.Data.SelectedMenuType = "10";
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionSelectionPage());
                }
                else if (SelectedMenuItem.Index == 11)
                {
                    Helpers.Data.SelectedMenuType = "11";
                    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                    if (result)
                    {
                        Helpers.Data.SelectedBatch = null;
                        ClearFromDB.ClearBatchAll(App.DatabaseLocation);
                        DependencyService.Get<IMessage>().LongAlert("Batch and Session Cleared Successfully");
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                    }
                }
                else if (SelectedMenuItem.Index == 12)
                {
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new DataSyncTabbedPage());
                }
                else if (SelectedMenuItem.Index == 13)
                {
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new DataListTabbedPage());
                }
                else if (SelectedMenuItem.Index == 14)
                {
                    Helpers.Data.SelectedMenuType = "14";
                    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to download? ", "Yes", "No");
                    if (result)
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new ActivityIndicatorPage());
                        new DataDownload().DownloadInitialData();
                    }
                }

                else if (SelectedMenuItem.Index == 15)
                {
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new PriceSheetPage());
                }

                else if (SelectedMenuItem.Index == 16)
                {
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new ViewDownloadsTabbedPage());
                }

                #region comment
                //else if (SelectedMenuItem.Index == 8)
                //{
                //    Helpers.Data.SelectedMenuType = "8";
                //    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                //    if (result)
                //    {
                //        Helpers.Data.Session = null;
                //        ClearFromDB.ClearSessionAll(App.DatabaseLocation);
                //        DependencyService.Get<IMessage>().LongAlert("Session Cleared Successfully");
                //        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                //    }
                //}
                //else if (SelectedMenuItem.Index == 9)
                //{
                //    Helpers.Data.SelectedMenuType = "9";
                //    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                //    if (result)
                //    {
                //        Helpers.Data.Session = null;
                //        ClearFromDB.ClearStockTakeAll(App.DatabaseLocation);
                //        DependencyService.Get<IMessage>().LongAlert("All StockTake Cleared Successfully");
                //        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                //    }
                //}

                //else if (SelectedMenuItem.Index == 11)
                //{
                //    Helpers.Data.SelectedMenuType = "11";
                //    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                //    if (result)
                //    {
                //        Helpers.Data.GrnEntryList = null;
                //        ClearFromDB.ClearGrnDataList(App.DatabaseLocation);
                //        DependencyService.Get<IMessage>().LongAlert("All GrnData Cleared Successfully");
                //        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                //    }
                //}
                //else if (SelectedMenuItem.Index == 15)
                //{
                //    Helpers.Data.SelectedMenuType = "15";
                //    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                //    if (result)
                //    {
                //        Helpers.Data.GrnEntryList = null;
                //        ClearFromDB.ClearBranchInList(App.DatabaseLocation);
                //        DependencyService.Get<IMessage>().LongAlert("All BranchIn Cleared Successfully");
                //        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                //    }
                //}
                //else if (SelectedMenuItem.Index == 16)
                //{
                //    Helpers.Data.SelectedMenuType = "16";
                //    var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
                //    if (result)
                //    {
                //        Helpers.Data.GrnEntryList = null;
                //        ClearFromDB.ClearBranchOutList(App.DatabaseLocation);
                //        DependencyService.Get<IMessage>().LongAlert("All BranchOut Cleared Successfully");
                //        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                //    }
                //}
                #endregion

            }
            catch (Exception e)
            { }

        }

        public async void CheckBatchAndSession(string selectedIndex, Page page)
        {
            try
            {
                Helpers.Data.SelectedMenuType = selectedIndex;
                LoadFromDB.LoadBatch(App.DatabaseLocation);
                LoadFromDB.LoadSession(App.DatabaseLocation);
                if (Helpers.Data.SelectedBatch == null || Helpers.Data.SelectedBatch.BATCHNO == "")
                {
                    DependencyService.Get<IMessage>().LongAlert("No Batch Created. First Create a Batch");
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new BatchEntryPage());
                }
                else if (Helpers.Data.Session == null || Helpers.Data.Session.SESSIONID == 0)
                {
                    DependencyService.Get<IMessage>().LongAlert("No Session Active. First Start a Session");
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionStartPage());
                }
                else
                {
                    Helpers.Data.EntryMode = "";
                    //(App.Current.MainPage) = (page);
                    await CheckEditOrNew("3", page);
                }
            }
            catch (Exception e)
            { }
        }

        public async Task CheckEditOrNew(string selectedIndex, Page page)
        {
            try
            {
                var actionSheet = await App.Current.MainPage.DisplayActionSheet("Select New or Edit", "Cancel", null, "Edit", "New");
                switch (actionSheet)
                {
                    case "Cancel":
                        Helpers.Data.EntryMode = "";
                        break;
                    case "Edit":
                        Helpers.Data.EntryMode = "Edit";
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);
                        break;
                    case "New":
                        Helpers.Data.EntryMode = "New";
                        (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(page);
                        break;
                }
            }catch(Exception e)
            { }

        }
        

    }
}
