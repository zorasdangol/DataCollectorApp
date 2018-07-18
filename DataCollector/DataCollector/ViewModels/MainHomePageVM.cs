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

namespace DataCollector.ViewModels
{
    public class MainHomePageVM:BaseViewModel
    {
        public Command TBMenuCommand { get; set; }

        public MainHomePageVM()
        {
            TBMenuCommand = new Command<string>(ExecuteTBMenuCommand);                       
        }
        
        public async void ExecuteTBMenuCommand(string value)
        {
            //await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushPopupAsync(new TBMenuPopupPage());            
        }

    }
}
