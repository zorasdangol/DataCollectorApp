
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollector.DatabaseAccess;

namespace DataCollector.ViewModels
{
    public class LocationChangePageVM : BaseViewModel
    {
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

        public Command SetCommand {get;set;}


        public LocationChangePageVM()
        {
            SetCommand = new Command(ExecuteSetCommand);
            SelectedLocation = new Location();
            LocationList = Helpers.Data.LocationList;

            SelectedBatch = LoadFromDB.LoadBatch(App.DatabaseLocation);

            if (Helpers.Data.SelectedBatch == null || string.IsNullOrEmpty(Helpers.Data.SelectedBatch.BATCHNO))
            {
                SelectedBatch = new Batch();
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new BatchEntryPage());
            }
            else
            {
                Helpers.Data.SelectedBatch = new Batch(SelectedBatch);
                SelectedLocation = LocationList.Find(x => x.NAME == SelectedBatch.LOCATIONNAME);
            }           
        }

        private void ExecuteSetCommand()
        {
            if (SelectedLocation == null || String.IsNullOrEmpty(SelectedLocation.NAME))
            {
                App.Current.MainPage.DisplayAlert("Info", "Select Location First", "Ok");
            }
            else
            {
                SelectedBatch.LOCATIONNAME = SelectedLocation.NAME;
                bool res = DBAccess.LocationChange(App.DatabaseLocation, SelectedBatch);
                if (res == true)
                {
                    Helpers.Data.SelectedBatch = SelectedBatch;
                    DependencyService.Get<IMessage>().ShortAlert("Location Changed Successfully");
                    (App.Current.MainPage as MasterDetailPage).Detail = (new NavigationPage(new MainHomePage())); 
                }
                else
                    DependencyService.Get<IMessage>().LongAlert("Location couldnot be Changed");
            }
        }
    }
}
