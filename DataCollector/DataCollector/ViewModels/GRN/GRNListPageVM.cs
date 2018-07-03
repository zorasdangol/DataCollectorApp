﻿using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.GRN
{
    public class GRNListPageVM:BaseViewModel
    {
        private List<GrnEntry> _GrnDataList;
        public List<GrnEntry> GrnDataList
        {
            get { return _GrnDataList; }
            set
            {
                _GrnDataList = value;
                OnPropertyChanged("GrnDataList");
            }
        }


        private List<StockTake> _StockTakeList;
        public List<StockTake> StockTakeList
        {
            get { return _StockTakeList; }
            set
            {
                _StockTakeList = value;
                OnPropertyChanged("StockTakeList");
            }
        }

        private GrnEntry _SelectedGrnData;
        public GrnEntry SelectedGrnData
        {
            get { return _SelectedGrnData; }
            set
            {
                //_SelectedGrnDataList = value;
                if (value != null && !string.IsNullOrEmpty(value.barcode))
                {
                    DataDeletion(SelectedGrnData);
                    _SelectedGrnData = new GrnEntry();
                }
                OnPropertyChanged("SelectedGrnDataList");
            }
        }

        public GRNListPageVM()
        {
            GrnDataList = new List<GrnEntry>();
            //SelectedGrnDataList = new GrnEntry();
            RefreshItem();
        }

        public void RefreshItem()
        {
            //GrnDataList = LoadFromDB.LoadGrnDataList(App.DatabaseLocation,Helpers.Data.GrnMain);

            GrnDataList = Helpers.Data.GrnDataList;
           
        }

        public async void DataDeletion(GrnEntry Selected)
        {
            var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
            if (res)
            {
                var result = ClearFromDB.DeleteGrnData(App.DatabaseLocation, Selected);
                if (result)
                {
                    DependencyService.Get<IMessage>().ShortAlert(" Item Deleted Successfully");
                    Helpers.Data.GrnDataList.Remove(Selected);
                    GrnDataList = Helpers.Data.GrnDataList;
                    SelectedGrnData = new GrnEntry();
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Error while Deleting");
                }
            }
        }
    }
}
