using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.Interfaces;
using DataCollector.ViewModels;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainHomePage : ContentPage
    {
        public MainHomePageVM viewModel { get; set; }
        public bool BExit { get; set; }
        public MainHomePage()
        {
            BExit = true;
            InitializeComponent();
            BindingContext = viewModel = new MainHomePageVM();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BExit)
            {
                DependencyService.Get<IMessage>().ShortAlert("Press again to exit");
                BExit = false;
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}