﻿using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.GRN
{
    public class GRNEntryPageVM : BaseViewModel
    {
        private List<BarCode> _BarCodeList;
        public List<BarCode> BarCodeList
        {
            get { return _BarCodeList; }
            set
            {
                _BarCodeList = value;
                OnPropertyChanged("BarCodeList");
            }
        }

        private StockTake _StockTake;
        public StockTake StockTake
        {
            get { return _StockTake; }
            set
            {
                _StockTake = value;
                OnPropertyChanged("StockTake");
            }
        }


        private BarCode _SelectedBarCode;
        public BarCode SelectedBarCode
        {
            get { return _SelectedBarCode; }
            set
            {
                if (value == null)
                    return;
                _SelectedBarCode = value;
                OnPropertyChanged("SelectedBarCode");
            }
        }

        private bool _IsButtonVisible;
        public bool IsButtonVisible
        {
            get { return _IsButtonVisible; }
            set
            {
                _IsButtonVisible = value;
                OnPropertyChanged("IsButtonVisible");
            }
        }

        private GrnProd _GrnEntry;
        public GrnProd GrnEntry
        {
            get { return _GrnEntry; }
            set
            {
                _GrnEntry = value;
                OnPropertyChanged("GrnEntry");
            }
        }        

        public Command AddCommand { get; set; }

        public GRNEntryPageVM()
        {
            try
            {
                SelectedBarCode = new BarCode();
                BarCodeList = Helpers.Data.BarCodeList;
                AddCommand = new Command(ExecuteAddCommand);
                IsButtonVisible = !Helpers.Data.AutoModeEnabled;
                GrnEntry = new GrnProd();
                StockTake = new StockTake();
                GrnEntry.SetInitialGrnData(Helpers.Data.GrnMain);
                
                LoadFromDB.LoadGrnEntryList(App.DatabaseLocation,Helpers.Data.GrnMain);
            }
            catch { }


        }

        public void ExecuteAddCommand()
        {
            SavingGrnToSqlite();            
        }      

        public void OnEnterPressed()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedBarCode.BCODE))
                {
                    DependencyService.Get<IMessage>().ShortAlert("InCorrect BarCode");
                    return;
                }
                var EnteredBCODE = SelectedBarCode.BCODE;
                var BarCode = BarCodeList.Where(op => op.BCODE == SelectedBarCode.BCODE).FirstOrDefault();
                if (BarCode != null && BarCode.BCODE == SelectedBarCode.BCODE)
                {
                    SelectedBarCode = new BarCode(BarCode);
                    StockTake = new StockTake()
                    {
                        BCODE = BarCode.BCODE,
                        MCODE = BarCode.MCODE,
                        DESCA = BarCode.DESCA,
                        QUANTITY = 1
                    };
                    var menuitem = Helpers.Data.MenuItemsList.Where(x => x.MCODE == StockTake.MCODE).FirstOrDefault();
                    if (menuitem != null)
                    {
                        StockTake.DESCA = menuitem.DESCA;
                        StockTake.RATE = menuitem.RATE_A;
                        StockTake.UNIT = menuitem.BASEUNIT;
                    }
                    if (Helpers.Data.AutoModeEnabled)
                    {
                        SavingGrnToSqlite();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(SelectedBarCode.BCODE))
                        DependencyService.Get<IMessage>().ShortAlert("InCorrect BarCode");
                    StockTake = new StockTake();
                    SelectedBarCode = new BarCode();
                    //SelectedBarCode.BCODE = EnteredBCODE;                  

                }               
            }
            catch (Exception e) { }
        }

        public void SavingGrnToSqlite()
        {
            try
            {
                var response = GrnValidator.CheckInputData(StockTake, SelectedBarCode);
                if(response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if(response.status == "ok")
                {
                    GrnEntry.barcode = SelectedBarCode.BCODE;
                    GrnEntry.mcode = StockTake.MCODE;
                    GrnEntry.desca = StockTake.DESCA;
                    GrnEntry.unit = StockTake.UNIT;
                    GrnEntry.rate = StockTake.RATE.ToString();
                    GrnEntry.quantity = StockTake.QUANTITY.ToString();

                    //GrnEntry.batchNo = Helpers.Data.SelectedBatch.BATCHNO;
                    //GrnEntry.locationName = Helpers.Data.SelectedBatch.LOCATIONNAME;
                    //GrnEntry.sessionId = Helpers.Data.Session.SESSIONID;

                    bool res = InsertIntoDB.InsertGrnEntry(App.DatabaseLocation, GrnEntry);
                    if (res == true)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Successfully saved StockTake ");

                        GrnEntry.mcode = "";
                        GrnEntry.barcode = "";
                        GrnEntry.quantity = "0";

                        SelectedBarCode = new BarCode();
                    }
                    else
                        DependencyService.Get<IMessage>().ShortAlert("Couldnot save StockTake in Sqlite database");
                }
            }
            catch { }
        }

    }
}
