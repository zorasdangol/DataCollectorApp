using DataCollector.ViewModels.DataSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.DataSync
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockSyncPage : ContentPage
    {
        public StockSyncPageVM viewModel { get; set; }
        public StockSyncPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StockSyncPageVM();
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}