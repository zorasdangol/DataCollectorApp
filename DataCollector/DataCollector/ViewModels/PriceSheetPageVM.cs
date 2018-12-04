using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels
{
    public class PriceSheetPageVM:BaseViewModel
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

        public PriceSheetPageVM()
        {
            SelectedBarCode = new BarCode();
            BarCodeList = Helpers.Data.BarCodeList;
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
                    

                    var menuitem = Helpers.Data.MenuItemsList.Where(x => x.MCODE == StockTake.MCODE).FirstOrDefault();
                    if (menuitem != null)
                    {
                        StockTake.MENUCODE = menuitem.MENUCODE;
                        StockTake.DESCA = menuitem.DESCA;
                        StockTake.RATE = menuitem.VAT==1?(menuitem.RATE_A * 1.13):menuitem.RATE_A;
                        StockTake.UNIT = menuitem.BASEUNIT;
                        StockTake.PRATE = menuitem.PRATE_A;
                        var supplier = Helpers.Data.AcList.Find( x=> x.ACID ==  menuitem.SUPCODE);
                        if(supplier !=null)
                        {
                            StockTake.Supplier = supplier.ACNAME;
                        }
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("No item found in menuitems with this Barcode");
                    }

                    //if (Helpers.Data.AutoModeEnabled)
                    //{
                    //    SaveToSqlite();
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(SelectedBarCode.BCODE))
                        DependencyService.Get<IMessage>().ShortAlert("InCorrect BarCode");
                    
                    SelectedBarCode = new BarCode();
                    StockTake = new StockTake();
                    //SelectedBarCode.BCODE = EnteredBCODE;
                }
            }
            catch (Exception e) { }
        }
    }
}
