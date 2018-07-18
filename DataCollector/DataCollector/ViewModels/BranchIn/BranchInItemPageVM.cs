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

namespace DataCollector.ViewModels.BranchIn
{
    public class BranchInItemPageVM : BaseViewModel
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

        private BranchInItem _BranchInItem;
        public BranchInItem BranchInItem
        {
            get { return _BranchInItem; }
            set
            {
                _BranchInItem = value;
                OnPropertyChanged("BranchInItem");
            }
        }

        public Command AddCommand { get; set; }

        public BranchInItemPageVM()
        {
            try
            {
                SelectedBarCode = new BarCode();
                BarCodeList = Helpers.Data.BarCodeList;
                AddCommand = new Command(ExecuteAddCommand);
                IsButtonVisible = !Helpers.Data.AutoModeEnabled;
                BranchInItem = new BranchInItem();
                StockTake = new StockTake();
                BranchInItem.SetInitialBranchItem(Helpers.Data.BranchInDetail);

                LoadFromDB.LoadBranchInItemList(App.DatabaseLocation, Helpers.Data.BranchInDetail);
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
                var response = StockTakeValidator.CheckInputData(StockTake, SelectedBarCode);
                if (response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if (response.status == "ok")
                {
                    BranchInItem.barcode = SelectedBarCode.BCODE;
                    BranchInItem.mcode = StockTake.MCODE;
                    BranchInItem.desca = StockTake.DESCA;
                    BranchInItem.quantity = StockTake.QUANTITY;

                    //BranchOutItem.batchNo = Helpers.Data.SelectedBatch.BATCHNO;
                    //BranchOutItem.locationName = Helpers.Data.SelectedBatch.LOCATIONNAME;
                    //BranchOutItem.sessionId = Helpers.Data.Session.SESSIONID;

                    if (CheckReceiveItem())
                    {
                        bool res = InsertIntoDB.InsertBranchInItem(App.DatabaseLocation, BranchInItem);
                        if (res == true)
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Successfully saved StockTake ");

                            BranchInItem.mcode = "";
                            BranchInItem.barcode = "";
                            BranchInItem.quantity = 0;

                            SelectedBarCode = new BarCode();
                        }
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Couldnot save StockTake in Sqlite database");
                    }
                }
            }
            catch { }
        }

        public bool CheckReceiveItem()
        {
            try
            {
                var receiveItemSummary = Helpers.Data.SendBranchOutSummaryList.Find(x => x.mcode == BranchInItem.mcode);
                if (receiveItemSummary == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("This item is not sent");
                    return false;
                }
                var maxQuantity = receiveItemSummary.quantity;
                var BranchInItemList = new List<BranchInItem>(Helpers.Data.BranchInItemList);
                BranchInItemList.Add(BranchInItem);

                var EnteredStockSummary = BranchInDetailValidator.StockTakeToStockSummary(BranchInItemList);
                
                var EnteredQuantity = EnteredStockSummary.Find(x => x.MCODE == BranchInItem.mcode).QUANTITY;
                 
                if (maxQuantity < EnteredQuantity)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Item exceeded the quantity. Couldnot insert");
                    SelectedBarCode = new BarCode();
                    return false;
                }
                else
                {

                    return true;
                }
                // var stocksummary = BranchOutDetailValidator.StockTakeToStockSummary(Helpers.Data.ReceiveItemList);
            }
            catch (Exception e )
            { return false; }
            
        }
    }
}
