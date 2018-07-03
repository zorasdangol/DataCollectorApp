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
    public partial class SessionEndPage : ContentPage
    {
        public SessionEndPageVM viewModel { get; set; }
        public SessionEndPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SessionEndPageVM();
        }
       
        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }
    }
}