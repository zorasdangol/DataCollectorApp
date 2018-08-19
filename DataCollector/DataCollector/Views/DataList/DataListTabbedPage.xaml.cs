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
    public partial class DataListTabbedPage : TabbedPage
    {
        public DataListTabbedPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            return true;
        }
    }
}