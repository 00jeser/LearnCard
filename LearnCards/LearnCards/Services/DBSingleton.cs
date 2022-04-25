using LearnCards.Services.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearnCards.Services
{
    public static class DBSingleton
    {
        public static IDataStorage Storage { get; private set; }
        public static SQLiteConnection SqLiteDatabase;
        public static void Init()
        {
            //SQLiteDatabase = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "collections.db"), SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite);
            Storage = new JsonDataStorage();
        }
    }
}
