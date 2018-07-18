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
	public partial class BranchInDetailPage : ContentPage
	{
        public BranchInDetailPageVM viewModel { get; set; }
		public BranchInDetailPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new BranchInDetailPageVM();
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
                if (Helpers.Data.EntryMode == "Edit" && !string.IsNullOrEmpty(viewModel.BranchInDetail.division))
                {
                    EntryNoPicker.Focus();
                }
            }
            catch { }
        }

    }
}