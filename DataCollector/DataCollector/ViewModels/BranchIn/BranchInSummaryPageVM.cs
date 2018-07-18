using DataCollectorStandardLibrary.DataValidationLayer;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.BranchIn
{
    public class BranchInSummaryPageVM : BaseViewModel
    {

        private List<BranchInSummary> _BranchInSummaryList;
        public List<BranchInSummary> BranchInSummaryList
        {
            get { return _BranchInSummaryList; }
            set
            {
                _BranchInSummaryList = value;
                OnPropertyChanged("BranchInSummaryList");
            }
        }
        public BranchInSummaryPageVM()
        {
            RefreshBranchInSummaryList();
        }

        public void RefreshBranchInSummaryList()
        {
            try
            {
                BranchInSummaryList = new List<BranchInSummary>();

                BranchInSummaryList = BranchInDetailValidator.BranchInToBranchInSummary(Helpers.Data.SendBranchOutSummaryList, Helpers.Data.BranchInItemList);
                Helpers.Data.SendBranchOutSummaryList = BranchInSummaryList;        
            }
            catch (Exception e) { }

        }
    }
}
