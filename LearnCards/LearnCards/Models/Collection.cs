using LearnCards.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Card, int> Cards { get; set;}
    }
}
