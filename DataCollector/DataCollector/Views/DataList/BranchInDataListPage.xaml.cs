﻿using DataCollector.ViewModels.DataList;
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
    public partial class BranchInDataListPage : ContentPage
    {
        public BranchInDataListPageVM viewModel { get; set; }
        public BranchInDataListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BranchInDataListPageVM();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
            viewModel.RefreshItem();
            //base.OnAppearing();
        }

        public void MenuItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}