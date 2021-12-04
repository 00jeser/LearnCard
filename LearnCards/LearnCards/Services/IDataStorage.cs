using LearnCards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LearnCards.Services
{
    public interface IDataStorage
    {
        System.Collections.ObjectModel.ObservableCollection<Collection> Collections { get; }

        Collection GetCollectionById(int id);
        System.Collections.ObjectModel.ObservableCollection<Collection> GetCollectionsByName(string name);
        System.Collections.ObjectModel.ObservableCollection<Collection> GetCollections();

        Card GetCardById(Collection collection, int id);
        List<Card> GetCards(Collection collection);


        void DeleteCollectionById(int id);
        void DeleteCollectionsByName(string name);

        void DeleteCardById(Collection collection, int id);


        void AddCollection(Collection collection);

        void AddCard(Collection collection, Card card);

        int generateId();

        void SaveCard(Collection collection, Card card);
        void SaveCardsInCollectionCollection(Collection collection);

    }

    public static class DataStorageExtensions
    {
        public static List<Card> GetCards(this Collection collection) => DependencyService.Get<IDataStorage>().GetCards(collection);
        public static Card GetCardById(this Collection collection, int id) => DependencyService.Get<IDataStorage>().GetCardById(collection, id);
    }
}
