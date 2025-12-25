using System;
using System.Collections.Generic;


public class Player
{
    public Room? CurrentRoom { get; private set; }
    public Game game { get; private set; }
    public List<Item> Inventory { get; set; }
    public List<Quest> ActiveQuests { get; private set; } = new List<Quest>();
    public string? AcceptedQuestFromNPC { get; private set; } = null;
    public int IgnoredDialogueCount { get; private set; } = 0;
    public bool MusicOn { get; set; } = false;
    public int AdsCnt { get; set; } = 0;
    public bool TalkwMaelle { get; set; } = false;
    public bool AllQuestsCompleted { get; set; } = false;

    public Player(Room? startingRoom, Game gamee)
    {
        if (startingRoom == null)
        {
            Console.WriteLine("Nizenany pokój\n");
            return;
        }
        CurrentRoom = startingRoom;
        game = gamee;
        Inventory = new List<Item>();
    }

    public void MoveTo(Room? room, Player player)
    {
        CurrentRoom = room;
        Console.WriteLine($"Wszedłeś do pomieszczenia {room.Name}\n");
        Console.WriteLine(room.Describe(player));
        foreach (var npc in player.CurrentRoom.NPCs)
        {
            InteractWith(npc);
        }

        if (player.CurrentRoom.CreatureInRoom != null)
        {
            player.CurrentRoom.CreatureInRoom.Interact(player, CurrentRoom, game);
        }
    }

    public void InteractWith(NPC npc)
    {
        npc.TalkTo(this);
    }

    public void InteractWithObject(string objectName)
    {
        if (CurrentRoom == null)
        {
            Console.WriteLine("Nie znajdujesz się w żadnym pomieszczeniu.\n");
            return;
        }

        CurrentRoom.InteractWithObject(objectName, this);
    }

    public void TakeItem(string itemName)
    {
        var item = CurrentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            Inventory.Add(item);
            CurrentRoom.RemoveItem(item);
            Console.WriteLine($"Podniosłeś przedmiot: {item.Name}\n");
        }
        else
        {
            Console.WriteLine("Nie ma tu takiego przedmiotu.\n");
        }
    }

    public void AcceptQuest(Quest quest)
    {
        if (!ActiveQuests.Contains(quest))
        {
            ActiveQuests.Add(quest);
        }
    }

    public bool CanAcceptQuestsFrom(string npcName)
    {
        return AcceptedQuestFromNPC == null || AcceptedQuestFromNPC == npcName;
    }

    public void SetAcceptedQuestFromNPC(string npcName)
    {
        if (AcceptedQuestFromNPC == null)
            AcceptedQuestFromNPC = npcName;
    }

    public void AddItem(Item item)
    {
        Inventory.Add(item);
        Console.WriteLine($"Nowy przedmiot w ekwipunku!: {item.Name}\n");
    }

    public bool HasItem(string itemName)
    {
        return Inventory.Exists(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
    }

    public void CheckAllQuests()
    {
        bool b = true;
        foreach (var quest in ActiveQuests)
        {
            quest.CheckCompletion(this);
            if (!quest.IsComplete()) b = false;
        }
        if (b) AllQuestsCompleted = true;
    }

    public void IncrementIgnoredDialogue()
    {
        IgnoredDialogueCount++;
    }
        
    public void RemoveItem(Item item)
    {
        if (Inventory.Contains(item))
        {
            Inventory.Remove(item);
        }
        else
        {
            Console.WriteLine($"Nie masz przedmiotu: {item.Name}\n");
        }
    }



}

