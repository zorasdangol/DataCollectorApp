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
    public partial class TabbedStockPage : MasterDetailPage
    {
        public TabbedStockPageVM viewModel { get; set; }
        public TabbedStockPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new TabbedStockPageVM();
            this.listView.SelectedItem = null;
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            listview.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MasterPage();
            return true;
        }
    }
}