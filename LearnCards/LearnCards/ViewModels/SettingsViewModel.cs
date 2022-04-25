using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LearnCards.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public double LearnCount
        {
            get
            {
                return Preferences.Get("LearnCount", 3d);
            }
            set
            {
                Preferences.Set("LearnCount", value);
                OnPropertyChanged();
            }
        }
        public bool IsFrontSide
        {
            get
            {
                return Preferences.Get("IsFrontSide", true);
            }
            set
            {
                Preferences.Set("IsFrontSide", value);
                OnPropertyChanged();
            }
        }
    }
}
