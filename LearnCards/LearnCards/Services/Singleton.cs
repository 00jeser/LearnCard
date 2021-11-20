using LearnCards.Services.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearnCards.Services
{
    public static class Singleton
    {
        public static IDataStorage Storage { get; private set; }
        public static SQLiteConnection SQLiteDatabase;
        public static void Init()
        {
            SQLiteDatabase = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "collections.db"), SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite);
            Storage = new LearnCards.Services.SQLite.SQLiteDataStorage();
        }
    }
}
