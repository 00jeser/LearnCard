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

        private Command delete;
        public Command Delete { get { return delete; } set { delete = value; OnPropertyChanged(); } }

        private Command add;
        public Command Add { get { return add; } set { add = value; OnPropertyChanged(); } }

        private Command doAddShow;
        public Command DoAddShow { get { return doAddShow; } set { doAddShow = value; OnPropertyChanged(); } }

        private bool addShow;
        public bool AddShow { get { return addShow; } set { addShow = value; OnPropertyChanged(); } }

        private string inputField1;
        private string inputField2;
        public string InputField1 { get { return inputField1; } set { inputField1 = value; OnPropertyChanged(); } }
        public string InputField2 { get { return inputField2; } set { inputField2 = value; OnPropertyChanged(); } }

        public List<Models.Card> Cards { get => Collection.Cards.Keys.ToList(); }

        public CollectionEditViewModel(Models.Collection c)
        {
            Collection = c;
            Delete = new Command<Models.Card>((Models.Card card) =>
            {
                //Collection.Cards.Remove(card);
                Singleton.Storage.DeleteCardById(Collection, card.id);
                OnPropertyChanged(nameof(Cards));
            });
            DoAddShow = new Command<Models.Card>((Models.Card card) =>
            {
                AddShow = true;
            });
            Add = new Command(() =>
            {
                var card = new Models.Card()
                {
                    Field1 = InputField1,
                    Field2 = InputField2,
                    id = Singleton.Storage.generateId()
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
