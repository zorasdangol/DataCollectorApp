using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.ViewDownloads
{
    public class ViewBarCodePageVM : BaseViewModel
    {
        private List<BarCode> _BarCodeList;
        public List<BarCode> BarCodeList
        {
            get { return _BarCodeList; }
            set
            {
                if (value == null)
                    return;
                _BarCodeList = value;
                OnPropertyChanged("BarCodeList");
            }
        }

        public ViewBarCodePageVM()
        {
            BarCodeList = Helpers.Data.BarCodeList.OrderBy(x => x.BCODE).ToList();
        }
    }
}
