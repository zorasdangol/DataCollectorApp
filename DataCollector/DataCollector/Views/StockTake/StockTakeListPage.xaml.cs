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
    public partial class StockTakeListPage : ContentPage
    {
        public StockTakeListPageVM viewModel { get; set; }
        public StockTakeListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StockTakeListPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshItem();
            //base.OnAppearing();

        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
            viewModel.RefreshItem();
            InitializeComponent();
        }
    }
}