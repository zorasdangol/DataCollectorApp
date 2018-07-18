using DataCollector.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPageVM viewModel { get; set; }
        public MasterPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel = new MasterPageVM();
                this.Detail = new NavigationPage(new MainHomePage());
                this.listView.SelectedItem = null;
            }
            catch { }
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            listview.SelectedItem = null;
        }
    }
}