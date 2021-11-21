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
    public class CollectionSelectViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Models.Collection> collections;
        public ObservableCollection<Models.Collection> Collections { get => collections; set { collections = value; OnPropertyChanged(); } }


        private Command add;
        public Command Add { get => add; set { add = value; OnPropertyChanged(); } }

        public CollectionSelectViewModel()
        {
            Collections = new ObservableCollection<Models.Collection>(Singleton.Storage.Collections);
            Add = new Command(() =>
            {
                Models.Collection c = new Models.Collection() 
                {
                    Id = Singleton.Storage.generateId(),
                    Name = "efefwsedfw", 
                    Cards = new Dictionary<Models.Card, int>() 
                };
                Singleton.Storage.AddCollection(c);
                Collections.Add(c);
            });
        }




        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
