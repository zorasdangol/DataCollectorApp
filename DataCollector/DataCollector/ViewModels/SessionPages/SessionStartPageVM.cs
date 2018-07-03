using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollectorStandardLibrary.Models;
using DataCollector.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.SessionPages
{
    public class SessionStartPageVM:BaseViewModel
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

        private bool _IsButtonEnabled;
        public bool IsButtonEnabled
        {
            get { return _IsButtonEnabled; }
            set
            {
                _IsButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }

        }

        public Command StartCommand { get; set; }

        public SessionStartPageVM()
        {
            StartCommand = new Command(ExecuteStartCommand);
            Session = LoadFromDB.LoadSession(App.DatabaseLocation);
            if (Session != null )
            {
                IsButtonEnabled = false;
                //DependencyService.Get<IMessage>().LongAlert("Session already started. First End the session");
                App.Current.MainPage.DisplayAlert("Info", "Session already started. First End the session", "ok");
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new SessionEndPage());
            }
            else
            {
                IsButtonEnabled = true;                     
                Session = new Session();
            }
        }

        public void ExecuteStartCommand()
        {
            //if (string.IsNullOrEmpty(Session.SESSIONID))
            //{
            //    DependencyService.Get<IMessage>().ShortAlert("Enter Session ID");
            //}
             if (string.IsNullOrEmpty(Session.USERNAME))
            {
                DependencyService.Get<IMessage>().ShortAlert("Enter User Name");
            }
            else
            {
                var res = InsertIntoDB.InsertSession(App.DatabaseLocation, Session);
                if(res == true)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Session Started");
                    (App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Session Couldnot be started. Sqlite Database error");
                }
            }
        }


    }
}
