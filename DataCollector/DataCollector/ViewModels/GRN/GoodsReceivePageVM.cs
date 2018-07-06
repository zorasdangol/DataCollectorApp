using DataCollector.DatabaseAccess;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollector.Views;
using DataCollector.Views.GRN;
using DataCollectorStandardLibrary;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.DataValidationLayer;

namespace DataCollector.ViewModels.GRN
{
    public class GoodsReceivePageVM:BaseViewModel
    {
        public List<Division> DivisionList { get; set; }
        public List<Warehouse> WarehouseList { get; set; }
        public List<OrderProd> OrderProdList { get; set; }
        public List<AcList> AcList { get; set; }


        //used for recent count of GRN
        public int GrnCount { get; set; }

        private List<GrnMain> _GrnMainList;
        public List<GrnMain> GrnMainList
        {
            get { return _GrnMainList; }
            set
            {
                _GrnMainList = value;
                OnPropertyChanged("GrnMainList");
            }
        }

        private GrnMain _GrnMain;
        public GrnMain GrnMain
        {
            get { return _GrnMain; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        _GrnMain = value;
                        if (!string.IsNullOrEmpty(GrnMain.division))
                        {
                            PickerValueChange(value);
                        }
                        OnPropertyChanged("GrnMain");
                    }

                }catch{ }                
            }
        }
        
        private GrnMain _SelectedGrn;
        public GrnMain SelectedGrn
        {
            get { return _SelectedGrn; }
            set
            {
                try
                {
                    if (value != null && !string.IsNullOrEmpty(value.vchrNo))
                    {
                        _SelectedGrn = value;
                        GrnMain = new GrnMain();
                        GrnMain = value;
                        //SetGrnMainData(value);
                        OnPropertyChanged("SelectedGrn");
                    }
                }
                catch { }
            }
        }

       

        private Division _SelectedStore;
        public Division SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                try
                {
                    _SelectedStore = value;
                    
                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        GrnMain = new GrnMain();
                        GrnMain.division = value.NAME;
                        GrnEntrySet(value.NAME);
                    }
                    OnPropertyChanged("SelectedStore");
                }catch(Exception e)
                { }
               
            }
        }

        private Warehouse _SelectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get { return _SelectedWarehouse; }
            set
            {
                try
                {
                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        GrnMain.wareHouse = value.NAME;
                        _SelectedWarehouse = value;
                        OnPropertyChanged("SelectedWarehouse");
                    }
                }catch(Exception e)
                { }
                
            }
        }

        private OrderProd _SelectedOrderProd;
        public OrderProd SelectedOrderProd
        {
            get { return _SelectedOrderProd; }
            set
            {
                try
                {
                    if (value != null)
                    {

                        GrnMain.refOrdBill = value.VCHRNO;
                        if (!string.IsNullOrEmpty(value.SUPPLIERCODE))
                            SelectedAcList = AcList.Where(x => x.ACID == value.SUPPLIERCODE).ToList().FirstOrDefault();
                        _SelectedOrderProd = value;
                        OnPropertyChanged("SelectedOrderProd");
                    }
                }
                catch { }
                
            }
        }


        private AcList _SelectedAcList;
        public AcList SelectedAcList
        {
            get { return _SelectedAcList; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        GrnMain.trnAc = value.ACID;
                        GrnMain.supplierName = value.ACNAME;
                        _SelectedAcList = value;
                        OnPropertyChanged("SelectedAcList");
                    }
                }catch(Exception e)
                { }
                
            }
        }

        private bool _IsCash;
        public bool IsCash
        {
            get { return _IsCash; }
            set
            {
                if(_IsCash != value)
                {
                    _IsCash = value;
                    IsCredit = !value;
                    GrnMain.trnMode = "Cash";
                    OnPropertyChanged("IsCash");
                }
            }
        }

        private bool _IsCredit;
        public bool IsCredit
        {
            get { return _IsCredit; }
            set
            {
                if (_IsCredit != value)
                {
                    _IsCredit = value;
                    IsCash = !value;
                    GrnMain.trnMode = "CreditCard";
                    OnPropertyChanged("IsCredit");
                }
            }
        }

        private bool _IsUsePoNo;
        public bool IsUsePoNo
        {
            get { return _IsUsePoNo; }
            set
            {
                SelectedOrderProd = new OrderProd();
                SelectedAcList = new AcList();
                _IsUsePoNo = value;
                OnPropertyChanged("IsUsePoNo");
            }
        }

        private bool _IsTaxInvoice;
        public bool IsTaxInvoice
        {
            get { return _IsTaxInvoice; }
            set
            {
                try
                {
                    _IsTaxInvoice = value;
                    if (value == true)
                        GrnMain.isTaxInvoice = "1";
                    else
                        GrnMain.isTaxInvoice = "0";
                    OnPropertyChanged("IsTaxInvoice");

                }
                catch { }
                         
            }
        }

        public Command GrnSetCommand { get; set; }

        public GoodsReceivePageVM()
        {            
            GrnSetCommand = new Command(ExecuteGrnSetCommand);
            GrnMain = new GrnMain();
            Helpers.Data.GrnMainData = new LoadGrnCollect();
            SelectedStore = new Division();
            SelectedAcList = new AcList();
            SelectedGrn = new GrnMain();
            GrnMain = new GrnMain();
            DivisionList = Helpers.JsonData.DivisionList;
            WarehouseList = Helpers.JsonData.WarehouseList;
            OrderProdList = Helpers.JsonData.OrderProdList;
            AcList = Helpers.JsonData.AcList;
            IsCash = true;
            IsUsePoNo = false;
            IsTaxInvoice = false;
        }

        public void GrnEntrySet(string store)
        {
            try
            {
                LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);

                GrnMainList = Helpers.Data.GrnMainList.Where(x => x.division == store).ToList().OrderBy(x => x.curNo).ToList();
                
                var maxObject = Helpers.Data.GrnMainList.Where(x => x.division == store).OrderByDescending(item => item.curNo).FirstOrDefault();
                                
                if(Helpers.Data.EntryMode == "New")
                {
                    if (maxObject == null)
                    {
                        GrnMain.vchrNo = "GRN" + 1;
                        GrnMain.curNo = 1;
                        GrnCount = 1;
                        
                    }
                    else
                    {
                        GrnMain.vchrNo = "GRN" + (Convert.ToInt32(maxObject.curNo) + 1);
                        GrnCount = maxObject.curNo + 1;
                        GrnMain.curNo = GrnCount;
                    }

                    GrnMainList.Add(GrnMain);
                    SelectedGrn = GrnMainList.Where(x => x.vchrNo == GrnMain.vchrNo).ToList().FirstOrDefault();
                }                             

                OnPropertyChanged("GrnMain");

            }catch(Exception e)
            { }
           
        }

       
        public void ExecuteGrnSetCommand()
        {
            try
            {
                var response = GrnMainValidator.CheckInputData(GrnMain);
                if (response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if (response.status == "ok")
                {
                    Helpers.Data.GrnMain = GrnMain;
                   // GrnMain.curNo = GrnCount;
                    
                    InsertIntoDB.InsertGrnMain(App.DatabaseLocation, GrnMain);

                    App.Current.MainPage = new GRNTabbedPage();
                }
            }catch(Exception e)
            { }           
        }

        //private void SetGrnMainData(GrnMain GrnMain)
        //{
        //    try
        //    {
        //        //GrnMainData.vchrNo = GrnMain.vchrNo;
        //        //GrnMainData.division = GrnMain.division;
        //        //GrnMainData.chalanNo = GrnMain.chalanNo;
        //        //GrnMainData.trnDate = GrnMain.trnDate;
        //        //GrnMainData.trnAc = GrnMain.trnAc;
        //        //GrnMainData.ParAc = GrnMain.ParAc;
        //        //GrnMainData.trnMode = GrnMain.trnMode;
        //        //GrnMainData.refOrdBill = GrnMain.refOrdBill;
        //        //GrnMainData.remarks = GrnMain.remarks;
        //        //GrnMainData.wareHouse = GrnMain.wareHouse;
        //        //GrnMainData.isTaxInvoice = GrnMain.isTaxInvoice;
                
               
                
        //    }
        //    catch { }
        //}

        public void PickerValueChange(GrnMain GrnMain)
        {
            try
            {
                SelectedAcList = AcList.Find(x => x.ACID == GrnMain.trnAc);
                SelectedWarehouse = WarehouseList.Find(x => x.NAME == GrnMain.wareHouse);

                if (GrnMain.trnMode == "Cash")
                {
                    IsCash = true;
                }
                else if (GrnMain.trnMode == "CreditCard")
                {
                    IsCash = false;
                }

            }
            catch { }
            

        }

       
    }

   
}
