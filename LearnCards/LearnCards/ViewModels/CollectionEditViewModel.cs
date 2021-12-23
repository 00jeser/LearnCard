using LearnCards.Services;
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
    public class CollectionEditViewModel : ViewModelBase
    {
        private Models.Collection _collection;
        public Models.Collection Collection { get { return _collection; } set { _collection = value; OnPropertyChanged(); } }

        private Command _delete;
        public Command Delete { get { return _delete; } set { _delete = value; OnPropertyChanged(); } }

        private Command _add;
        public Command Add { get { return _add; } set { _add = value; OnPropertyChanged(); } }

        private Command _doAddShow;
        public Command DoAddShow { get { return _doAddShow; } set { _doAddShow = value; OnPropertyChanged(); } }

        public Command DoLearn { get; set; }

        private bool _addShow;
        public bool AddShow { get { return _addShow; } set { _addShow = value; OnPropertyChanged(); } }

        private string _inputField1;
        private string _inputField2;
        public string InputField1 { get { return _inputField1; } set { _inputField1 = value; OnPropertyChanged(); } }
        public string InputField2 { get { return _inputField2; } set { _inputField2 = value; OnPropertyChanged(); } }

        public List<Models.Card> Cards { get => Collection.Cards.Keys.ToList(); }

        public CollectionEditViewModel(Models.Collection c)
        {
            Collection = c;
            DoLearn = new Command(() =>
            {
                Shell.Current.GoToAsync($"//Learn?id={c.Id}");
            });
            Delete = new Command<Models.Card>(card =>
            {
                //Collection.Cards.Remove(card);
                Singleton.Storage.DeleteCardById(Collection, card.Id);
                OnPropertyChanged(nameof(Cards));
            });
            DoAddShow = new Command<Models.Card>(card =>
            {
                AddShow = true;
            });
            Add = new Command(() =>
            {
                var card = new Models.Card()
                {
                    Field1 = InputField1,
                    Field2 = InputField2,
                    Id = Singleton.Storage.GenerateId()
                };
                Singleton.Storage.AddCard(Collection, card);
                OnPropertyChanged(nameof(Cards));
                AddShow = false;
                InputField1 = String.Empty;
                InputField2 = String.Empty;
            });

        }
    }
}
