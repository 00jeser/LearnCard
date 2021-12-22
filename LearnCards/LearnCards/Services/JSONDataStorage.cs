using LearnCards.Models;
using LearnCards.Models.JSONDecorators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace LearnCards.Services
{
    public class JSONDataStorage : IDataStorage
    {
        public ObservableCollection<Collection> Collections { get; }

        public JSONDataStorage()
        {
            Collections = new ObservableCollection<Collection>();
            if (File.Exists(JSONpath))
            {
                load();
            }
            else
            {
                //File.Create(JSONpath);
                File.WriteAllText(JSONpath, "[]");
            }
        }

        void IDataStorage.AddCard(Collection collection, Card card)
        {
            Collections.First(x => x.Id == collection.Id).Cards[card] = 0;
            save();
        }

        void IDataStorage.AddCollection(Collection collection)
        {
            Collections.Add(collection);
            save();
        }

        void IDataStorage.DeleteCardById(Collection collection, int id)
        {
            collection = Collections.First(x => x.Id == collection.Id);
            collection.Cards.Remove(collection.Cards.Keys.First(x => x.id == id));
            save();
        }

        void IDataStorage.DeleteCollectionById(int id)
        {
            Collections.Remove(Collections.First(x => x.Id == id));
            save();
        }

        void IDataStorage.DeleteCollectionsByName(string name)
        {
            Collections.Remove(Collections.First(x => x.Name == name));
            save();
        }

        int IDataStorage.generateId()
        {
            List<int> ids = new List<int>();
            foreach (Collection col in Collections)
            {
                ids.Add(col.Id);
                foreach (Card card in col.Cards.Keys)
                    ids.Add(card.id);
            }
            int id = 0;
            while (ids.Contains(id))
                id++;
            return id;
        }

        Card IDataStorage.GetCardById(Collection collection, int id)
        {
            collection = Collections.First(x => x.Id == collection.Id);
            return collection.Cards.Keys.First(x => x.id == id);
        }

        List<Card> IDataStorage.GetCards(Collection collection)
        {
            collection = Collections.First(x => x.Id == collection.Id);
            return collection.Cards.Keys.ToList();
        }

        Collection IDataStorage.GetCollectionById(int id)
        {
            return Collections.First(x => x.Id == id);
        }

        ObservableCollection<Collection> IDataStorage.GetCollections()
        {
            return Collections;
        }

        ObservableCollection<Collection> IDataStorage.GetCollectionsByName(string name)
        {
            return new ObservableCollection<Collection>(Collections.Where(x => x.Name == name));
        }

        void IDataStorage.SaveCard(Collection collection, Card card)
        {
            int a = collection.Cards[card];
            collection = Collections.First(x => x.Id == collection.Id);
            collection.Cards.Remove(collection.Cards.Keys.First(x => x.id == card.id));
            collection.Cards[card] = a;
            save();
        }

        void IDataStorage.SaveCardsInCollectionCollection(Collection collection)
        {
            Collections.Remove(Collections.First(x => x.Id == collection.Id));
            Collections.Add(collection);
            save();
        }


        #region saveload
        private string JSONpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LearnCards.json");
        private void save()
        {
            List<JSONCollection> cols = new List<JSONCollection>();
            foreach(var col in Collections)
            {
                List<JSONCard> cards = new List<JSONCard>();
                foreach(var card in col.Cards.Keys)
                {
                    cards.Add(new JSONCard() { amo = col.Cards[card], Field1 = card.Field1, Field2 = card.Field2, id = card.id });
                }
                cols.Add(new JSONCollection() { Id = col.Id, Name = col.Name, Cards = cards });
            }
            File.WriteAllText(JSONpath, JsonConvert.SerializeObject(cols));
        }
        private async void load()
        {
            string str = File.ReadAllText(JSONpath);
            Collections.Clear();
            var cols = JsonConvert.DeserializeObject<List<JSONCollection>>(str);
            foreach(var col in cols)
            {
                Collection collection = new Collection() { Name = col.Name, Id = col.Id };
                collection.Cards = new Dictionary<Card, int>();
                foreach(var jcard in col.Cards)
                {
                    collection.Cards[new Card() { Field1 = jcard.Field1, Field2 = jcard.Field2, id = jcard.id }] = jcard.amo;
                }
                Collections.Add(collection);
            }
        }
        #endregion
    }
}
