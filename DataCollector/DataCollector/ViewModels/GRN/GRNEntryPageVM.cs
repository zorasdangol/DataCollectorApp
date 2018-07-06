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

        private GrnEntry _GrnEntry;
        public GrnEntry GrnEntry
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
                BarCodeList = Helpers.JsonData.BarCodeList;
                AddCommand = new Command(ExecuteAddCommand);
                IsButtonVisible = !Helpers.Data.AutoModeEnabled;
                GrnEntry = new GrnEntry();
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
                var response = GrnValidator.CheckInputData(StockTake, SelectedBarCode);
                if(response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if(response.status == "ok")
                {
                    GrnEntry.barcode = SelectedBarCode.BCODE;
                    GrnEntry.mcode = StockTake.MCODE;
                    GrnEntry.desca = StockTake.DESCA;
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
