using DataCollector.ViewModels.DataSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.DataSync
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataSyncTabbedPage : TabbedPage
    {
        public DataSyncTabbedPageVM viewModel { get; set; }
        public DataSyncTabbedPage()
        {
            BindingContext = viewModel = new DataSyncTabbedPageVM();
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }
    }
}