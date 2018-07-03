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
    public partial class BatchEntryPage : ContentPage
    {
        public BatchEntryPageVM viewModel { get; set; } 
        public BatchEntryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BatchEntryPageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }
    }
}