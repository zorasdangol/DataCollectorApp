using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.DataList
{
    public class GRNDataListPageVM:BaseViewModel
    {
        private List<GrnMain> _DataList;
        public List<GrnMain> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        private GrnMain _SelectedGrnMain;
        public GrnMain SelectedGrnMain
        {
            get { return _SelectedGrnMain; }
            set
            {
                _SelectedGrnMain = value;
                if (value != null && !string.IsNullOrEmpty(value.vchrNo))
                {
                    DataDeletion(value);
                }
                OnPropertyChanged("SelectedGrnMain");
            }
        }

        public Command DeleteCommand { get; set; }

        public void RefreshItem()
        {
            DataList = LoadFromDB.LoadGrnMainList(App.DatabaseLocation);           
        }

        public GRNDataListPageVM()
        {
            DataList = LoadFromDB.LoadGrnMainList(App.DatabaseLocation);
            DeleteCommand = new Command(ExecuteDeleteCommand);
        }

        public async void ExecuteDeleteCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
            if (result)
            {
                Helpers.Data.GrnEntryList = null;
                ClearFromDB.ClearGrnDataList(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All GrnData Cleared Successfully");
                this.RefreshItem();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
        }

        public async void DataDeletion(GrnMain Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteGrnMain(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        RefreshItem();
                        SelectedGrnMain = new GrnMain();
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Error while Deleting");
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert(ex.Message);
            }
        }


    }
}
