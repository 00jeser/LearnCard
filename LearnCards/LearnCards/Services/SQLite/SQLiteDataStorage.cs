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
            collections = _collectionsRepository.GetItems().Select(x => x.GetCollection()).ToList();
            foreach (var card in _cardsRepository.GetItems())
            {
                foreach (var col in collections)
                {
                    if (col.Id == card.id)
                    {
                        col.Cards[card.GetCard()] = card.count;
                    }
                }
            }
        }

        private List<Collection> collections;
        public List<Collection> Collections => collections;

        public void AddCard(Collection collection, Card card)
        {
            collection.Cards[card] = 0;
            _cardsRepository.SaveItem(new Models.SQLiteDecorators.SQLiteCard()
            {
                CollectionID = collection.Id,
                count = 0,
                Field1 = card.Field1,
                Field2 = card.Field2,
                id = card.id,
            });
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
            collections.Remove(collections.Where(x => x.Name == name).FirstOrDefault());
            _collectionsRepository.DeleteItem(collections.Where(x => x.Name == name).FirstOrDefault().Id);
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

        public List<Collection> GetCollections()
        {
            return collections;
        }


        public List<Collection> GetCollectionsByName(string name)
        {
            return collections.Where(x => x.Name == name).ToList();
        }

        public int generateId()
        {
            int id = int.MinValue;
            foreach (var col in collections)
            {
                foreach (var card in col.Cards?.Keys)
                {
                    if (id == card.id)
                        id++;
                }
                if (id == col.Id)
                    id++;
            }
            return id;
        }
    }
}
