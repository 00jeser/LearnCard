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
        private ObservableCollection<Models.Collection> _collections;
        public ObservableCollection<Models.Collection> Collections { get => _collections; set { _collections = value; OnPropertyChanged(); } }


        private Command _doAdd;
        public Command DoAdd { get => _doAdd; set { _doAdd = value; OnPropertyChanged(); } }
        private Command _doMinus;
        public Command DoMinus { get => _doMinus; set { _doMinus = value; OnPropertyChanged(); } }
        private Command _doAddShow;
        public Command DoAddShow { get => _doAddShow; set { _doAddShow = value; OnPropertyChanged(); } }
        private Command _doMinusShow;
        public Command DoMinusShow { get => _doMinusShow; set { _doMinusShow = value; OnPropertyChanged(); } }

        private Command<Models.Collection> _openCollection;
        public Command<Models.Collection> OpenCollection { get => _openCollection; set { _openCollection = value; OnPropertyChanged(); } }

        public List<string> NamesList { get => Collections.Select(x => x.Name).ToList(); }

        private bool _showMinus;
        public bool ShowMinus { get => _showMinus; set { _showMinus = value; OnPropertyChanged();} }

        private int _minusIndex;
        public int MinusIndex { get => _minusIndex; set { _minusIndex = value; OnPropertyChanged(); } }

        private bool _addShow;
        public bool AddShow { get => _addShow; set { _addShow = value; OnPropertyChanged();} }

        private string _addName;
        public string AddName { get => _addName; set { _addName = value; OnPropertyChanged(); } }

        public CollectionsListViewModel()
        {
            Collections = Singleton.Storage.Collections;

            DoAddShow = new Command(() =>
            {
                AddShow = true;
            });
            DoAdd = new Command(() =>
            {
                if (string.IsNullOrWhiteSpace(_addName))
                    return;
                AddShow = false;
                Models.Collection c = new Models.Collection()
                {
                    Id = Singleton.Storage.GenerateId(),
                    Name = _addName,
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
                if (_minusIndex == -1)
                    return;
                ShowMinus = false;
                Singleton.Storage.DeleteCollectionById(_collections[_minusIndex].Id);
            });

            OpenCollection = new Command<Models.Collection>(o => {
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
