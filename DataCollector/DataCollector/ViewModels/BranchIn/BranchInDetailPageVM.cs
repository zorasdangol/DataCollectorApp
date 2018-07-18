using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views.BranchIn;
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
    public class BranchInDetailPageVM:BaseViewModel
    {
        public List<Division> DivisionList { get; set; }
        public List<Warehouse> WarehouseList { get; set; }

        //used for recent count of GRN
        public int GrnCount { get; set; }

        private List<BranchInDetail> _BranchInDetailList;
        public List<BranchInDetail> BranchInDetailList
        {
            get { return _BranchInDetailList; }
            set
            {
                _BranchInDetailList = value;
                OnPropertyChanged("BranchInDetailList");
            }
        }

        private List<BranchOutItem> _ReceivedItemList;
        public List<BranchOutItem> ReceivedItemList
        {
            get { return _ReceivedItemList; }
            set
            {
                _ReceivedItemList = value;
                OnPropertyChanged("ReceivedItemList");
            }
        }

        private BranchInDetail _BranchInDetail;
        public BranchInDetail BranchInDetail
        {
            get { return _BranchInDetail; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        _BranchInDetail = value;
                        if (!string.IsNullOrEmpty(value.division))
                        {
                            PickerValueChange(value);
                        }
                        OnPropertyChanged("BranchInDetail");
                    }

                }
                catch { }
            }
        }

        private BranchInDetail _SelectedBranchInDetail;
        public BranchInDetail SelectedBranchInDetail
        {
            get { return _SelectedBranchInDetail; }
            set
            {
                try
                {
                    if (value != null && !string.IsNullOrEmpty(value.vchrNo))
                    {
                        _SelectedBranchInDetail = value;
                        BranchInDetail = new BranchInDetail();
                        BranchInDetail = value;
                        OnPropertyChanged("SelectedBranchInDetail");
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
                        BranchInDetail = new BranchInDetail();
                        BranchInDetail.division = value.NAME;
                        BIEntrySet(value.NAME);
                    }
                    OnPropertyChanged("SelectedStore");
                }
                catch (Exception e)
                { }

            }
        }

        private Division _SelectedDivisionFrom;
        public Division SelectedDivisionFrom
        {
            get { return _SelectedDivisionFrom; }
            set
            {
                try
                {
                    _SelectedDivisionFrom = value;
                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        BranchInDetail.divisionFrom = value.NAME;
                    }
                    OnPropertyChanged("SelectedDivisionFrom");
                }
                catch (Exception e)
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
                        BranchInDetail.wareHouse = value.NAME;
                        _SelectedWarehouse = value;
                        OnPropertyChanged("SelectedWarehouse");
                    }
                }
                catch (Exception e)
                { }
            }
        }

       

        public Command BISetCommand { get; set; }


        public BranchInDetailPageVM()
        {
            BISetCommand = new Command(ExecuteBISetCommand);
            SelectedStore = new Division();
            SelectedDivisionFrom = new Division();
            SelectedWarehouse = new Warehouse();
            BranchInDetail = new BranchInDetail();
            DivisionList = Helpers.Data.DivisionList;
            WarehouseList = Helpers.Data.WarehouseList;
        }

        public void BIEntrySet(string store)
        {
            try
            {
                LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
                BranchInDetailList = Helpers.Data.BranchInDetailList.Where(x => x.division == store).ToList().OrderBy(x => x.curNo).ToList();

                var maxObject = Helpers.Data.BranchInDetailList.Where(x => x.division == store).OrderByDescending(item => item.curNo).FirstOrDefault();

                if (Helpers.Data.EntryMode == "New")
                {
                    if (maxObject == null)
                    {
                        BranchInDetail.vchrNo = "BI" + 1;
                        BranchInDetail.curNo = 1;
                        GrnCount = 1;
                    }
                    else
                    {
                        BranchInDetail.vchrNo = "BI" + (Convert.ToInt32(maxObject.curNo) + 1);
                        GrnCount = maxObject.curNo + 1;
                        BranchInDetail.curNo = GrnCount;
                    }

                    BranchInDetailList.Add(BranchInDetail);
                    SelectedBranchInDetail = BranchInDetailList.Where(x => x.vchrNo == BranchInDetail.vchrNo).ToList().FirstOrDefault();
                }
            }
            catch
            { }
        }

        public void ExecuteBISetCommand()
        {
            try
            {
                var response = BranchInDetailValidator.CheckInputData(BranchInDetail);
                if (response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if (response.status == "ok")
                {
                    Helpers.Data.BranchInDetail = BranchInDetail;
                    var branchin = BranchInDetail;
                    var list = LoadFromDB.LoadSendBranchOutList(App.DatabaseLocation,BranchInDetail);
                    
                    Helpers.Data.SendBranchOutSummaryList = BranchInDetailValidator.ConvertToReceiveItemSummary(list);
                    
                    //var list = Helpers.JsonData.ReceivedBranchOutList;

                    if (list.Count == 0)
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Ref No. is invalid");
                    }
                    else
                    {
                        var res = InsertIntoDB.InsertBranchInDetail(App.DatabaseLocation, BranchInDetail);
                        if(res)
                            App.Current.MainPage = new BranchInTabbedPage();
                        else
                            DependencyService.Get<IMessage>().ShortAlert("Error occured while Inserting Data");
                    }
                }
            }
            catch (Exception e)
            { }
        }

        public void PickerValueChange(BranchInDetail BranchInDetail)
        {
            try
            {
                SelectedWarehouse = WarehouseList.Find(x => x.NAME == BranchInDetail.wareHouse);
                SelectedDivisionFrom = DivisionList.Find(x => x.NAME == BranchInDetail.divisionFrom);
            }
            catch { }

        }
    }
}
