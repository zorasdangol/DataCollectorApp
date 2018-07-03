using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.ViewModels
{
    public class MainSettingsPageVM:BaseViewModel
    {
        private bool _AutoModeEnabled;

        public bool AutoModeEnabled
        {
            get { return _AutoModeEnabled; }
            set
            {
                _AutoModeEnabled = value;
                Helpers.Data.AutoModeEnabled = value;
                OnPropertyChanged("AutoModeEnabled");                
            }
        }

        public MainSettingsPageVM()
        {

            AutoModeEnabled = Helpers.Data.AutoModeEnabled;
        }
    }
}
