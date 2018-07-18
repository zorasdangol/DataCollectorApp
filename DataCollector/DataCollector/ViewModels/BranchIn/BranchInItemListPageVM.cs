using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchIn
{
    public class BranchInItemListPageVM:BaseViewModel
    {
        private List<BranchInItem> _BranchInItemList;
        public List<BranchInItem> BranchInItemList
        {
            get { return _BranchInItemList; }
            set
            {
                _BranchInItemList = value;
                OnPropertyChanged("BranchInItemList");
            }
        }

        private BranchInItem _SelectedBranchInItem;
        public BranchInItem SelectedBranchInItem
        {
            get { return _SelectedBranchInItem; }
            set
            {
                _SelectedBranchInItem = value;
                if (value != null && !string.IsNullOrEmpty(value.barcode))
                {
                    DataDeletion(value);
                }
                OnPropertyChanged("SelectedBranchInItem");
            }
        }


        public BranchInItemListPageVM()
        {
            BranchInItemList = new List<BranchInItem>();
            SelectedBranchInItem = new BranchInItem();
            RefreshItem();
        }

        public void RefreshItem()
        {
            BranchInItemList = Helpers.Data.BranchInItemList;
        }

        public async void DataDeletion(BranchInItem Selected)
        {
            try
            {
                var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
                if (res)
                {
                    var result = ClearFromDB.DeleteBranchInItem(App.DatabaseLocation, Selected);
                    if (result)
                    {
                        DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                        //Helpers.Data.BranchOutItemList.Remove(Selected);
                        //GrnDataList = Helpers.Data.GrnDataList;
                        BranchInItemList = LoadFromDB.LoadBranchInItemList(App.DatabaseLocation, Helpers.Data.BranchInDetail);
                        SelectedBranchInItem = new BranchInItem();
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
