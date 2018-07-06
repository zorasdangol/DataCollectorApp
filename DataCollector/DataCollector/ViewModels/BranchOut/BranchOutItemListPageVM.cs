using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchOut
{
    public class BranchOutItemListPageVM:BaseViewModel
    {
        private List<BranchOutItem> _BranchOutItemList;
        public List<BranchOutItem> BranchOutItemList
        {
            get { return _BranchOutItemList; }
            set
            {
                _BranchOutItemList = value;
                OnPropertyChanged("BranchOutItemList");
            }
        }

        private BranchOutItem _SelectedBranchOutItem;
        public BranchOutItem SelectedBranchOutItem
        {
            get { return _SelectedBranchOutItem; }
            set
            {
                _SelectedBranchOutItem = value;
                if (value != null && !string.IsNullOrEmpty(value.barcode))
                {
                    DataDeletion(value);
                }
                OnPropertyChanged("SelectedBranchOutItem");
            }
        }


        public BranchOutItemListPageVM()
        {
            BranchOutItemList = new List<BranchOutItem>();
            SelectedBranchOutItem = new BranchOutItem();
            RefreshItem();
        }


        public void RefreshItem()
        {            
            BranchOutItemList = Helpers.Data.BranchOutItemList;

        }

        public async void DataDeletion(BranchOutItem Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteBranchOutItem(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        //Helpers.Data.BranchOutItemList.Remove(Selected);
                        //GrnDataList = Helpers.Data.GrnDataList;
                        BranchOutItemList = LoadFromDB.LoadBranchOutItemList(App.DatabaseLocation, Helpers.Data.BranchOutDetail);
                        SelectedBranchOutItem = new BranchOutItem();
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Error while Deleting");
                    }
                }
            }
            catch { }
        }

    }
}
