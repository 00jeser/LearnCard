using LearnCards.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnCards.Services.SQLite
{
    internal class SQLiteDataStorage : IDataStorage
    {
        private CollectionsRepository _collectionsRepository;
        private CardsRepository _cardsRepository;
        public SQLiteDataStorage()
        {
            _collectionsRepository = new CollectionsRepository();
            _cardsRepository = new CardsRepository();
            collections = new System.Collections.ObjectModel.ObservableCollection<Collection>(_collectionsRepository.GetItems().Select(x => x.GetCollection()).ToList());
            foreach (var card in _cardsRepository.GetItems())
            {
                foreach (var col in collections)
                {
                    if (col.Id == card.CollectionID)
                    {
                        col.Cards[card.GetCard()] = card.count;
                    }
                }
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<Collection> collections;
        public System.Collections.ObjectModel.ObservableCollection<Collection> Collections => collections;

        public void AddCard(Collection collection, Card card)
        {
            collection.Cards[card] = 0;
            _cardsRepository.InsertItem(new Models.SQLiteDecorators.SQLiteCard()
            {
                CollectionID = collection.Id,
                count = 0,
                Field1 = card.Field1,
                Field2 = card.Field2,
                id = card.id,
            });
            Collections.Add(new Collection());
            Collections.Remove(Collections.Last());
        }

        public void AddCollection(Collection collection)
        {
            collections.Add(collection);
            _collectionsRepository.InsertItem(new Models.SQLiteDecorators.SQLiteCollection()
            {
                Name = collection.Name,
                Id = collection.Id,
            });
        }

        public void DeleteCardById(Collection collection, int id)
        {
            collection.Cards.Remove(collection.Cards.Keys.Where(x => x.id == id).FirstOrDefault());
            _cardsRepository.DeleteItem(id);
        }

        public void DeleteCollectionById(int id)
        {
            collections.Remove(collections.Where(x => x.Id == id).FirstOrDefault());
            _collectionsRepository.DeleteItem(id);
        }

        public void DeleteCollectionsByName(string name)
        {
            var c = collections.Where(x => x.Name == name).FirstOrDefault();
            collections.Remove(c);
            _collectionsRepository.DeleteItem(c.Id);
        }

        public Card GetCardById(Collection collection, int id)
        {
            return collection.Cards.Keys.First(x => x.id == id);
        }

        public List<Card> GetCards(Collection collection)
        {
            return collection.Cards.Keys.ToList();
        }

        public Collection GetCollectionById(int id)
        {
            return collections.First(x => x.Id == id);
        }

        public System.Collections.ObjectModel.ObservableCollection<Collection> GetCollections()
        {
            return collections;
        }


        public System.Collections.ObjectModel.ObservableCollection<Collection> GetCollectionsByName(string name)
        {
            return new System.Collections.ObjectModel.ObservableCollection<Collection>(collections.Where(x => x.Name == name).ToList());
        }

        public int generateId()
        {
            int id = int.MinValue;
            List<int> ids = new List<int>();
            //foreach (var col in collections)
            //{
            //    ids.Add(col.Id);
            //    foreach (var card in col.Cards?.Keys)
            //    {
            //        ids.Add(card.id);
            //    }
            //}
            foreach (var card in _cardsRepository.GetItems())
                ids.Add(card.id);
            foreach (var collection in _collectionsRepository.GetItems())
                ids.Add(collection.Id);
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }

        public void SaveCard(Collection collection, Card card)
        {
            _cardsRepository.SaveItem(new Models.SQLiteDecorators.SQLiteCard()
            {
                CollectionID = collection.Id,
                count = collection.Cards[card],
                Field1 = card.Field1,
                Field2 = card.Field2,
                id = card.id
            });
        }

        public void SaveCardsInCollectionCollection(Collection collection)
        {
            foreach(var card in collection.Cards.Keys)
                _cardsRepository.SaveItem(new Models.SQLiteDecorators.SQLiteCard()
                {
                    CollectionID = collection.Id,
                    count = collection.Cards[card],
                    Field1 = card.Field1,
                    Field2 = card.Field2,
                    id = card.id
                });
        }
    }
}
