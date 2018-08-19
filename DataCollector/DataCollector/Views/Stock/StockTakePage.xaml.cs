using DataCollector.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollectorStandardLibrary;
using DataCollector.ViewModels.Stock;

namespace DataCollector.Views.Stock
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockTakePage : ContentPage
    {
        public StockTakePageVM viewModel { get; set; }
        public StockTakePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StockTakePageVM();
        }


        protected override void OnAppearing()
        {
            viewModel.IsButtonVisible = !Helpers.Data.AutoModeEnabled;
            base.OnAppearing();
            BarCodeEntry.Focus();
        }

        //private void BarCode_Entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var oldText = e.OldTextValue;
        //    var newText = e.NewTextValue;

        //    if(oldText == null) { oldText = ""; }
        //    if(newText == null) { newText = ""; }
        //    viewModel.BarCode_Entry_TextChanged(oldText, newText);

        //}

        public void OnEnterPressed(object sender, EventArgs e)
        {            
            viewModel.OnEnterPressed();
            if(viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
            {
                BarCodeEntry.Focus();
            }
            else if (!Helpers.Data.AutoModeEnabled)
            {               
                QuantityEntry.Focus();
            }
        }

        public void Quantity_EnterPressed(object sender, EventArgs e)
        {
            viewModel.SaveToSqlite();
            if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
            {
                BarCodeEntry.Focus();
            }
        }

        protected override void OnDisappearing()
        {
            BarCodeEntry.Unfocus();
        }

        //public void OnUnFocused(object sender, EventArgs e)
        //{                       
        //    if(viewModel.SelectedBarCode != null && !string.IsNullOrEmpty(viewModel.SelectedBarCode.BCODE))
        //    {
        //        BarCodeEntry.Focus();                               
        //    }
        //    else
        //    {
        //        viewModel.OnEnterPressed();
        //        if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
        //        {
        //            BarCodeEntry.Focus();
        //        }
        //        else if (!Helpers.Data.AutoModeEnabled)
        //        {
        //            QuantityEntry.Focus();
        //        }
        //    }
        //}

    }
}