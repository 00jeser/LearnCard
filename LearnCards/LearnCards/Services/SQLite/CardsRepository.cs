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
        SQLiteConnection database;
        public CardsRepository()
        {
            database = Singleton.SQLiteDatabase;
            database.CreateTable<SQLiteCard>();
        }

        public IEnumerable<SQLiteCard> GetItems()
        {
            return database.Table<SQLiteCard>().ToList();
        }
        public SQLiteCard GetItem(int id)
        {
            return database.Get<SQLiteCard>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<SQLiteCard>(id);
        }
        public int SaveItem(SQLiteCard item)
        {
            if (item.id != 0)
            {
                database.Update(item);
                return item.id;
            }
            else
            {
                return database.Insert(item);
            }

        }
    }
}
