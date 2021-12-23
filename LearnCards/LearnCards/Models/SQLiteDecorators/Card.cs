using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LearnCards.Models.SQLiteDecorators
{
    [Table("Cards")]
    public class SqLiteCard
    {
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public int Count { get; set; }
        public int CollectionId { get; set; }

        [PrimaryKey, Column("_id")]
        public int Id { get; set; }

        public SqLiteCard() { }
        public SqLiteCard(Card card) 
        { 
            Field1 = card.Field1; 
            Field2 = card.Field2;
            Id = card.Id;
        }
        public Card GetCard()
        {
            return new Card() { Field1 = Field1, Field2 = Field2, Id = Id };
        }
    }
}
