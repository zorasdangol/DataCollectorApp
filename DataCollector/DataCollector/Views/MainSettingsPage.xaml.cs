using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.ViewModels;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainSettingsPage : ContentPage
    {
        public MainSettingsPageVM viewModel { get; set; }
        public MainSettingsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainSettingsPageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }

    }
}