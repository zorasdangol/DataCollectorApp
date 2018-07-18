using DataCollector.DatabaseAccess;
using DataCollector.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DataCollector
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new DataCollector.Views.MasterPage();
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = (new MasterPage());

            DatabaseLocation = databaseLocation;

            new LoadInitialList(App.DatabaseLocation);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
