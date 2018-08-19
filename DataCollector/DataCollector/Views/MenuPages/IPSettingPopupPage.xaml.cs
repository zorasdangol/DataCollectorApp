using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.ViewModels.MenuPagesVM;

namespace DataCollector.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IPSettingPopupPage : PopupPage
    {
        IPSettingPopupPageVM viewModel { get; set; }
        public IPSettingPopupPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new IPSettingPopupPageVM();
        }

        protected override bool OnBackButtonPressed()
        {            
            //(App.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
            return true;
            //return base.OnBackButtonPressed();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //     //(App.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();

        //    //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
        //    return base.OnBackButtonPressed();
        //}
    }
}