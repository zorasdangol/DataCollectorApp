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
    public partial class BranchOutItemListPage : ContentPage
    {
        public BranchOutItemListPageVM viewModel { get; set; }
        public BranchOutItemListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BranchOutItemListPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshItem();
            //base.OnAppearing();

        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }

    }
}