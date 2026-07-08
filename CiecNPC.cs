#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
public class CiecNPC : DialogueNPC
{
    public CiecNPC(string name, string dialogueText,
    System.Collections.Generic.List<Quest>? quests = null,
    string[]? responses = null,
    System.Collections.Generic.List<Item>? offeredItems = null,
    Room? currentRoom = null,
    Item? requiredItem = null,
    Item? rewardItem = null)
    : base(name, dialogueText, quests, responses, offeredItems, currentRoom, requiredItem, rewardItem)
    {
    }

    public override void TalkTo(Player player)
    {
        Console.WriteLine($"{Name} mówi: {DialogueText}");
        for (int i = 0; i < Responses.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Responses[i]}");
        }

        Console.Write("Zapytaj o: ");
        string? input = Console.ReadLine()?.Trim().ToLower();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Nie podano żadnej wartości.");
            return;
        }

        HandleDialogueOption(input, player);
    }

    private void HandleDialogueOption(string input, Player player)
    {
        // Klucze A-K oprócz I
        var validKeys = new HashSet<string> { "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "l" };

        if (input == null)
        {
            Console.WriteLine("Nie podano żadnej wartości.");
            return;
        }
        if (input != null && validKeys.Contains(input))
        {
            var keyName = $"Klucz {input.ToUpper()}";
            
            if (!player.HasItem(keyName))
            {
                var itemToAdd = OfferedItems?.FirstOrDefault(item => item.Name == keyName);
                if (itemToAdd != null)
                {
                    Console.WriteLine(keyName);
                    player.AddItem(itemToAdd);
                } 
            }
        }
        else if (input == "i")
        {
            Console.WriteLine("Nie posiadam klucza do drzwi I.");
        }
        else if (input == "pralnia")
        {
            Console.WriteLine("Klucz do pralni znajduje się w pokoju 107.");
        }
        else if (input == "muzyka")
        {
            if (player.HasItem("plakietka ppoż"))
            {
                Console.WriteLine("Włączam muzykę. Miłego słuchania!");
                player.MusicOn = true;
                var quest = player.ActiveQuests.FirstOrDefault(q => q.Name == "Puść muzykę");
                if (quest != null)
                {
                    quest.CheckCompletion(player);
                }
            }
            else
            {
                Console.WriteLine("Nie mogę włączyć muzyki bez plakietki ppoż.");
            }
        }
        else
        {
            Console.WriteLine("Nie rozumiem, o co pytasz.");
        }
    }
}
