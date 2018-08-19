using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.DataList
{
    public class BranchInDataListPageVM:BaseViewModel
    {

        private List<BranchInDetail> _DataList;
        public List<BranchInDetail> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        private BranchInDetail _SelectedMain;
        public BranchInDetail SelectedMain
        {
            get { return _SelectedMain; }
            set
            {
                _SelectedMain = value;
                if (value != null && !string.IsNullOrEmpty(value.vchrNo))
                {
                    DataDeletion(value);
                }
                OnPropertyChanged("SelectedMain");
            }
        }

        public Command DeleteCommand { get; set; }

        public BranchInDataListPageVM()
        {
            DataList = LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
            DeleteCommand = new Command(ExecuteDeleteCommand);

        }

        public async void ExecuteDeleteCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
            if (result)
            {
                Helpers.Data.GrnEntryList = null;
                ClearFromDB.ClearBranchInList(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All BranchIn Cleared Successfully");
                this.RefreshItem();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
        }
        public void RefreshItem()
        {
            DataList = LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
        }


        public async void DataDeletion(BranchInDetail Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteBranchInDetail(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        RefreshItem();
                        SelectedMain = new BranchInDetail();
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
