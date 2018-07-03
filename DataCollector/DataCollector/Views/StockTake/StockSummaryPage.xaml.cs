using DataCollector.ViewModels;
using DataCollector.ViewModels.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.Stock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockSummaryPage : ContentPage
    {
        public StockSummaryPageVM viewModel { get; set; }
        public StockSummaryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StockSummaryPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshStockSummaryList();
            base.OnAppearing();
        }
    }
}