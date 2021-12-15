using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Models.JSONDecorators
{
    public class JSONCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JSONCard> Cards { get; set; }
    }
}
