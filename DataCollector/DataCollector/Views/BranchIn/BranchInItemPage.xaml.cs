using DataCollector.ViewModels.BranchIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.BranchIn
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BranchInItemPage : ContentPage
	{
        public BranchInItemPageVM viewModel { get; set; }
		public BranchInItemPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new BranchInItemPageVM();
		}

        protected async override void OnAppearing()
        {
            viewModel.IsButtonVisible = !Helpers.Data.AutoModeEnabled;
            base.OnAppearing();
            await Task.Delay(500);
            BarCodeEntry.Focus();
        }

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