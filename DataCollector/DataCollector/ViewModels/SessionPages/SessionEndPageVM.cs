
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollector.DatabaseAccess;
using DataCollector.Views.SessionPages;

namespace DataCollector.ViewModels.SessionPages
{
    public class SessionEndPageVM:BaseViewModel
    {
        private Session _Session;
        public Session Session
        {
            get { return _Session; }
            set
            {
                _Session = value;
                OnPropertyChanged("Session");
            }
        }

        public Command EndCommand { get; set; }


        public SessionEndPageVM()
        {
            EndCommand = new Command(ExecuteEndCommand);
            Session = LoadFromDB.LoadSession(App.DatabaseLocation);
            if(Session == null || string.IsNullOrEmpty(Session.USERNAME))
            {
                App.Current.MainPage.DisplayAlert("Info","There is no Session to end. Start the session First","ok");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionStartPage());
            }           
        }

        public void ExecuteEndCommand()
        {
            var res = DBAccess.EndSession(App.DatabaseLocation, Session);
            if (res == true)
            {
                DependencyService.Get<IMessage>().LongAlert("Session Ended");
                (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Session Couldnot be started. Sqlite Database error");
            }


        }

    }
}
