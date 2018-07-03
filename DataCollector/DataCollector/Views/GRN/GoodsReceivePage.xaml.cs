using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.ViewModels.GRN;

namespace DataCollector.Views.GRN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodsReceivePage : ContentPage
    {
        public GoodsReceivePageVM viewModel { get; set; }

        public GoodsReceivePage()
        {            
            InitializeComponent();
            BindingContext = viewModel = new GoodsReceivePageVM();
        }
        
        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MasterPage();
            return true;
        }

        private void StorePicker_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (Helpers.Data.EntryMode == "Edit" && !string.IsNullOrEmpty(viewModel.GrnMain.division))
                {
                    EntryNoPicker.Focus();
                }

            }
            catch { }

        }
    }
}