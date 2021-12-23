using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCards.Models.JSONDecorators
{
    public class JsonCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<JsonCard> Cards { get; set; }
    }
}
