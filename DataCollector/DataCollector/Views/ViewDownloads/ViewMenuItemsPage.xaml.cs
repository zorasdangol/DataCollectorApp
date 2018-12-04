using DataCollector.ViewModels.ViewDownloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.ViewDownloads
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMenuItemsPage : ContentPage
    {
        public ViewMenuItemsPageVM viewModel { get; set; }

        public ViewMenuItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewMenuItemsPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            //viewModel.RefreshItem();
            //base.OnAppearing();
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}