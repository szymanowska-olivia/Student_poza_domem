using System;
using System.Linq;

public class Ant : Creature
{
    public bool HasItem { get; set; }
    public int DirtLevel { get; set; }
    public Item? StolenItem { get; set; }
    
    public List<Item> stolenItems = new List<Item>();
    public Ant() : base() { }

    public void StealItem(Player player)
    {
        if (player.Inventory.Count > 0)
        {
            StolenItem = player.Inventory[0];
            player.Inventory.RemoveAt(0);
            stolenItems.Add(StolenItem);
            HasItem = true;
            Console.WriteLine($"Stworzenie ukradło przedmiot: {StolenItem.Name}");
        }
        else
        {
            Console.WriteLine("Stworzenie nie mogło nic ukraść, ekwipunek gracza jest pusty.");
        }
    }

    public override void Interact(Player player, Room room, Game game)
    {
        Console.WriteLine("Mrówki idą w twoją stronę!");

        var specialItem = player.Inventory.FirstOrDefault(i => i.Name.ToLower() == "spray na mrówki");

        if (specialItem != null)
        {
            Console.WriteLine("Używasz sprayu na mrówki i pokonujesz je! Otrzymujesz z powrotem przedmioty, które ukradły.");

            foreach (var item in stolenItems)
            {
                player.AddItem(item);
                Console.WriteLine($"Odzyskałeś przedmiot: {item.Name}");
            }
            stolenItems.Clear();
            HasItem = false;

            // Usuwamy mrówki z pokoju (zakładam, że room ma listę Creature)
            room.RemoveCreature();
            var quest = player.ActiveQuests.FirstOrDefault(q => q.Name == "Pokonaj mrówki");
                if (quest != null)
                {
                    quest.CheckCompletion(player);
                }  

            LeaveDirt(room); // zostawiają brud
            ReturnItem(player);
            return;
        }
        Console.WriteLine("Mrówki atakują Cię, a Ty nie masz sprayu...");
        StealItem(player);

    }
    public void LeaveDirt(Room room)
    {
        DirtLevel++;
        Console.WriteLine($"Stworzenie zostawiło brud w pokoju {room.Name}. Poziom brudu: {DirtLevel}");
    }
    public void ReturnItem(Player player)
    {
        if (StolenItem != null)
        {
            player.AddItem(StolenItem);
            Console.WriteLine($"Stworzenie zwróciło przedmiot: {StolenItem.Name}");
            StolenItem = null;
            HasItem = false;
        }
    }

}
