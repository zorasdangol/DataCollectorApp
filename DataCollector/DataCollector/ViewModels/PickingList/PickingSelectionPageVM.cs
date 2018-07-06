using DataCollector.DatabaseAccess;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.PickingList
{
    public class PickingSelectionPageVM:BaseViewModel
    {
        private List<PickingDetail> _PickingDetailList;
        public List<PickingDetail> PickingDetailList
        {
            get { return _PickingDetailList; }
            set
            {
                _PickingDetailList =  value;
                OnPropertyChanged("PickingDetailList");
            }
        }

        private PickingDetail _SelectedPickingDetail;
        public PickingDetail SelectedPickingDetail
        {
            get { return _SelectedPickingDetail; }
            set
            {
                _SelectedPickingDetail = value;
                OnPropertyChanged("SelectedPickingDetail");
            }
        }

        public Command SelectCommand { get; set; }

        public PickingSelectionPageVM()
        {
            PickingDetailList = Helpers.JsonData.PickingDetailList;
            SelectCommand = new Command(ExecuteSelectCommand);
        }

        private void ExecuteSelectCommand()
        {
            Helpers.Data.SelectedPickingDetail = SelectedPickingDetail;
            //LoadFromDB.LoadStockTakeMaxList(App.DatabaseLocation, SelectedPickingDetail);

            Helpers.Data.SelectedStockTakeMaxList = Helpers.JsonData.StockTakeMaxList.Where(x => x.pickNo == SelectedPickingDetail.pickNo).ToList();
        }
    }
}
