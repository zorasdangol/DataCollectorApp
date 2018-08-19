using DataCollector.DatabaseAccess;
using DataCollector.Interfaces;
using DataCollector.Views;
using DataCollectorStandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataCollector.ViewModels.SessionPages
{
    public class SessionSelectionPageVM:BaseViewModel
    {
        private List<Session> _SessionList;
        public List<Session> SessionList
        {
            get { return _SessionList; }
            set
            {
                _SessionList = value;
                OnPropertyChanged("SessionList");
            }
        }

        private Session _SelectedSession;
        public Session SelectedSession
        {
            get { return _SelectedSession; }
            set
            {
                try
                {
                    _SelectedSession = value;
                    if (value != null && value.SESSIONID != 0)
                    {
                        var ses = Helpers.Data.Session;
                        if (value.SESSIONID != Helpers.Data.Session.SESSIONID)
                        {
                            ChangeSession(value);                            
                        }
                        else
                        {
                            DependencyService.Get<IMessage>().ShortAlert("Selected Session is running");
                        }
                    }
                    OnPropertyChanged("SelectedSession");
                }
                catch { }               
            }
        }
        public Command DeleteCommand { get; set; }

        public SessionSelectionPageVM()
        {
            LoadFromDB.LoadSession(App.DatabaseLocation);
            SessionList = LoadFromDB.LoadSessionList(App.DatabaseLocation);
            DeleteCommand = new Command(ExecuteDeleteCommand);
        }

        public void RefreshItem()
        {
            SessionList = LoadFromDB.LoadSessionList(App.DatabaseLocation);
        }

        public async void ExecuteDeleteCommand()
        {
            var result = await App.Current.MainPage.DisplayAlert("Choose", "Are you sure to delete? ", "Yes", "No");
            if (result)
            {
                Helpers.Data.Session = null;
                ClearFromDB.ClearSessionAll(App.DatabaseLocation);
                DependencyService.Get<IMessage>().LongAlert("All Session Cleared Successfully");
                this.RefreshItem();
                //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
            }
        }

        public async void ChangeSession(Session value)
        {
            try
            {
                var response = await App.Current.MainPage.DisplayAlert("Choice", "Are you sure to Select Session?", "Yes", "No");
                if (response)
                {
                    DBAccess.EndSession(App.DatabaseLocation, Helpers.Data.Session);
                    var res = DBAccess.SelectedSession(App.DatabaseLocation, value);
                    if (res)
                    {
                        SessionList = LoadFromDB.LoadSessionList(App.DatabaseLocation);
                        DependencyService.Get<IMessage>().ShortAlert("Session Selected");
                        SelectedSession = new Session();
                        //(App.Current.MainPage as MasterDetailPage).Detail = new NavigationPage(new MainHomePage());
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().ShortAlert("Could not select session");
                    }
                }
                else
                {
                    //SessionList = Helpers.Data.SessionList;
                    SelectedSession = new Session();
                }
            }
            catch { }
        }
    }
}
