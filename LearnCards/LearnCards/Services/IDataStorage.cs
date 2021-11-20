using LearnCards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.Services
{
    public interface IDataStorage
    {
        List<Collection> Collections { get; }

        Collection GetCollectionById(int id);
        List<Collection> GetCollectionsByName(string name);
        List<Collection> GetCollections();

        Card GetCardById(Collection collection, int id);
        List<Card> GetCards(Collection collection);


        void DeleteCollectionById(int id);
        void DeleteCollectionsByName(string name);

        void DeleteCardById(Collection collection, int id);


        void AddCollection(Collection collection);

        void AddCard(Collection collection, Card card);

        int generateId();

    }

    public static class DataStorageExtensions
    {
        public static List<Card> GetCards(this Collection collection) => DependencyService.Get<IDataStorage>().GetCards(collection);
        public static Card GetCardById(this Collection collection, int id) => DependencyService.Get<IDataStorage>().GetCardById(collection, id);
    }
}
