using DataCollector.ViewModels.DataList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.DataList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchOutDataListPage : ContentPage
    {
        public BranchOutDataListPageVM viewModel { get; set; }
        public BranchOutDataListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BranchOutDataListPageVM();
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