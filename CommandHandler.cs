using System;
using System.Linq;

public class CommandHandler
{
    private Game game;
    private Player player;

    public CommandHandler(Game game)
    {
        this.game = game;
        this.player = game.Player;
    }

    public void HandleInput(string? input)
    {
        string[] tokens = input.ToLower().Split(' ', 2);
        string command = tokens[0];
        string argument = tokens.Length > 1 ? tokens[1] : "";

        switch (command)
        {
            case "idz":
                MovePlayer(argument);
                break;

            case "inventory":
                ShowInventory();
                break;

            case "zbadaj":
                Zbadaj(argument);
                break;
            
            case "podnieś":
                PickUp(argument);
                break;

            case "quit":
                Game.EndGame("Gracz wyszedł z gry");
                break;

            case "quests":
                ShowQuests();
                break;

            case "help":
                ShowHelp();
                break;

            default:
                Console.WriteLine("Nieznana komenda. Wpisz 'help' by zobaczyć dostępne polecenia.");
                break;
        }
    }

    private void MovePlayer(string direction)
    {
        Room currentRoom = player.CurrentRoom;
        RoomConnection? connection = currentRoom.Connections.FirstOrDefault(conn => conn.Direction.ToLower() == direction.ToLower());
        if (connection != null && connection.CanPass(player))
        {
            player.MoveTo(connection.TargetRoom, player);

        }
        else
        {
            Console.WriteLine("Nie możesz tam przejść.");
        }
    }

    private void ShowInventory()
    {
        if (player.Inventory.Count == 0)
        {
            Console.WriteLine("Twój ekwipunek jest pusty.");
        }
        else
        {
            Console.WriteLine("Ekwipunek:");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }
    }

    private void TalkToNPC(string npcName)
    {
        var npcPair = game.NPCs.FirstOrDefault(n => n.Value.Name.ToLower() == npcName.ToLower());
        var npc = npcPair.Value;

        if (npc is DialogueNPC dialogueNPC)
        {
            dialogueNPC.TalkTo(player);
        }
        else
        {
            Console.WriteLine("Nie ma tu nikogo takiego do rozmowy.");
        }
    }

    private void Zbadaj(string objectName)
    {
        var objects = player.CurrentRoom.Objects;
        if (objects == null || objects.Count == 0)
        {
            Console.WriteLine("Nie ma tutaj żadnych obiektów do zbadania.");
            return;
        }

        var obj = player.CurrentRoom.Objects.FirstOrDefault(o => o.Name.Equals(objectName, StringComparison.OrdinalIgnoreCase));

        if (obj != null)
        {
            player.InteractWithObject(objectName);
        }
        else
        {
            Console.WriteLine("Nie ma tu takiego obiektu.");
        }
    }

    private void PickUp(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            Console.WriteLine("Musisz podać nazwę przedmiotu do podniesienia.");
            return;
        }

        var item = player.CurrentRoom.Items
            .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            Console.WriteLine($"Nie widzisz tutaj przedmiotu o nazwie: {itemName}.");
            return;
        }

        player.Inventory.Add(item);
        player.CurrentRoom.RemoveItem(item);
        Console.WriteLine($"Podniosłeś: {item.Name}");
    }
    private void ShowQuests()
    {
        if (player.AcceptQuest == null)
        {
            Console.WriteLine("Nie masz żadnych aktywnych zadań.");
        }
        else
        {
            Console.WriteLine("Twoje zadania:");
            foreach (var quest in player.ActiveQuests)
            {
                Console.WriteLine($"- {quest.Name} {(quest.IsComplete() ? "[ukończone]" : "")}");
            }
        }
    }

    private void ShowHelp()
    {
        Console.WriteLine("Dostępne komendy:");
        Console.WriteLine("- idz [kierunek]");
        Console.WriteLine("- inventory");
        Console.WriteLine("- zbadaj [obiekt]");
        Console.WriteLine("- podnieś [przedmiot]");
        Console.WriteLine("- quit");
        Console.WriteLine("- quests");
        Console.WriteLine("- help");
    }

}
