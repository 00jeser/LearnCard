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
    public class JsonDataStorage : IDataStorage
    {
        public ObservableCollection<Collection> Collections { get; }

        public JsonDataStorage()
        {
            Collections = new ObservableCollection<Collection>();
            if (File.Exists(_jsoNpath))
            {
                Load();
            }
            else
            {
                //File.Create(JSONpath);
                File.WriteAllText(_jsoNpath, "[]");
            }
        }

        void IDataStorage.AddCard(Collection collection, Card card)
        {
            Collections.First(x => x.Id == collection.Id).Cards[card] = 0;
            Save();
        }

        void IDataStorage.AddCollection(Collection collection)
        {
            Collections.Add(collection);
            Save();
        }

        void IDataStorage.DeleteCardById(Collection collection, int id)
        {
            collection = Collections.First(x => x.Id == collection.Id);
            collection.Cards.Remove(collection.Cards.Keys.First(x => x.Id == id));
            Save();
        }

        void IDataStorage.DeleteCollectionById(int id)
        {
            Collections.Remove(Collections.First(x => x.Id == id));
            Save();
        }

        void IDataStorage.DeleteCollectionsByName(string name)
        {
            Collections.Remove(Collections.First(x => x.Name == name));
            Save();
        }

        int IDataStorage.GenerateId()
        {
            List<int> ids = new List<int>();
            foreach (Collection col in Collections)
            {
                ids.Add(col.Id);
                foreach (Card card in col.Cards.Keys)
                    ids.Add(card.Id);
            }
            int id = 0;
            while (ids.Contains(id))
                id++;
            return id;
        }

        Card IDataStorage.GetCardById(Collection collection, int id)
        {
            collection = Collections.First(x => x.Id == collection.Id);
            return collection.Cards.Keys.First(x => x.Id == id);
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
            collection.Cards.Remove(collection.Cards.Keys.First(x => x.Id == card.Id));
            collection.Cards[card] = a;
            Save();
        }

        void IDataStorage.SaveCardsInCollectionCollection(Collection collection)
        {
            Collections.Remove(Collections.First(x => x.Id == collection.Id));
            Collections.Add(collection);
            Save();
        }


        #region saveload
        private string _jsoNpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LearnCards.json");
        private void Save()
        {
            List<JsonCollection> cols = new List<JsonCollection>();
            foreach(var col in Collections)
            {
                List<JsonCard> cards = new List<JsonCard>();
                foreach(var card in col.Cards.Keys)
                {
                    cards.Add(new JsonCard() { Amo = col.Cards[card], Field1 = card.Field1, Field2 = card.Field2, Id = card.Id });
                }
                cols.Add(new JsonCollection() { Id = col.Id, Name = col.Name, Cards = cards });
            }
            File.WriteAllText(_jsoNpath, JsonConvert.SerializeObject(cols));
        }
        private async void Load()
        {
            string str = File.ReadAllText(_jsoNpath);
            Collections.Clear();
            var cols = JsonConvert.DeserializeObject<List<JsonCollection>>(str);
            foreach(var col in cols)
            {
                Collection collection = new Collection() { Name = col.Name, Id = col.Id };
                collection.Cards = new Dictionary<Card, int>();
                foreach(var jcard in col.Cards)
                {
                    collection.Cards[new Card() { Field1 = jcard.Field1, Field2 = jcard.Field2, Id = jcard.Id }] = jcard.Amo;
                }
                Collections.Add(collection);
            }
        }
        #endregion
    }
}
