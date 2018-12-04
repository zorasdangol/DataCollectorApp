using DataCollector.ViewModels.GRN;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.GRN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GRNEntryPage : ContentPage
	{
        public GRNEntryPageVM viewModel { get; set; }
		public GRNEntryPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new GRNEntryPageVM();
		}

        protected async override void OnAppearing()
        {
            viewModel.IsButtonVisible = !Helpers.Data.AutoModeEnabled;            
            base.OnAppearing();
            await Task.Delay(500);
            BarCodeEntry.Focus();
        }


        //private void BarCode_Entry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Helpers.Data.AutoModeEnabled)
        //        {
        //            if (!string.IsNullOrEmpty(e.NewTextValue))
        //            {
        //                var oldText = e.OldTextValue;
        //                var newText = e.NewTextValue;

        //                if (oldText == null) { oldText = ""; }
        //                if (newText == null) { newText = ""; }

        //                viewModel.BarCode_Entry_TextChanged(oldText, newText);
        //            }
        //        }
        //    }
        //    catch { }
        //}

        public void OnEnterPressed(object sender, EventArgs e)
        {
            viewModel.OnEnterPressed();
            if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
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
            viewModel.SavingGrnToSqlite();
            if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
            {
                BarCodeEntry.Focus();
            }            
        }

        protected override void OnDisappearing()
        {
            BarCodeEntry.Unfocus();
        }


    }

    
}