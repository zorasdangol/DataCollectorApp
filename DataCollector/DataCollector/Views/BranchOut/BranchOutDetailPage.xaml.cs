using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.ViewModels.GRN;

namespace DataCollector.Views.BranchOut
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchOutDetailPage : ContentPage
    {
       
        public BranchOutDetailPageVM viewModel { get; set; }

        public BranchOutDetailPage()
        {            
            InitializeComponent();
            BindingContext = viewModel = new BranchOutDetailPageVM();
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
                if (Helpers.Data.EntryMode == "Edit" && !string.IsNullOrEmpty(viewModel.BranchOutDetail.division))
                {
                    EntryNoPicker.Focus();
                }

            }
            catch { }
        }
    }
}