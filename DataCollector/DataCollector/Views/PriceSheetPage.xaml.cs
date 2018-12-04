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
	public partial class PriceSheetPage : ContentPage
	{
        public PriceSheetPageVM viewModel { get; set; }
		public PriceSheetPage ()
		{
			InitializeComponent ();
            BarCodeEntry.Focus();
            BindingContext = viewModel = new PriceSheetPageVM();
		}

        protected async override void OnAppearing()
        {            
            base.OnAppearing();
            await Task.Delay(500);
            BarCodeEntry.Focus();
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MasterPage();
            return true;
        }

        public void OnEnterPressed(object sender, EventArgs e)
        {
            viewModel.OnEnterPressed();
            if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
            {
                BarCodeEntry.Focus();
            }
            //else if (!Helpers.Data.AutoModeEnabled)
            //{
            //    QuantityEntry.Focus();
            //}
        }

        //public void Quantity_EnterPressed(object sender, EventArgs e)
        //{
        //    //viewModel.SaveToSqlite();
        //    if (viewModel.SelectedBarCode != null && string.IsNullOrEmpty(viewModel.SelectedBarCode.MCODE))
        //    {
        //        BarCodeEntry.Focus();
        //    }
        //}
             
        private void BarCodeEntry_Focused_1(object sender, FocusEventArgs e)
        {
            viewModel.SelectedBarCode = new DataCollectorStandardLibrary.Models.BarCode();
        }
    }
}