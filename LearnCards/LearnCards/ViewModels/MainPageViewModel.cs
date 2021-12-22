using LearnCards.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Models.Collection> _collections;
        public ObservableCollection<Models.Collection> Collections
        {
            get { return _collections; }
            set { _collections = value; OnPropertyChanged(); }
        }

        public Command DoAdd { get; set; }
        public Command DoDelete { get; set; }
        public Command DoShowAdd { get; set; }
        public Command LongPressCommand { get; set; }
        public Command SelectionChange { get; set; }

        private SelectionMode _selectionMode = SelectionMode.Single;
        public SelectionMode SelectionMode { get => _selectionMode; set { _selectionMode = value; OnPropertyChanged(); } }

        private ObservableCollection<object> selectedCollections;
        public ObservableCollection<object> SelectedCollections { get => selectedCollections; set { selectedCollections = value; OnPropertyChanged(); } }

        public object SelectedCollection
        {
            set
            {
                if (value != null)
                    Shell.Current.GoToAsync($"//Edit?id={(value as Models.Collection).Id}");
            }
        }


        private string addName;
        public string AddName
        {
            get { return addName; }
            set { addName = value; OnPropertyChanged(); }
        }

        private bool addShow;
        public bool AddShow
        {
            get { return addShow; }
            set { addShow = value; OnPropertyChanged(); }
        }

        private bool showDelButton;

        public bool ShowDelButton
        {
            get { return showDelButton; }
            set { showDelButton = value; OnPropertyChanged(); }
        }



        public MainPageViewModel()
        {
            Collections = Singleton.Storage.GetCollections();
            SelectedCollections = new ObservableCollection<object>();

            SelectionChange = new Command(() =>
            {
                var c = SelectedCollections.Count;
                if (c == 0)
                {
                    SelectionMode = SelectionMode.Single;
                    ShowDelButton = false;
                }
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

            DoDelete = new Command(() =>
            {
                foreach(var c in SelectedCollections)
                {
                    Singleton.Storage.DeleteCollectionById((c as Models.Collection).Id);
                }
            });

            DoShowAdd = new Command(() =>
            {
                AddShow = true;
            });

            LongPressCommand = new Command<Models.Collection>((Models.Collection col) =>
            {
                SelectedCollections.Clear();
                SelectedCollections.Add(col);
                SelectionMode = SelectionMode == SelectionMode.Single ? SelectionMode.Multiple : SelectionMode.Single;
                ShowDelButton = SelectionMode == SelectionMode.Multiple;
            });
        }

    }
}
