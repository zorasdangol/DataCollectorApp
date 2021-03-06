﻿using DataCollector.ViewModels.ViewDownloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataCollector.Views.ViewDownloads
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBarCodePage : ContentPage
    {
        public ViewBarCodePageVM viewModel { get; set; }

        public ViewBarCodePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewBarCodePageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            //viewModel.RefreshItem();
            //base.OnAppearing();
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}