using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Services
{
    public static class Singleton
    {
        public static IDataStorage Storage { get; private set; }
        public static void Init()
        {
            Storage = new MongoDBStorage();
        }
    }
}
