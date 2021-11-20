using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LearnCards.Models.SQLiteDecorators
{
    [Table("Cards")]
    public class SQLiteCard
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public int count { get; set; }
        public int CollectionID { get; set; }

        [PrimaryKey, Column("_id")]
        public int id { get; set; }

        public SQLiteCard() { }
        public SQLiteCard(Card card) 
        { 
            Field1 = card.Field1; 
            Field2 = card.Field2;
            id = card.id;
        }
        public Card GetCard()
        {
            return new Card() { Field1 = Field1, Field2 = Field2, id = id };
        }
    }
}
