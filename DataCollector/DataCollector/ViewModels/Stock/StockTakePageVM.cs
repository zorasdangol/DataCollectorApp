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
            InitialStockTake();
            SelectedBarCode = new BarCode();
            BarCodeList = Helpers.Data.BarCodeList;
            AddCommand = new Command(ExecuteAddCommand);
            IsButtonVisible = !Helpers.Data.AutoModeEnabled;
            _MainData = new LoadDataCollect();

            LoadFromDB.LoadBatch(App.DatabaseLocation);
            LoadFromDB.LoadSession(App.DatabaseLocation);
            var StockTakeList = LoadFromDB.LoadStockTakeList(App.DatabaseLocation);
           // var list = LoadFromDB.LoadStockTakeList(App.DatabaseLocation);
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
                        sid = Helpers.Data.StockTake.sid,
                        ind = Helpers.Data.StockTake.ind,
                        division = Helpers.Data.StockTake.division,
                        wareHouse = Helpers.Data.StockTake.wareHouse,
                        trnDate = Helpers.Data.StockTake.trnDate,

                        BCODE = BarCode.BCODE,
                        MCODE = BarCode.MCODE,
                        DESCA = BarCode.DESCA,
                        QUANTITY = 1
                    };

                    //StockTake.BCODE = BarCode.BCODE;
                    //StockTake.MCODE = BarCode.MCODE;
                    //StockTake.DESCA = BarCode.DESCA;
                    //StockTake.QUANTITY = 1;

                    StockTake.BATCHNO = Helpers.Data.SelectedBatch.BATCHNO;
                    StockTake.LOCATIONNAME = Helpers.Data.SelectedBatch.LOCATIONNAME;
                    StockTake.SESSIONID = Helpers.Data.Session.SESSIONID;

                    var menuitem = Helpers.Data.MenuItemsList.Where(x => x.MCODE == StockTake.MCODE).FirstOrDefault();
                    if (menuitem != null)
                    {
                        StockTake.MENUCODE = menuitem.MENUCODE;
                        StockTake.DESCA = menuitem.DESCA;
                        StockTake.RATE = menuitem.RATE_A;
                        StockTake.UNIT = menuitem.BASEUNIT;
                    }
                    if (Helpers.Data.AutoModeEnabled)
                    {
                        SaveToSqlite();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(SelectedBarCode.BCODE))
                        DependencyService.Get<IMessage>().ShortAlert("InCorrect BarCode");
                    InitialStockTake();
                    SelectedBarCode = new BarCode();
                    //SelectedBarCode.BCODE = EnteredBCODE;
                }                
            }
            catch (Exception e) { }
        }

       
        public void SaveToSqlite()
        {
            try
            {
                var response = StockTakeValidator.CheckInputData(StockTake,SelectedBarCode);
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
                            //StockTake = new StockTake();
                            InitialStockTake();
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

        public void InitialStockTake()
        {
            StockTake = new StockTake()
            {
                sid = Helpers.Data.StockTake.sid,
                ind = Helpers.Data.StockTake.ind,
                division = Helpers.Data.StockTake.division,
                wareHouse = Helpers.Data.StockTake.wareHouse,
                trnDate = Helpers.Data.StockTake.trnDate,
            };
        }

    }
}
