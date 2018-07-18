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
	public partial class BranchInTabbedPage : MasterDetailPage
	{
        public BranchInTabbedPageVM viewModel { get; set; }
		public BranchInTabbedPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new BranchInTabbedPageVM();
            listView.SelectedItem = null;
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            listview.SelectedItem = null;
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MasterPage();
            return true;
        }
    }
}