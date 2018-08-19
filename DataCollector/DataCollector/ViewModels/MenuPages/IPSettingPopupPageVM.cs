using DataCollector.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.MenuPagesVM
{
    public class IPSettingPopupPageVM : BaseViewModel
    {
        public Command ButtonCommand { get; set; }


        public IPSettingPopupPageVM()
        {
            ButtonCommand = new Command<string>(ExecuteButtonCommand);
        }

        public  void ExecuteButtonCommand(string value)
        {
            if(value == "1")
            {
               
            }
            else if(value == "2")
            {
                App.Current.MainPage = new MasterPage();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage()) ;
            }
        }
    }
}
