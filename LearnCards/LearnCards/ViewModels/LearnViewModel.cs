using LearnCards.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.ViewModels
{
    public class LearnViewModel : ViewModelBase
    {
        private Models.Collection _collection;
        public Models.Collection Collection { get { return _collection; } set { _collection = value; OnPropertyChanged(); } }

        private ObservableCollection<Models.Card> _tempCards;
        public ObservableCollection<Models.Card> TempCards
        {
            get => _tempCards;
            set
            {
                _tempCards = value;
                OnPropertyChanged();
            }
        }

        public Command Right { get; set; }
        public Command Left { get; set; }


        private int _done;
        private int _max;
        public LearnViewModel(int id)
        {
            Collection = Singleton.Storage.GetCollectionById(id);
            if (NeedUpdateAmo())
            {
                var cards = Collection.Cards.Keys.ToList();
                for (int i = 0; i < cards.Count; i++)
                {
                    Collection.Cards[cards[i]] = 0;
                }
                Singleton.Storage.SaveCardsInCollectionCollection(Collection);
            }
            var r = new Random();
            TempCards = new ObservableCollection<Models.Card>(Collection.Cards.Keys.OrderBy(x => r.Next()).Where(x => Collection.Cards[x] < 3).Take(5));
            Right = new Command<Models.Card>(c =>
            {
                TempCards.Remove(c);
                Collection.Cards[c] += 1;
                Singleton.Storage.SaveCard(Collection, c);
                _done++;
                _max++;
                if (TempCards.Count == 0)
                    if (NeedUpdateAmo())
                        Shell.Current.GoToAsync($"//Done?id={id}");
                    else
                        Shell.Current.GoToAsync($"//Stat?id={id}&done={_done}&max={_max}");
            });
            Left = new Command<Models.Card>(c =>
            {
                TempCards.Remove(c);
                _max++;
                if (TempCards.Count == 0)
                    Shell.Current.GoToAsync($"//Stat?id={id}&done={_done}&max={_max}");
            });
        }

        private bool NeedUpdateAmo()
        {
            foreach (var card in Collection.Cards.Keys)
            {
                if (Collection.Cards[card] < 3)
                    return false;
            }
            return true;
        }
    }
}
