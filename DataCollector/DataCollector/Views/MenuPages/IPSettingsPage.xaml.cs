using DataCollector.ViewModels.MenuPagesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IPSettingsPage : ContentPage
    {
        public IPSettingsPageVM viewModel { get; set; }
        public IPSettingsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new IPSettingsPageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }


    }
}