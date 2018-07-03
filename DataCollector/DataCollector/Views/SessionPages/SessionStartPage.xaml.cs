using DataCollector.ViewModels;
using DataCollector.ViewModels.SessionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.SessionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionStartPage : ContentPage
    {
        public SessionStartPageVM viewModel { get; set; }
        public SessionStartPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SessionStartPageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }
    }
}