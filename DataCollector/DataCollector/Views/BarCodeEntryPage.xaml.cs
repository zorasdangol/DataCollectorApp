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
    public partial class BarCodeEntryPage : ContentPage
    {
        public BarCodeEntryPageVM viewModel { get; set; }
        public BarCodeEntryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BarCodeEntryPageVM();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;

            viewModel.Entry_TextChanged(oldText, newText);
        }

    }
}