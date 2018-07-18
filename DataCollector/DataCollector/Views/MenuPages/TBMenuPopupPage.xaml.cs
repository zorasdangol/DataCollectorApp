using DataCollector.ViewModels.MenuPagesVM;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TBMenuPopupPage : PopupPage
    {
        public TBMenuPopupPageVM viewModel { get; set; }
        public TBMenuPopupPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel = new TBMenuPopupPageVM();              
            }
            catch (Exception ex)
            { }
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var listview = sender as ListView;
                listview.SelectedItem = null;
            }
            catch (Exception ex)
            { }
        }
    }
}