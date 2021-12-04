using LearnCards.Services;
using LearnCards.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.ViewModels
{
    internal class CollectionsListViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Collection> collections;
        public ObservableCollection<Models.Collection> Collections { get => collections; set { collections = value; OnPropertyChanged(); } }


        private Command doAdd;
        public Command DoAdd { get => doAdd; set { doAdd = value; OnPropertyChanged(); } }
        private Command doMinus;
        public Command DoMinus { get => doMinus; set { doMinus = value; OnPropertyChanged(); } }
        private Command doAddShow;
        public Command DoAddShow { get => doAddShow; set { doAddShow = value; OnPropertyChanged(); } }
        private Command doMinusShow;
        public Command DoMinusShow { get => doMinusShow; set { doMinusShow = value; OnPropertyChanged(); } }

        private Command<Models.Collection> openCollection;
        public Command<Models.Collection> OpenCollection { get => openCollection; set { openCollection = value; OnPropertyChanged(); } }

        public List<string> NamesList { get => Collections.Select(x => x.Name).ToList(); }

        private bool showMinus;
        public bool ShowMinus { get => showMinus; set { showMinus = value; OnPropertyChanged();} }

        private int minusIndex;
        public int MinusIndex { get => minusIndex; set { minusIndex = value; OnPropertyChanged(); } }

        private bool addShow;
        public bool AddShow { get => addShow; set { addShow = value; OnPropertyChanged();} }

        private string addName;
        public string AddName { get => addName; set { addName = value; OnPropertyChanged(); } }

        public CollectionsListViewModel()
        {
            Collections = Singleton.Storage.Collections;

            DoAddShow = new Command(() =>
            {
                AddShow = true;
            });
            DoAdd = new Command(() =>
            {
                if (string.IsNullOrWhiteSpace(addName))
                    return;
                AddShow = false;
                Models.Collection c = new Models.Collection()
                {
                    Id = Singleton.Storage.generateId(),
                    Name = addName,
                    Cards = new Dictionary<Models.Card, int>()
                };
                Singleton.Storage.AddCollection(c);
                AddName = "";
            });
            DoMinusShow = new Command(() =>
            {
                ShowMinus = true;
                OnPropertyChanged(nameof(NamesList));
            });
            DoMinus = new Command(() =>
            {
                if (minusIndex == -1)
                    return;
                ShowMinus = false;
                Singleton.Storage.DeleteCollectionById(collections[minusIndex].Id);
            });

            OpenCollection = new Command<Models.Collection>((Models.Collection o) => {
                Shell.Current.GoToAsync($"//Edit?id={o.Id}");
            });
        }
        public void Back()
        {
            AddShow = false;
            ShowMinus = false;
        }
    }
}
