using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using DataCollector.Views.MenuPages;
using DataCollectorStandardLibrary.DataAccessLayer;
using DataCollector.Interfaces;
using DataCollector.Views;

namespace DataCollector.ViewModels
{
    public class MainHomePageVM:BaseViewModel
    {
        public Command TBMenuCommand { get; set; }

        public MainHomePageVM()
        {
            TBMenuCommand = new Command<string>(ExecuteTBMenuCommand);                       
        }
        
        public  void ExecuteTBMenuCommand(string value)
        {
            if(value == "1")
            {
                (App.Current.MainPage as MasterDetailPage).Detail = new IPSettingsPage();
            }
            else if(value == "2")
            {
                (App.Current.MainPage as MasterDetailPage).Detail = new MainSettingsPage();
            }
            //await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushPopupAsync(new TBMenuPopupPage());            
        }

    }
}
