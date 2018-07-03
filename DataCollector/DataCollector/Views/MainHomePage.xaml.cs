using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataCollector.Interfaces;

namespace DataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainHomePage : ContentPage
    {

        public bool BExit { get; set; }
        public MainHomePage()
        {
            BExit = true;
            InitializeComponent();
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