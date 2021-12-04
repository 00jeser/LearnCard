using LearnCards.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.ViewModels
{
    public class CollectionSelectViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Collection> collections;
        public ObservableCollection<Models.Collection> Collections { get => collections; set { collections = value; OnPropertyChanged(); } }

        private Command open;
        public Command OpenCollection { get => open; set { open = value; OnPropertyChanged(); } }

        public CollectionSelectViewModel()
        {
            OpenCollection = new Command<Models.Collection>((Models.Collection collection) => {
                Shell.Current.GoToAsync($"//Learn?id={collection.Id}");
            });
            Collections = Singleton.Storage.Collections;
        }
    }
}
