using DataCollector.Views;
using DataCollector.Views.BranchOut;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.BranchOut
{
    public class BranchOutTabbedPageVM:BaseViewModel
    {
        private List<MasterMenu> _GrnMenuList;
        public List<MasterMenu> GrnMenuList
        {
            get { return _GrnMenuList; }
            set
            {
                _GrnMenuList = value;
                OnPropertyChanged("GrnMenuList");
            }
        }

        private MasterMenu _SelectedMenuItem;
        
        public MasterMenu SelectedMenuItem
        {
            get { return _SelectedMenuItem; }
            set
            {
                if (SelectedMenuItem != value && value != null && value.Index > 0)
                {
                    _SelectedMenuItem = value;
                    NavigateToPage();
                    OnPropertyChanged("SelectedMenuItem");
                }
                else
                {
                    _SelectedMenuItem = null;
                }
            }
        }

        public BranchOutTabbedPageVM()
        {
            GrnMenuList = Helpers.Data.GrnMenuList;
        }

        public void NavigateToPage()
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = false;
            if (SelectedMenuItem.Index == 1)
            {
                Helpers.Data.AutoModeEnabled = false;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchOutTabbedPage());
            }
            else if (SelectedMenuItem.Index == 2)
            {
                Helpers.Data.AutoModeEnabled = true;
                (App.Current.MainPage as MasterDetailPage).Detail = (new BranchOutTabbedPage());
            }
            else if (SelectedMenuItem.Index == 4)
            {
                App.Current.MainPage = new MasterPage();
            }

        }
    }
}
