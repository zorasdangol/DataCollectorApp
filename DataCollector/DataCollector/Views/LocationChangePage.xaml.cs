using DataCollector.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationChangePage : ContentPage
    {
        public LocationChangePageVM viewModel { get; set; }
        public LocationChangePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LocationChangePageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }

    }
}