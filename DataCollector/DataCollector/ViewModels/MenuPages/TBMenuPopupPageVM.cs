
using DataCollector.Views.MenuPages;
using DataCollectorStandardLibrary.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.MenuPagesVM
{
    public class TBMenuPopupPageVM : BaseViewModel
    {
        private List<MasterMenu> _TBMenuItems;
        public List<MasterMenu> TBMenuItems
        {
            get { return _TBMenuItems; }
            set { _TBMenuItems = value; OnPropertyChanged("TBMenuItems"); }
        }

        private MasterMenu _SelectedTBItem;
        public MasterMenu SelectedTBItem
        {
            get { return _SelectedTBItem; }
            set
            {
                try
                {
                    if (SelectedTBItem != value && value != null && value.Index > 0)
                    {
                        _SelectedTBItem = value;
                        //Helpers.Data.Data.SelectedItem = value;
                        NavigateToPage();
                        OnPropertyChanged("SelectedTBItem");
                    }
                    else
                    {
                        _SelectedTBItem = null;
                    }
                }
                catch { }               
            }
        }

        public TBMenuPopupPageVM()
        {
            TBMenuItems = Helpers.Data.TBMenuItems;
        }
        
        private async void NavigateToPage()
        {
            try
            {
                switch (SelectedTBItem.Index)
                {
                    case 1:
                        await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PopPopupAsync();
                        //await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new MainSettingsPage());
                        break;
                    case 2:
                        await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PopPopupAsync();
                        //await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushPopupAsync(new DeveloperSettingsPopupPage());
                        break;
                    case 3:
                        await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PopPopupAsync();
                        await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushPopupAsync(new IPSettingPopupPage());
                        break;
                }
            }
            catch { }
        }
    }
}
