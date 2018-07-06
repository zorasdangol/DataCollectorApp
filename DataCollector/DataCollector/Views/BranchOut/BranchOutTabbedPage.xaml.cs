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
    public partial class BranchOutTabbedPage : MasterDetailPage
    {
        public BranchOutTabbedPageVM viewModel { get; set; }
        public BranchOutTabbedPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BranchOutTabbedPageVM();
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