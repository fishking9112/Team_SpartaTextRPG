using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public Item(string _Name , string _Des  , int _Price)
        {
            Name = _Name; 
            Description = _Des;
            Price = _Price;
        }
    }
}
