using System;
using System.Linq;

    public class DialogueNPC : NPC
    {
        public string DialogueText { get; private set; }
        public string[] Responses { get; private set; }
        protected List<Item> OfferedItems;
        public Item? RequiredItem { get; private set; }
        public Item? RewardItem { get; private set; }

        public DialogueNPC(string name, string dialogueText, List<Quest>? quests = null, string[]? responses = null,
    List<Item> offeredItems = null, Room? currentRoom = null, Item? requiredItem = null, Item? rewardItem = null)
    : base(name, quests, currentRoom)
    {
        DialogueText = dialogueText;
        Responses = responses ?? new string[0];
        OfferedItems = offeredItems ?? new List<Item>();
        RequiredItem = requiredItem;
        RewardItem = rewardItem;
    }

        public void TradeItem(Player player)
        {
            if (RequiredItem == null || RewardItem == null)
            {
                Console.WriteLine($"{Name} nie ma żadnej wymiany do zaproponowania.");
                return;
            }

            if (player.HasItem(RequiredItem.Name))
            {
                player.RemoveItem(RequiredItem);
                player.AddItem(RewardItem);
                Console.WriteLine($"{Name} mówi: Dziękuję za {RequiredItem.Name}! Oto {RewardItem.Name} w zamian.");
            }
            else
            {
                Console.WriteLine($"{Name} mówi: Potrzebuję {RequiredItem.Name}, zanim mogę Ci coś dać.");
            }
        }

        public void GiveItems(Player player)
        {
            foreach (var item in OfferedItems)
            {
                if (!player.HasItem(item.Name))
                {
                    player.AddItem(item);
                }
            }
        }

        public override void TalkTo(Player player)
        {
            Console.WriteLine($"{Name} mówi: {DialogueText}");
            for (int i = 0; i < Responses.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Responses[i]}");
            }

            if (Name == "Maelle") player.TalkwMaelle = true;
            var quest1 = player.ActiveQuests.FirstOrDefault(q => q.Name == "Pożegnaj współlokatorke");
                    if (quest1 != null)
                    {
                        quest1.CheckCompletion(player);
                    }


            // Odczytujemy wybór gracza
            Console.Write("Wybierz opcję: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= Responses.Length)
            {
                string selected = Responses[choice - 1].ToLower();

                if (selected.Contains("*Ignoruj*"))
                {
                    player.IncrementIgnoredDialogue();
                    Console.WriteLine("Zignorowałeś rozmowę.");

                    var quest = player.ActiveQuests.FirstOrDefault(q => q.Name == "Ignoruj NPC");
                    if (quest != null)
                    {
                        quest.CheckCompletion(player);
                    }
                }
                if (RequiredItem == null)
                {
                    GiveItems(player);
                }
                else if (selected == "wymiana")
                {
                    TradeItem(player);
                }
                else
                {
                    Console.WriteLine("Może następnym razem.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
        }
    }

