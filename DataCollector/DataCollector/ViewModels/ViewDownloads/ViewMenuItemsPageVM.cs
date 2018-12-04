using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels.ViewDownloads
{
    public class ViewMenuItemsPageVM : BaseViewModel
    {
        private List<MenuItem> _MenuItemsList;
        public List<MenuItem> MenuItemsList
        {
            get { return _MenuItemsList; }
            set
            {
                if (value == null)
                    return;
                _MenuItemsList = value;
                OnPropertyChanged("MenuItemsList");
            }
        }

        public ViewMenuItemsPageVM()
        {
            MenuItemsList = Helpers.Data.MenuItemsList.OrderBy(x=> x.MCODE).ToList();
        }
    }
}
