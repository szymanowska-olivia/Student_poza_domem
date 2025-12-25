using System;
using System.Linq;


    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAd { get; set; } = false;
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public virtual string Inspect()
        {
            return $"{Name}: {Description}";
        }
    }

