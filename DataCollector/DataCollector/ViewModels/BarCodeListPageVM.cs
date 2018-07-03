using DataCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels
{
    public class BarCodeListPageVM : BaseViewModel
    {
        private List<OrderProd> _SelectedOrderProdList;
        public List<OrderProd> SelectedOrderProdList
        {
            get { return _SelectedOrderProdList; }
            set
            {
                _SelectedOrderProdList = value;
                OnPropertyChanged("SelectedOrderProdList");
            }
        }
        public BarCodeListPageVM()
        {
            SelectedOrderProdList = Helpers.Data.SelectedOrderProdList;
        }

        public void RefreshItem()
        {
            SelectedOrderProdList = Helpers.Data.SelectedOrderProdList;
        }
    }
}
