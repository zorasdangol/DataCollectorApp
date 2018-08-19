using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DataCollectorStandardLibrary.Helpers.Data;
using DataCollector.DatabaseAccess;

namespace DataCollector.ViewModels.MenuPagesVM
{
    public class IPSettingsPageVM:BaseViewModel
    {
        private string _ip1;
        public string ip1
        {
            get { return _ip1; }
            set { _ip1 = value; OnPropertyChanged("ip1"); }
        }

        private string _ip2;
        public string ip2
        {
            get { return _ip2; }
            set { _ip2 = value; OnPropertyChanged("ip2"); }
        }

        private string _ip3;
        public string ip3
        {
            get { return _ip3; }
            set { _ip3 = value; OnPropertyChanged("ip3"); }
        }

        private string _ip4;
        public string ip4
        {
            get { return _ip4; }
            set { _ip4 = value; OnPropertyChanged("ip4"); }
        }

        private string _Port;
        public string Port
        {
            get { return _Port; }
            set { _Port = value; OnPropertyChanged("Port"); }
        }

        private string _ip;
        public string ip
        {
            get { return _ip; }
            set { _ip = value; OnPropertyChanged("ip"); }
        }

        public string ipAddress { get; set; }

        public Command IPSetCommand { get; set; }

        public IPSettingsPageVM()
        {
            ip = Constants.ipAddress;
            ip1 = "192";
            ip2 = "168";
            ip3 = "0";
            ip4 = "131";
            Port = "8080";
            IPSetCommand = new Command(SetApi);
        }

        public async void SetApi()
        {
            try
            {
                if (string.IsNullOrEmpty(ip1) || string.IsNullOrEmpty(ip2) || string.IsNullOrEmpty(ip3) || string.IsNullOrEmpty(ip4))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Fill the ip address", "OK");
                }
                else
                {
                    var res = await App.Current.MainPage.DisplayAlert("Confirm", "Are you sure to change Ip Address?", "Yes","No");
                    if (res)
                    {
                        if (string.IsNullOrEmpty(Port))
                        {
                            ipAddress = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                        }
                        else
                        {
                            ipAddress = ip1 + "." + ip2 + "." + ip3 + "." + ip4 + ":" + Port;
                        }
                        bool result = LoadIPAddress.SetIPAddress(App.DatabaseLocation,ipAddress);
                        if(result)
                        {
                            ip = Constants.ipAddress = ipAddress;
                            Constants.SetMainURL(Constants.ipAddress); 
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}
