using LearnCards.Models;
using LearnCards.Models.SQLiteDecorators;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Services.SQLite
{
    public class CardsRepository
    {
        SQLiteConnection _database;
        public CardsRepository()
        {
            _database = Singleton.SqLiteDatabase;
            _database.CreateTable<SqLiteCard>();
        }

        public IEnumerable<SqLiteCard> GetItems()
        {
            return _database.Table<SqLiteCard>().ToList();
        }
        public SqLiteCard GetItem(int id)
        {
            return _database.Get<SqLiteCard>(id);
        }
        public int DeleteItem(int id)
        {
            return _database.Delete<SqLiteCard>(id);
        }
        public int InsertItem(SqLiteCard item)
        {
            return _database.Insert(item);
        }
        public int SaveItem(SqLiteCard item)
        {
            _database.Update(item);
            return item.Id;

        }
    }
}
