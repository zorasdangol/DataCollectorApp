﻿using DataCollector.DatabaseAccess;
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
using DataCollector.Views.BranchOut;

namespace DataCollector.ViewModels.GRN
{
    public class BranchOutDetailPageVM:BaseViewModel
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

        private List<BranchOutDetail> _BranchOutDetailList;
        public List<BranchOutDetail> BranchOutDetailList
        {
            get { return _BranchOutDetailList; }
            set
            {
                _BranchOutDetailList = value;
                OnPropertyChanged("BranchOutDetailList");
            }
        }

        private BranchOutDetail _BranchOutDetail;
        public BranchOutDetail BranchOutDetail
        {
            get { return _BranchOutDetail; }
            set
            {
                try
                {
                    if (value != null)
                    {
                        _BranchOutDetail = value;
                        if (!string.IsNullOrEmpty(value.division))
                        {
                            PickerValueChange(value);
                        }
                        OnPropertyChanged("BranchOutDetail");
                    }

                }catch{ }                
            }
        }             

        private BranchOutDetail _SelectedBranchOutDetail;
        public BranchOutDetail SelectedBranchOutDetail
        {
            get { return _SelectedBranchOutDetail; }
            set
            {
                try
                {
                    if (value != null && !string.IsNullOrEmpty(value.vchrNo))
                    {
                        _SelectedBranchOutDetail = value;
                        BranchOutDetail = new BranchOutDetail();
                        BranchOutDetail = value;
                        OnPropertyChanged("SelectedBranchOutDetail");
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
                        BranchOutDetail = new BranchOutDetail();
                        BranchOutDetail.division = value.INITIAL;
                        BranchOutDetail.billTo = value.INITIAL;
                        WarehouseList = Helpers.Data.WarehouseList.Where(x => x.DIVISION == value.INITIAL).ToList();
                        BOEntrySet(value.INITIAL);
                    }
                    OnPropertyChanged("SelectedStore");
                }catch(Exception e)
                { }
               
            }
        }

        private Division _SelectedDivisionTo;
        public Division SelectedDivisionTo
        {
            get { return _SelectedDivisionTo; }
            set
            {
                try
                {
                    _SelectedDivisionTo = value;
                    if (value != null && !string.IsNullOrEmpty(value.NAME))
                    {
                        BranchOutDetail.billToAdd = value.INITIAL;
                    }
                    OnPropertyChanged("SelectedDivisionTo");
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
                        BranchOutDetail.wareHouse = value.NAME;
                        _SelectedWarehouse = value;
                        OnPropertyChanged("SelectedWarehouse");
                    }
                }catch(Exception e)
                { }                
            }
        }      

        public Command BOSetCommand { get; set; }

        public BranchOutDetailPageVM()
        {            
            BOSetCommand = new Command(ExecuteBOSetCommand);
            SelectedStore = new Division();
            SelectedDivisionTo = new Division();
            SelectedWarehouse = new Warehouse();
            BranchOutDetail = new BranchOutDetail();
            DivisionList = Helpers.Data.DivisionList;
            WarehouseList = Helpers.Data.WarehouseList;
        }

        public void BOEntrySet(string store)
        {
            try
            {
                LoadFromDB.LoadBranchOutDetailList(App.DatabaseLocation);
                BranchOutDetailList = Helpers.Data.BranchOutDetailList.Where(x => (x.division == store) && (x.IsSaved == false)).ToList().OrderBy(x => x.curNo).ToList();
                
                var maxObject = Helpers.Data.BranchOutDetailList.Where(x => x.division == store).OrderByDescending(item => item.curNo).FirstOrDefault();
                                
                if(Helpers.Data.EntryMode == "New")
                {
                    if (maxObject == null)
                    {
                        BranchOutDetail.vchrNo = "TO" + 1;
                        BranchOutDetail.curNo = 1;
                        GrnCount = 1;                        
                    }
                    else
                    {
                        BranchOutDetail.vchrNo = "TO" + (Convert.ToInt32(maxObject.curNo) + 1);
                        GrnCount = maxObject.curNo + 1;
                        BranchOutDetail.curNo = GrnCount;
                    }

                    BranchOutDetailList.Add(BranchOutDetail);
                    SelectedBranchOutDetail = BranchOutDetailList.Where(x => x.vchrNo == BranchOutDetail.vchrNo).ToList().FirstOrDefault();
                }                        
                
            }catch
            { }           
        }
       
        public void ExecuteBOSetCommand()
        {
            try
            {
                var response = BranchOutDetailValidator.CheckInputData(BranchOutDetail);
                if (response.status == "error")
                    DependencyService.Get<IMessage>().ShortAlert(response.Message);
                else if (response.status == "ok")
                {     
                    var res = InsertIntoDB.InsertBranchOutDetail(App.DatabaseLocation, BranchOutDetail);
                    if(res)
                        App.Current.MainPage = new BranchOutTabbedPage();
                    else
                        DependencyService.Get<IMessage>().ShortAlert("Error while inserting data");
                }
            }
            catch(Exception e)
            { }           
        }       

        public void PickerValueChange(BranchOutDetail BranchOutDetail)
        {
            try
            {
                SelectedWarehouse = WarehouseList.Find(x => x.NAME == BranchOutDetail.wareHouse);
                SelectedDivisionTo = DivisionList.Find(x => x.INITIAL == BranchOutDetail.billToAdd);               
            }
            catch { }           

        }

       
    }

   
}
