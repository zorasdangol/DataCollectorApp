using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views.BranchIn;
using DataCollectorStandardLibrary.DataAccessLayer;
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

        private List<Warehouse> _WarehouseList;
        public List<Warehouse> WarehouseList
        {
            get { return _WarehouseList; }
            set
            {
                if (value == null)
                    return;
                _WarehouseList = value;
                OnPropertyChanged("WarehouseList");
            }
        }

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
                        BranchInDetail.division = value.INITIAL;
                        BranchInDetail.billTo = value.INITIAL;
                        WarehouseList = Helpers.Data.WarehouseList.Where(x => x.DIVISION == value.INITIAL).ToList();
                        BIEntrySet(value.INITIAL);
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
                        BranchInDetail.billToAdd = value.INITIAL;
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

        private bool _ActivityIndicatorEnabled;
        public bool ActivityIndicatorEnabled
        {
            get { return _ActivityIndicatorEnabled; }
            set
            {
                _ActivityIndicatorEnabled = value;
                OnPropertyChanged("ActivityIndicatorEnabled");
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
            ActivityIndicatorEnabled = false;

        }

        public void BIEntrySet(string store)
        {
            try
            {
                LoadFromDB.LoadBranchInDetailList(App.DatabaseLocation);
                BranchInDetailList = Helpers.Data.BranchInDetailList.Where(x => (x.division == store) && (x.IsSaved == false)).ToList().OrderBy(x => x.curNo).ToList();

                var maxObject = Helpers.Data.BranchInDetailList.Where(x => x.division == store).OrderByDescending(item => item.curNo).FirstOrDefault();

                if (Helpers.Data.EntryMode == "New")
                {
                    if (maxObject == null)
                    {
                        BranchInDetail.vchrNo = "TR" + 1;
                        BranchInDetail.curNo = 1;
                        GrnCount = 1;
                    }
                    else
                    {
                        BranchInDetail.vchrNo = "TR" + (Convert.ToInt32(maxObject.curNo) + 1);
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

        public async void ExecuteBISetCommand()
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
                    var list = new List<BranchInItem>();
                    ActivityIndicatorEnabled = true;
                    DependencyService.Get<IMessage>().LongAlert("Checking BranchOut Data in Server. Please wait for a while.....");
                    var functionresponse = await UploadBranchInData.GetSendItemList(BranchInDetail);
                    if (functionresponse.status == "error")
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Error:" + functionresponse.Message);
                    }
                    else if (functionresponse.status == "ok")
                    {
                        list = functionresponse.result;
                        if (list.Count == 0)
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Ref No. is invalid");
                        }
                        else
                        {
                            foreach (var item in list)
                            {
                                var menuitem = Helpers.Data.MenuItemsList.Where(x => x.MCODE == item.mcode).FirstOrDefault();
                                if (menuitem != null)
                                {
                                    item.desca = menuitem.DESCA;
                                    item.rate = menuitem.RATE_A.ToString();
                                    item.unit = menuitem.BASEUNIT;
                                }
                            }

                            Helpers.Data.SendBranchOutSummaryList = BranchInDetailValidator.ConvertToReceiveItemSummary(list);

                            var res = InsertIntoDB.InsertBranchInDetail(App.DatabaseLocation, BranchInDetail);
                            if (res)
                                App.Current.MainPage = new BranchInTabbedPage();
                            else
                                DependencyService.Get<IMessage>().ShortAlert("Error occured while Inserting Data");
                        }
                    }
                    ActivityIndicatorEnabled = false;
                }
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert("Error:" + e.Message);
                ActivityIndicatorEnabled = false;
            }
        }

        public void PickerValueChange(BranchInDetail BranchInDetail)
        {
            try
            {
                SelectedWarehouse = WarehouseList.Find(x => x.NAME == BranchInDetail.wareHouse);
                SelectedDivisionFrom = DivisionList.Find(x => x.INITIAL == BranchInDetail.billToAdd);
            }
            catch { }

        }
    }
}
