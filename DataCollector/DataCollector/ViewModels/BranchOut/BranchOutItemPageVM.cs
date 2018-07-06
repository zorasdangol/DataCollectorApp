using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchOut
{
    public class BranchOutItemPageVM:BaseViewModel
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

        private BranchOutItem _BranchOutItem;
        public BranchOutItem BranchOutItem
        {
            get { return _BranchOutItem; }
            set
            {
                _BranchOutItem = value;
                OnPropertyChanged("BranchOutItem");
            }
        }

        public Command AddCommand { get; set; }

        public BranchOutItemPageVM()
        {
            try
            {
                SelectedBarCode = new BarCode();
                BarCodeList = Helpers.JsonData.BarCodeList;
                AddCommand = new Command(ExecuteAddCommand);
                IsButtonVisible = !Helpers.Data.AutoModeEnabled;
                BranchOutItem = new BranchOutItem();
                StockTake = new StockTake();
                BranchOutItem.SetInitialBranchOutItem(Helpers.Data.BranchOutDetail);

                LoadFromDB.LoadBranchOutItemList(App.DatabaseLocation, Helpers.Data.BranchOutDetail);
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

                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("InCorrect BarCode");
                    StockTake = new StockTake();
                    SelectedBarCode = new BarCode();
                    //SelectedBarCode.BCODE = EnteredBCODE;                   

                }

                if (Helpers.Data.AutoModeEnabled)
                {
                    SavingGrnToSqlite();
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

                        //StockTake.BATCHNO = Helpers.Data.SelectedBatch.BATCHNO;
                        //StockTake.LOCATIONNAME = Helpers.Data.SelectedBatch.LOCATIONNAME;
                        //StockTake.SESSIONID = Helpers.Data.Session.SESSIONID;

                        SavingGrnToSqlite();
                    }
                    else
                    {
                        StockTake = new StockTake();
                        SelectedBarCode = new BarCode();
                        //SelectedBarCode.BCODE = newText;
                    }
                }
            }
            catch (Exception e) { }
        }

        public void SavingGrnToSqlite()
        {
            try
            {
                var response = StockTakeValidator.CheckInputData(StockTake, SelectedBarCode);
                if (response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if (response.status == "ok")
                {
                    BranchOutItem.barcode = SelectedBarCode.BCODE;
                    BranchOutItem.mcode = StockTake.MCODE;
                    BranchOutItem.desca = StockTake.DESCA;
                    BranchOutItem.quantity = StockTake.QUANTITY.ToString();
                    
                    //BranchOutItem.batchNo = Helpers.Data.SelectedBatch.BATCHNO;
                    //BranchOutItem.locationName = Helpers.Data.SelectedBatch.LOCATIONNAME;
                    //BranchOutItem.sessionId = Helpers.Data.Session.SESSIONID;

                    bool res = InsertIntoDB.InsertBranchOutItem(App.DatabaseLocation, BranchOutItem);
                    if (res == true)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Successfully saved StockTake ");

                        BranchOutItem.mcode = "";
                        BranchOutItem.barcode = "";
                        BranchOutItem.quantity = "0";

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
