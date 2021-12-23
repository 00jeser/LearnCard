using LearnCards.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnCards.Services.SQLite
{
    internal class SqLiteDataStorage : IDataStorage
    {
        private CollectionsRepository _collectionsRepository;
        private CardsRepository _cardsRepository;
        public SqLiteDataStorage()
        {
            _collectionsRepository = new CollectionsRepository();
            _cardsRepository = new CardsRepository();
            _collections = new System.Collections.ObjectModel.ObservableCollection<Collection>(_collectionsRepository.GetItems().Select(x => x.GetCollection()).ToList());
            foreach (var card in _cardsRepository.GetItems())
            {
                foreach (var col in _collections)
                {
                    if (col.Id == card.CollectionId)
                    {
                        col.Cards[card.GetCard()] = card.Count;
                    }
                }
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<Collection> _collections;
        public System.Collections.ObjectModel.ObservableCollection<Collection> Collections => _collections;

        public void AddCard(Collection collection, Card card)
        {
            collection.Cards[card] = 0;
            _cardsRepository.InsertItem(new Models.SQLiteDecorators.SqLiteCard()
            {
                CollectionId = collection.Id,
                Count = 0,
                Field1 = card.Field1,
                Field2 = card.Field2,
                Id = card.Id,
            });
            Collections.Add(new Collection());
            Collections.Remove(Collections.Last());
        }

        public void AddCollection(Collection collection)
        {
            _collections.Add(collection);
            _collectionsRepository.InsertItem(new Models.SQLiteDecorators.SqLiteCollection()
            {
                Name = collection.Name,
                Id = collection.Id,
            });
        }

        public void DeleteCardById(Collection collection, int id)
        {
            collection.Cards.Remove(collection.Cards.Keys.Where(x => x.Id == id).FirstOrDefault());
            _cardsRepository.DeleteItem(id);
        }

        public void DeleteCollectionById(int id)
        {
            _collections.Remove(_collections.Where(x => x.Id == id).FirstOrDefault());
            _collectionsRepository.DeleteItem(id);
        }

        public void DeleteCollectionsByName(string name)
        {
            var c = _collections.Where(x => x.Name == name).FirstOrDefault();
            _collections.Remove(c);
            _collectionsRepository.DeleteItem(c.Id);
        }

        public Card GetCardById(Collection collection, int id)
        {
            return collection.Cards.Keys.First(x => x.Id == id);
        }

        public List<Card> GetCards(Collection collection)
        {
            return collection.Cards.Keys.ToList();
        }

        public Collection GetCollectionById(int id)
        {
            return _collections.First(x => x.Id == id);
        }

        public System.Collections.ObjectModel.ObservableCollection<Collection> GetCollections()
        {
            return _collections;
        }


        public System.Collections.ObjectModel.ObservableCollection<Collection> GetCollectionsByName(string name)
        {
            return new System.Collections.ObjectModel.ObservableCollection<Collection>(_collections.Where(x => x.Name == name).ToList());
        }

        public int GenerateId()
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
                ids.Add(card.Id);
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
            _cardsRepository.SaveItem(new Models.SQLiteDecorators.SqLiteCard()
            {
                CollectionId = collection.Id,
                Count = collection.Cards[card],
                Field1 = card.Field1,
                Field2 = card.Field2,
                Id = card.Id
            });
        }

        public void SaveCardsInCollectionCollection(Collection collection)
        {
            foreach(var card in collection.Cards.Keys)
                _cardsRepository.SaveItem(new Models.SQLiteDecorators.SqLiteCard()
                {
                    CollectionId = collection.Id,
                    Count = collection.Cards[card],
                    Field1 = card.Field1,
                    Field2 = card.Field2,
                    Id = card.Id
                });
        }
    }
}
