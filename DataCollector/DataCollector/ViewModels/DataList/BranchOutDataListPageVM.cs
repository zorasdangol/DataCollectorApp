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
    public class BranchOutDataListPageVM:BaseViewModel
    {
        private List<BranchOutDetail> _DataList;
        public List<BranchOutDetail> DataList
        {
            get { return _DataList; }
            set
            {
                _DataList = value;
                OnPropertyChanged("DataList");
            }
        }

        private BranchOutDetail _SelectedMain;
        public BranchOutDetail SelectedMain
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

        public BranchOutDataListPageVM()
        {
            DataList = LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);
            DeleteCommand = new Command(ExecuteDeleteCommand);

        }

        public void RefreshItem()
        {
            DataList = LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);
        }

        public async void ExecuteDeleteCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
            if (result)
            {
                Helpers.Data.GrnEntryList = null;
                ClearFromDB.ClearBranchOutList(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All BranchOut Cleared Successfully");
                this.RefreshItem();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
        }

        public async void DataDeletion(BranchOutDetail Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteBranchOutDetail(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        RefreshItem();
                        SelectedMain = new BranchOutDetail();
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
