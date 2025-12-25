using System.Collections.Generic;
using System.Linq;
using System;


    public class NPC
    {
        public string Name { get; private set; }
        protected List<Quest>? Quests;
        public Room? CurrentRoom { get; private set;}
        private List<Item>? GivenItems;   
        public NPC(string name, List<Quest>? quests = null, Room? currentRoom = null, List<Item>? givenItems = null)
    {
        Name = name;
        Quests = quests ?? new List<Quest>();
        CurrentRoom = currentRoom;
        GivenItems = givenItems ?? new List<Item>();
    }

        public virtual void TalkTo(Player player)
        {
            Console.WriteLine($"{Name} mówi: Cześć! Możesz ze mną porozmawiać.\n");

            // Daj przedmioty (jeśli jeszcze ich nie masz)

            if (Quests.Count == 0)
            {
                Console.WriteLine("Nie mam dla ciebie żadnych zadań.");
                return;
            }

            if (!player.CanAcceptQuestsFrom(Name))
            {
                Console.WriteLine($"Ale ty przyjąłeś już zadania od innego NPC i nie możesz przyjąć kolejnych, więc sobie nie pogadacie.");
                return;
            }

            Console.WriteLine("Mam dla ciebie kilka zadań:");
            foreach (var quest in Quests)
            {
                Console.WriteLine($"- {quest.Name} - {quest.Description}");
            }

            Console.Write("Czy chcesz przyjąć wszystkie te zadania? (tak/nie): ");
            string? input = Console.ReadLine()?.Trim().ToLower();

        if (input == "tak")
        {
            foreach (var quest in Quests)
            {
                player.AcceptQuest(quest);
            }
            player.SetAcceptedQuestFromNPC(Name);
            Console.WriteLine("\nPrzyjąłeś wszystkie zadania.\n");

            foreach (var item in GivenItems)
            {
                if (!player.HasItem(item.Name))
                {
                    player.AddItem(item);
                }
            }

            Console.WriteLine("Celem twojego pobytu jest teraz ukończenie wszystkich zadań, inaczej stanie się coś BARDZO złego... (gra się nie skończy :P)");
            Console.WriteLine("Możesz poruszać się między pomieszczeniami, prowadzić interakcje z NPC, prowadzić interakcje z obiektami znajdujacymi się w niektóych pomieszczeniach, prowadzić interakcję z przedmiotami, a także bić potworki :D\n");

        }
        else
        {
            Console.WriteLine("Nie przyjąłeś żadnych zadań.");
        }
        }

        public List<Quest> GetAvailableQuests()
        {
            return Quests.Where(q => !q.IsComplete()).ToList();
        }
    }

