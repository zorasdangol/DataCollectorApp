
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollector.DatabaseAccess;
using DataCollector.Views.SessionPages;

namespace DataCollector.ViewModels
{
    public class BatchEntryPageVM : BaseViewModel
    {
        private List<Location> _LocationList;

        public List<Location> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private Location _SelectedLocation;
        public Location SelectedLocation
        {
            get { return _SelectedLocation; }
            set
            {
                _SelectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        private Batch _SelectedBatch;
        public Batch SelectedBatch
        {
            get { return _SelectedBatch; }
            set
            {
                _SelectedBatch = value;
                OnPropertyChanged("SelectedBatch");

            }
        }

        public Command SetCommand { get; set; }

        public BatchEntryPageVM()
        {
            try
            {
                SetCommand = new Command(ExecuteSetCommand);
                SelectedLocation = new Location();
                LocationList = Helpers.Data.LocationList;

                SelectedBatch = LoadFromDB.LoadBatch(App.DatabaseLocation);
                if (SelectedBatch != null && !string.IsNullOrEmpty(SelectedBatch.BATCHNO))
                {
                    //Helpers.Data.SelectedBatch = new Batch(SelectedBatch);
                    SelectedLocation = LocationList.Find(x => x.NAME == SelectedBatch.LOCATIONNAME);                   

                }
                else
                {
                    SelectedBatch = new Batch();
                }
            }catch(Exception e) { }

        }

        private void ExecuteSetCommand()
        {
            if(SelectedLocation == null || SelectedLocation.NAME == "")
            {
                App.Current.MainPage.DisplayAlert("Info", "Select Location First", "Ok");
            }
            else if(SelectedBatch == null || SelectedBatch.BATCHNO == "")
            {
                App.Current.MainPage.DisplayAlert("Info", "Enter Batch No. First", "Ok");
            }
            else if (SelectedBatch == null || SelectedBatch.USERNAME == "")
            {
                App.Current.MainPage.DisplayAlert("Info", "Enter UserName First", "Ok");
            }
            else
            {
                SelectedBatch.LOCATIONNAME = SelectedLocation.NAME;
                SelectedBatch.DATE = DateTime.Today;
                SelectedBatch.STAMP = new DateTime().TimeOfDay.ToString();

                bool res = InsertIntoDB.InsertBatch(App.DatabaseLocation,SelectedBatch);
                if (res == true)
                {
                    ClearFromDB.ClearSessionAll(App.DatabaseLocation);
                    DependencyService.Get<IMessage>().LongAlert("Batch Created. Now Start Session ");
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionStartPage());
                }
                else
                    App.Current.MainPage.DisplayAlert("Error", "Couldnot save Batch in Sqlite database", "Ok");
            }

        }
    }
}

