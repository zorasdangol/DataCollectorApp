using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarCodeListPage : ContentPage
    {
        public BarCodeListPageVM viewModel { get; set; }
        public BarCodeListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BarCodeListPageVM();
        }


        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshItem();
            //base.OnAppearing();

        }
    }
}