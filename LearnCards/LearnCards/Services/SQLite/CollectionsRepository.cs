using LearnCards.Models;
using LearnCards.Models.SQLiteDecorators;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Services.SQLite
{
    public class CollectionsRepository
    {
        SQLiteConnection _database;
        public CollectionsRepository()
        {
            _database = Singleton.SqLiteDatabase;
            _database.CreateTable<SqLiteCollection>();
        }

        public IEnumerable<SqLiteCollection> GetItems()
        {
            var d = _database.Table<SqLiteCollection>();
            return d.ToList();
        }
        public SqLiteCollection GetItem(int id)
        {
            return _database.Get<SqLiteCollection>(id);
        }
        public int DeleteItem(int id)
        {
            return _database.Delete<SqLiteCollection>(id);
        }
        public int InsertItem(SqLiteCollection item)
        {
            return _database.Insert(item);
        }

        public int UpdateItem(SqLiteCollection item)
        {
            _database.Update(item);
            return item.Id;
        }

    }
}
