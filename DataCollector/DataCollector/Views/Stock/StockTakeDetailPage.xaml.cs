using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.ViewModels.Stock;

namespace DataCollector.Views.Stock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockTakeDetailPage : ContentPage
    {
        public StockTakeDetailPageVM viewModel { get; set; }
        public StockTakeDetailPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StockTakeDetailPageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MasterPage();
            return true;
        }

        private void StorePicker_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (Helpers.Data.EntryMode == "Edit" && !string.IsNullOrEmpty(viewModel.StockTake.division))
                {
                    EntryNoPicker.Focus();
                }

            }
            catch { }

        }
    }
}