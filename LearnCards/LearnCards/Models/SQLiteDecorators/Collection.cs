using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LearnCards.Models.SQLiteDecorators
{
    [Table("Colections")]
    public class SqLiteCollection
    {
        [PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }

        public Collection GetCollection()
        {
            return new Collection() { Id = Id, Name = Name, Cards = new Dictionary<Card, int>() };
        }
    }
}
