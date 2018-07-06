using DataCollector.ViewModels.BranchOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.BranchOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchOutSummaryPage : ContentPage
    {
        public BranchOutSummaryPageVM viewModel { get; set; }
        public BranchOutSummaryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BranchOutSummaryPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshStockSummaryList();
            base.OnAppearing();
        }
    }
}