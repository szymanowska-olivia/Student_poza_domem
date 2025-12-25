using System;
using System.Collections.Generic;
using System.Linq;


public class Room
{
    public string Name { get; }
    public string Description { get; }
    public string AsciiArt { get; }
    public List<NPC> NPCs { get; set; }
    public List<Item> Items { get; }
    public Creature? CreatureInRoom { get; set; }
    public List<RoomConnection> Connections { get; }
    public List<InteractiveObject> Objects { get; }
    private static readonly Random rand = new Random();

    public Room(string name, string description, string asciiArt = "")
    {
        Name = name;
        Description = description;
        AsciiArt = asciiArt;
        NPCs = new List<NPC>();
        Items = new List<Item>();
        CreatureInRoom = null;
        Connections = new List<RoomConnection>();
        Objects = new List<InteractiveObject>();
    }

    public void GenerateCreatures()
    {
        if (this.Name != "Przed akademikiem" && this.Name != "Recepcja" && this.Name != "Pralnia" && this.Name != "klatkaSchodowa0" && this.Name != "klatkaSchodowa1" && this.Name != "Zsyp 1" && this.Name != "Korytarz 1") {
            double chance = rand.NextDouble();

            if (chance < 0.4) // 40% szans na stworzenia
            {
                bool isAnt = rand.Next(2) == 0;
                CreatureInRoom = isAnt ? new Ant() : new Wasp();
            }
            else
            {
                CreatureInRoom = null;
            }
        }
    }

    public void AddNPC(NPC npc)
    {
        NPCs.Add(npc);
    }

    public string Describe(Player player)
    {
        var desc = $"{Name}\n{Description}\n";

        if (!string.IsNullOrWhiteSpace(AsciiArt))
        {
            desc += AsciiArt + "\n";
        }

        if (Items.Any())
        {
            desc += "Widzisz tutaj przedmioty: " +
                    string.Join(", ", Items.Select(i => i.Name)) + ".\n";
        }

        if (Objects.Any())
        {
            desc += "Są tutaj obiekty:\n";
            foreach (var o in Objects)
            {
                if (o is AdSpot adSpot && adSpot.HungAd != null)
                {
                    desc += $"Na jednej ze ścian widnieje reklama '{adSpot.HungAd.Name}'.\n";
                }
                else
                {
                    desc += $"- {o.Name}\n";
                }
            }
        }

        if (Connections.Any())
        {
            desc += "Przejścia: " +
                    string.Join(", ", Connections.Select(c => c.Direction)) + ".";
        }

        return desc;
    }

    public void Display()
    {
        Console.WriteLine($"== {Name} ==");
        Console.WriteLine(Description);
        if (!string.IsNullOrWhiteSpace(AsciiArt))
            Console.WriteLine(AsciiArt);
        Console.WriteLine();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void InteractWithObject(string objectName, Player player)
    {
        var obj = Objects.Find(o => o.Name.Equals(objectName, StringComparison.OrdinalIgnoreCase));
        if (obj != null)
        {
            obj.Interact(player);
        }
        else
        {
            Console.WriteLine("Nie ma tu takiego obiektu do interakcji.");
        }
    }

    public void AddObject(InteractiveObject obj)
    {
        Objects.Add(obj);
    }

    public void RemoveCreature()
    {
        CreatureInRoom = null;
    }
    
    public void AddCreatureInRoom(Creature creature)
    {
        CreatureInRoom = creature;
    }
}


