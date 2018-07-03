using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.ViewModels.GRN;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.GRN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GRNListPage : ContentPage
	{
        public GRNListPageVM viewModel { get; set; }
		public GRNListPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new GRNListPageVM();
		}

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshItem();
            //base.OnAppearing();

        }

        public  void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;    
        }


        //public async Task DataDeletion(GrnEntry Selected)
        //{
        //    try
        //    {
        //        var res = await App.Current.MainPage.DisplayAlert("Choose", "Do you want to Delete?", "Yes", "No");
        //        if (res)
        //        {
        //            ClearFromDB.DeleteGrnData(App.DatabaseLocation, Selected);
        //            Helpers.Data.GrnDataList.Remove(Selected);
        //            DependencyService.Get<IMessage>().ShortAlert("Deleted Item Successfully");
        //            viewModel.RefreshItem();
        //            InitializeComponent();
        //        }
        //    }
        //    catch(Exception e){ }
            
        //}

    }
}