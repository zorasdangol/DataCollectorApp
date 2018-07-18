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
	public partial class BranchInItemListPage : ContentPage
	{
        public BranchInItemListPageVM viewModel { get; set; }
		public BranchInItemListPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new BranchInItemListPageVM();
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