using System;

namespace GameNamespace
{
    public class UsableItem : Item
    {
        public UsableItem(string name, string description) : base(name, description)
        {
        }
        public virtual void UseEffect(Player player, Room room)
        {
            Console.WriteLine($"{Name} nie ma żadnego efektu.");
        }
    }
}
