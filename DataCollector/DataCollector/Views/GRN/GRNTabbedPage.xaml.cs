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
	public partial class GRNTabbedPage : MasterDetailPage
	{
        public GRNTabbedPageVM viewModel { get; set; }
		public GRNTabbedPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new GRNTabbedPageVM();
            this.listView.SelectedItem = null;
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