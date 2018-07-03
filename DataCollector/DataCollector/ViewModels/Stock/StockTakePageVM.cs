using DataCollector.DatabaseAccess;
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
using DataCollectorStandardLibrary;
using DataCollectorStandardLibrary.DataValidationLayer;

namespace DataCollector.ViewModels.Stock
{
    public class StockTakePageVM:BaseViewModel
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

        private object _MainData;

        public Command AddCommand { get; set; }

        public StockTakePageVM()
        {           
            StockTake = new StockTake();
            SelectedBarCode = new BarCode();
            BarCodeList = Helpers.JsonData.BarCodeList;
            AddCommand = new Command(ExecuteAddCommand);
            IsButtonVisible = !Helpers.Data.AutoModeEnabled;
            _MainData = new LoadDataCollect();



            var StockTakeList = LoadFromDB.LoadStockTake(App.DatabaseLocation);
            var list = LoadFromDB.LoadStockTake(App.DatabaseLocation);

        }

        public void ExecuteAddCommand()
        {
            SaveToSqlite();           
        }
        
        public void OnEnterPressed()
        {
            try
            {
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
                    StockTake.BATCHNO = Helpers.Data.SelectedBatch.BATCHNO;
                    StockTake.LOCATIONNAME = Helpers.Data.SelectedBatch.LOCATIONNAME;
                    StockTake.SESSIONID = Helpers.Data.Session.SESSIONID;
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("InCorrect BarCode");

                    StockTake = new StockTake();
                    SelectedBarCode = new BarCode();
                    //SelectedBarCode.BCODE = EnteredBCODE;

                }

                if (Helpers.Data.AutoModeEnabled)
                {
                    SaveToSqlite();
                }
            }
            catch (Exception e) { }
        }

        public void BarCode_Entry_TextChanged(string oldText, string newText)
        {
            try
            {
                if (oldText != newText)
                {
                    BarCode BarCode = null;
                    BarCode = BarCodeList.Where(op => op.BCODE == newText).FirstOrDefault();
                    if (BarCode != null && BarCode.BCODE == newText)
                    {
                        SelectedBarCode = new BarCode(BarCode);
                        StockTake = new StockTake()
                        {
                            MCODE = BarCode.MCODE,
                            DESCA = BarCode.DESCA,
                            QUANTITY = 1
                        };
                        StockTake.BATCHNO = Helpers.Data.SelectedBatch.BATCHNO;
                        StockTake.LOCATIONNAME = Helpers.Data.SelectedBatch.LOCATIONNAME;
                        StockTake.SESSIONID = Helpers.Data.Session.SESSIONID;

                    }
                    else
                    {
                        StockTake = new StockTake();
                        SelectedBarCode = new BarCode();
                        SelectedBarCode.BCODE = newText;
                    }
                }
            }
            catch (Exception e) { }
        }

        public void SaveToSqlite()
        {
            try
            {
                var response = StockValidator.CheckInputData(StockTake);
                if (response.status == "error")
                {
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                }
                else if (response.status == "ok")
                {
                    {
                        bool res = InsertIntoDB.InsertStockTake(App.DatabaseLocation, StockTake);
                        if (res == true)
                        {
                            DependencyService.Get<IMessage>().LongAlert("Successfully saved StockTake ");
                            StockTake = new StockTake();
                            SelectedBarCode = new BarCode();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot save StockTake in Sqlite database");
                    }
                }
            }
            catch
            { }
        }

    }
}
