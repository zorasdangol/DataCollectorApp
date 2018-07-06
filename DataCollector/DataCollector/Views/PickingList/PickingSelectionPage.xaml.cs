using DataCollector.ViewModels.PickingList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.PickingList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickingSelectionPage : ContentPage
    {
        public PickingSelectionPageVM viewModel { get; set; }
        public PickingSelectionPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PickingSelectionPageVM();
        }
    }
}