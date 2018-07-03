using DataCollector.ViewModels.GRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.GRN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GRNSummaryPage : ContentPage
	{
        public GRNSummaryPageVM viewModel { get; set; }
		public GRNSummaryPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new GRNSummaryPageVM();
		}

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshStockSummaryList();
            base.OnAppearing();
        }
    }
}