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
	public partial class BranchInSummaryPage : ContentPage
	{
        public BranchInSummaryPageVM viewModel { get; set; }
		public BranchInSummaryPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new BranchInSummaryPageVM();
		}

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshBranchInSummaryList();
            base.OnAppearing();
        }
    }
}