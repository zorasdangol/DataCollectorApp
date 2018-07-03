
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

        public  void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.SelectedMenuType = "1";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainSettingsPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                CheckBatchAndSession("2", new TabbedStockPage());
            }
            else if (SelectedMenuItem.Index == 3)
            {
                Helpers.Data.SelectedMenuType = "3";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new BatchEntryPage());
            }
            else if (SelectedMenuItem.Index == 4)
            {
                Helpers.Data.SelectedMenuType = "4";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new LocationChangePage());
            }
            else if (SelectedMenuItem.Index == 5)
            {
                Helpers.Data.SelectedMenuType = "5";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionStartPage());
            }
            else if (SelectedMenuItem.Index == 6)
            {
                Helpers.Data.SelectedMenuType = "6";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionEndPage());
            }
            else if (SelectedMenuItem.Index == 7)
            {
                Helpers.Data.SelectedMenuType = "7";
                Helpers.Data.SelectedBatch = null;
                ClearFromDB.ClearBatchAll(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("Batch and Session Cleared Successfully");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
            else if (SelectedMenuItem.Index == 8)
            {
                Helpers.Data.SelectedMenuType = "8";
                Helpers.Data.Session = null;
                ClearFromDB.ClearSessionAll(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("Session Cleared Successfully");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
            else if (SelectedMenuItem.Index == 9)
            {
                Helpers.Data.SelectedMenuType = "9";
                Helpers.Data.Session = null;
                ClearFromDB.ClearStockTakeAll(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All StockTake Cleared Successfully");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
            else if (SelectedMenuItem.Index == 10)
            {
                CheckBatchAndSession("10", new GoodsReceivePage());
            }
            else if(SelectedMenuItem.Index == 11)
            {
                Helpers.Data.SelectedMenuType = "11";
                Helpers.Data.GrnDataList = null;
                ClearFromDB.ClearGrnDataList(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All GrnData Cleared Successfully");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
            else if(SelectedMenuItem.Index == 12)
            {
                Helpers.Data.SelectedMenuType = "12";
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionSelectionPage());
            }
        }


        public async  void CheckBatchAndSession(string selectedIndex, Page page)
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
                if(SelectedMenuItem.Index == 2)
                {
                    Helpers.Data.EntryMode = "";
                    (App.Current.MainPage ) = (page);
                    return;
                }

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
            }

        }

    }
}
