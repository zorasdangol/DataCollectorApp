using DataCollector.ViewModels.SessionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.SessionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionSelectionPage : ContentPage
    {
        public SessionSelectionPageVM viewModel { get; set; }
        public SessionSelectionPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SessionSelectionPageVM();
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}