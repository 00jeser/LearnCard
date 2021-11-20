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
        SQLiteConnection database;
        public CollectionsRepository()
        {
            database = Singleton.SQLiteDatabase;
            database.CreateTable<SQLiteCollection>();
        }

        public IEnumerable<SQLiteCollection> GetItems()
        {
            var d = database.Table<SQLiteCollection>();
            return d.ToList();
        }
        public SQLiteCollection GetItem(int id)
        {
            return database.Get<SQLiteCollection>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<SQLiteCollection>(id);
        }
        public int InsertItem(SQLiteCollection item)
        {
            return database.Insert(item);
        }

        public int UpdateItem(SQLiteCollection item)
        {
            database.Update(item);
            return item.Id;
        }

    }
}
