using System;
using System.Linq;
using System.Collections.Generic;


    public class Game
    {
        public Player? Player { get; set; }
        public List<Item> KeyItems { get; private set; }= new List<Item>();
        public Dictionary<string, NPC> NPCs { get; private set; } = new Dictionary<string, NPC>();
        public Dictionary<string, Room> Rooms { get; private set; } = new Dictionary<string, Room>();
        public Dictionary<string, Item> Items { get; private set; } = new Dictionary<string, Item>();
    public void Start()
    {
        Console.WriteLine("Witaj w grze: Student poza domem!\n");
        Console.WriteLine("Tutaj nie masz imienia - twoja najważniejsza cecha to bycie studentem, a zarazem nowym lokatorem akademika. Zdążyłeś wyjść na chwilę do sklepu. Wróciłeś, a pod akademikiem czeka już na ciebie kilka osób... Będziesz musiał zdecydować się, aby pomóc którejś z nich - inaczej nie wejdziesz do środka D:\n");
        Console.WriteLine("Wpisz *help* żeby zobaczyć wszystkie komendy\n");
        InitializeMap();
        InitializeItems();
        InitializeNPCs();
        InitializeObjects();
        InitializeCreatures();
        InitializePlayer();

        }

        private string GetAsciiForRoom(string name)
        {
            if (name == "Recepcja") return RoomArtLibrary.Reception;
            if (name == "Pralnia") return RoomArtLibrary.LaundryRoom;
            if (name == "Przed akademikiem") return RoomArtLibrary.Outside;
            if (name == "Dach") return RoomArtLibrary.Roof;
            if (name == "Klatka schodowa dach") return RoomArtLibrary.Stairs;

            if (name.StartsWith("Kuchnia")) return RoomArtLibrary.Kitchen;
            if (name.StartsWith("Łazienka")) return RoomArtLibrary.Bathroom;
            if (name.StartsWith("pokój")) return RoomArtLibrary.Pokoj;
            if (name.StartsWith("Korytarz")) return RoomArtLibrary.Hallway;
            if (name.StartsWith("klatkaSchodowa")) return RoomArtLibrary.Stairs;
            if (name.StartsWith("Zsyp")) return RoomArtLibrary.Chute;
            if (name.StartsWith("Składzik")) return RoomArtLibrary.Closet;

            return "";
        }

        private void AddRoom(Room r)
        {
            if (!Rooms.ContainsKey(r.Name))
                Rooms[r.Name] = r;
        }

        private void CreateFloor(
        string floorName,
        string stairwellName,
        string chuteRoomName,
        bool chuteB,
        string hallwayName,
        string preAB, string preCD,
        string kitchenA, string kitchenB, string kitchenC, string kitchenD,
        string bathroomA, string bathroomB, string bathroomC, string bathroomD,
        string room1, string room2, string room3, string room4,
        string room5, string room6, string room7, string room8)
        {

            var stairwell = new Room(stairwellName, $"Klatka schodowa piętra {floorName}",
            GetAsciiForRoom(stairwellName));
            var chute = chuteB ? new Room(chuteRoomName, "Pomieszczenie ze zsypem") :
            new Room(chuteRoomName, "Pomieszczenie z przeróżnymi przedmiotami", GetAsciiForRoom(chuteRoomName));
            var hallway = new Room(hallwayName, "Główny korytarz piętra", GetAsciiForRoom(hallwayName));

            var przedpokojAB = new Room(preAB, $"Przedpokój prowadzący do {kitchenA} i {kitchenB}", GetAsciiForRoom(preAB));
            var przedpokojCD = new Room(preCD, $"Przedpokój prowadzący do {kitchenC} i {kitchenD}", GetAsciiForRoom(preCD));

            var kuchniaA = new Room(kitchenA, $"Wspólna kuchnia {room1} i {room2}", GetAsciiForRoom(kitchenA));
            var kuchniaB = new Room(kitchenB, $"Wspólna kuchnia {room3} i {room4}", GetAsciiForRoom(kitchenB));
            var kuchniaC = new Room(kitchenC, $"Wspólna kuchnia {room5} i {room6}", GetAsciiForRoom(kitchenC));
            var kuchniaD = new Room(kitchenD, $"Wspólna kuchnia {room7} i {room8}", GetAsciiForRoom(kitchenD));

            var lazienkaA = new Room(bathroomA, $"Wspólna łazienka {room1} i {room2}", GetAsciiForRoom(bathroomA));
            var lazienkaB = new Room(bathroomB, $"Wspólna łazienka {room3} i {room4}", GetAsciiForRoom(bathroomB));
            var lazienkaC = new Room(bathroomC, $"Wspólna łazienka {room5} i {room6}", GetAsciiForRoom(bathroomC));
            var lazienkaD = new Room(bathroomD, $"Wspólna łazienka {room7} i {room8}", GetAsciiForRoom(bathroomD));

            var pokoj1 = new Room(room1, "Pokój studentów", GetAsciiForRoom(room1));
            var pokoj2 = new Room(room2, "Pokój studentów", GetAsciiForRoom(room2));
            var pokoj3 = new Room(room3, "Pokój studentów", GetAsciiForRoom(room3));
            var pokoj4 = new Room(room4, "Pokój studentów", GetAsciiForRoom(room4));
            var pokoj5 = new Room(room5, "Pokój studentów", GetAsciiForRoom(room5));
            var pokoj6 = new Room(room6, "Pokój studentów", GetAsciiForRoom(room6));
            var pokoj7 = new Room(room7, "Pokój studentów", GetAsciiForRoom(room7));
            var pokoj8 = new Room(room8, "Pokój studentów", GetAsciiForRoom(room8));

            var allRooms = new[]
            {
                stairwell, chute, hallway,
                przedpokojAB, przedpokojCD,
                kuchniaA, kuchniaB, kuchniaC, kuchniaD,
                lazienkaA, lazienkaB, lazienkaC, lazienkaD,
                pokoj1, pokoj2, pokoj3, pokoj4,
                pokoj5, pokoj6, pokoj7, pokoj8
            };

            foreach (var room in allRooms) AddRoom(room);

            stairwell.Connections.Add(chuteB ? new RoomConnection("w prawo", chute) : new DoorConnection("w lewo", chute, "klucz do składzika"));
            stairwell.Connections.Add(new RoomConnection("prosto", hallway));
            hallway.Connections.Add(new RoomConnection("wyjdz", stairwell));
            hallway.Connections.Add(new RoomConnection("w lewo", przedpokojAB));
            przedpokojAB.Connections.Add(new RoomConnection("wyjdz", hallway));
            przedpokojAB.Connections.Add(new DoorConnection("w prawo", kuchniaA, "Klucz A"));
            kuchniaA.Connections.Add(new RoomConnection("wyjdz", przedpokojAB));
            kuchniaA.Connections.Add(new RoomConnection("w prawo", lazienkaA));
            lazienkaA.Connections.Add(new RoomConnection("wyjdz", kuchniaA));
            kuchniaA.Connections.Add(new RoomConnection("prosto", pokoj1));
            pokoj1.Connections.Add(new RoomConnection("wyjdz", kuchniaA));
            kuchniaA.Connections.Add(new RoomConnection("w prawo", pokoj2));
            pokoj2.Connections.Add(new RoomConnection("wyjdz", kuchniaA));
            przedpokojAB.Connections.Add(new DoorConnection("w lewo", kuchniaB, "Klucz B"));
            kuchniaB.Connections.Add(new RoomConnection("wyjdz", przedpokojAB));
            kuchniaB.Connections.Add(new RoomConnection("w lewo", lazienkaB));
            lazienkaB.Connections.Add(new RoomConnection("wyjdz", kuchniaB));
            kuchniaB.Connections.Add(new RoomConnection("prosto", pokoj3));
            pokoj3.Connections.Add(new RoomConnection("wyjdz", kuchniaB));
            kuchniaB.Connections.Add(new RoomConnection("w prawo", pokoj4));
            pokoj4.Connections.Add(new RoomConnection("wyjdz", kuchniaB));
            hallway.Connections.Add(new RoomConnection("w prawo", przedpokojCD));
            przedpokojCD.Connections.Add(new RoomConnection("wyjdz", hallway));
            przedpokojCD.Connections.Add(new DoorConnection("w prawo", kuchniaD, "Klucz D"));
            kuchniaD.Connections.Add(new RoomConnection("wyjdz", przedpokojCD));
            kuchniaD.Connections.Add(new RoomConnection("w prawo", lazienkaD));
            lazienkaD.Connections.Add(new RoomConnection("wyjdz", kuchniaD));
            kuchniaD.Connections.Add(new RoomConnection("prosto", pokoj8));
            pokoj8.Connections.Add(new RoomConnection("wyjdz", kuchniaD));
            kuchniaD.Connections.Add(new RoomConnection("w lewo", pokoj7));
            pokoj7.Connections.Add(new RoomConnection("wyjdz", kuchniaD));
            przedpokojCD.Connections.Add(new DoorConnection("w lewo", kuchniaC, "Klucz C"));
            kuchniaC.Connections.Add(new RoomConnection("wyjdz", przedpokojCD));
            kuchniaC.Connections.Add(new RoomConnection("w lewo", lazienkaC));
            lazienkaC.Connections.Add(new RoomConnection("wyjdz", kuchniaC));
            kuchniaC.Connections.Add(new RoomConnection("prosto", pokoj5));
            pokoj5.Connections.Add(new RoomConnection("wyjdz", kuchniaC));
            kuchniaC.Connections.Add(new RoomConnection("w prawo", pokoj6));
            pokoj6.Connections.Add(new RoomConnection("wyjdz", kuchniaC));
        }

        private void InitializeFloor0()
        {
            var przedAkademikiem = new Room("Przed akademikiem", "Stoisz przed głównym wejściem do akademika.", GetAsciiForRoom("Przed Akademikiem"));
            var recepcja = new Room("Recepcja", "Pomieszczenie z ladą, za którą siedzi cieć.", GetAsciiForRoom("Recepcja"));
            var klatkaSchodowa0 = new Room("klatkaSchodowa0", "Klatka schodowa prowadząca na wyższe piętra.", GetAsciiForRoom("klatkaSchodowa0"));
            var pralnia = new Room("Pralnia", "Pomieszczenie z pralkami i suszarkami dla mieszkańców.", GetAsciiForRoom("Pralnia"));

            AddRoom(przedAkademikiem);
            AddRoom(recepcja);
            AddRoom(klatkaSchodowa0);
            AddRoom(pralnia);

            przedAkademikiem.Connections.Add(new DoorConnection("prosto", recepcja, "karta mieszkańca"));
            recepcja.Connections.Add(new RoomConnection("wyjdz", przedAkademikiem));
            recepcja.Connections.Add(new RoomConnection("w lewo", klatkaSchodowa0));
            recepcja.Connections.Add(new DoorConnection("w prawo", pralnia, "klucz do pralni"));
            pralnia.Connections.Add(new RoomConnection("wyjdz", recepcja));
            klatkaSchodowa0.Connections.Add(new RoomConnection("wyjdz", recepcja));

        }

        private void InitializeFloor1()
        {
            CreateFloor("Piętro 1", "klatkaSchodowa1", "Zsyp 1", true, "Korytarz 1",
            "Przedpokój AB", "Przedpokój CD",
            "Kuchnia A", "Kuchnia B", "Kuchnia C", "Kuchnia D",
            "Łazienka A", "Łazienka B", "Łazienka C", "Łazienka D",
            "pokój 101", "pokój 102", "pokój 103", "pokój 104",
            "pokój 105", "pokój 106", "pokój 107", "pokój 108");

        }

        private void InitializeFloor2()
        {
            CreateFloor("Piętro 2", "klatkaSchodowa2", "Składzik", false, "Korytarz 2",
            "Przedpokój EF", "Przedpokój GH",
            "Kuchnia E", "Kuchnia F", "Kuchnia G", "Kuchnia H",
            "Łazienka E", "Łazienka F", "Łazienka G", "Łazienka H",
            "pokój 201", "pokój 202", "pokój 203", "pokój 204",
            "pokój 205", "pokój 206", "pokój 207", "pokój 208");

        }

        private void InitializeFloor3()
        {
            CreateFloor("Piętro 3", "klatkaSchodowa3", "Zsyp 3", true, "Korytarz 3",
            "Przedpokój IJ", "Przedpokój KL",
            "Kuchnia I", "Kuchnia J", "Kuchnia K", "Kuchnia L",
            "Łazienka I", "Łazienka J", "Łazienka K", "Łazienka L",
            "pokój 301", "pokój 302", "pokój 303", "pokój 304",
            "pokój 305", "pokój 306", "pokój 307", "pokój 308");
        }

        private void InitializeRoof()
        {
            var klatkaSchodowaDach = new Room("Klatka schodowa dach", "Klatka schodowa prowadząca na dach akademika", GetAsciiForRoom("Klatka schodowa dach"));
            var dach = new Room("Dach", "Rozciąga się przed Tobą panorama miasta. Wieje mocny wiatr.", GetAsciiForRoom("Dach"));

            AddRoom(klatkaSchodowaDach);
            AddRoom(dach);

            klatkaSchodowaDach.Connections.Add(new RoomConnection("prosto", dach));
            dach.Connections.Add(new RoomConnection("wyjdz", klatkaSchodowaDach));

        }

        private void InitializeStaircases()
        {
            // Połączenia między piętrami (klatki schodowe)
            if (Rooms.TryGetValue("klatkaSchodowa0", out var kl0) &&
                Rooms.TryGetValue("klatkaSchodowa1", out var kl1) &&
                Rooms.TryGetValue("klatkaSchodowa2", out var kl2) &&
                Rooms.TryGetValue("klatkaSchodowa3", out var kl3) &&
                Rooms.TryGetValue("Klatka schodowa dach", out var klDach))
            {
                kl0.Connections.Add(new RoomConnection("pietro 1", kl1));
                kl0.Connections.Add(new RoomConnection("pietro 2", kl2));
                kl0.Connections.Add(new RoomConnection("pietro 3", kl3));
                kl0.Connections.Add(new RoomConnection("pietro 4", klDach));
                kl1.Connections.Add(new RoomConnection("parter", kl0));
                kl1.Connections.Add(new RoomConnection("pietro 2", kl2));
                kl1.Connections.Add(new RoomConnection("pietro 3", kl3));
                kl1.Connections.Add(new RoomConnection("pietro 4", klDach));
                kl2.Connections.Add(new RoomConnection("parter", kl0));
                kl2.Connections.Add(new RoomConnection("pietro 1", kl1));
                kl2.Connections.Add(new RoomConnection("pietro 3", kl3));
                kl2.Connections.Add(new RoomConnection("pietro 4", klDach));
                kl3.Connections.Add(new RoomConnection("parter", kl0));
                kl3.Connections.Add(new RoomConnection("pietro 1", kl1));
                kl3.Connections.Add(new RoomConnection("pietro 2", kl2));
                kl3.Connections.Add(new RoomConnection("pietro 4", klDach));
                klDach.Connections.Add(new RoomConnection("parter", kl0));
                klDach.Connections.Add(new RoomConnection("pietro 1", kl1));
                klDach.Connections.Add(new RoomConnection("pietro 2", kl2));
                klDach.Connections.Add(new RoomConnection("pietro 3", kl3));
            }
        }

        private void InitializeMap()
        {
            InitializeFloor0();
            InitializeFloor1();
            InitializeFloor2();
            InitializeFloor3();
            InitializeRoof();
            InitializeStaircases();
        }

        private void AddItem(Item item)
        {
            if (!Items.ContainsKey(item.Name))
                Items[item.Name] = item;
        }
 
        private void InitializeItems()
        {
            AddItem(new Item("klucz do pralni", "Mały klucz otwierający drzwi do pralni."));
            AddItem(new Item("karta mieszkańca", "Karta dostępu do akademika."));
            AddItem(new Item("spray na mrówki", "Środek do zwalczania mrówek."));
            AddItem(new Item("plakietka ppoż", "Plakietka świadcząca o tym, że jesteś serwisantem uprawnionym do sprawdzania działania systemu przeciwpożarowego"));
            AddItem(new Item("płyn do mycia szyb", "Okna czyste jak nigdy wcześniej!"));
            AddItem(new Item("przepychaczka", "Idealnie sprawdza sie w prypadku zatkanych zlewów"));
            AddItem(new Item("syfon", "Nowy syfon"));
            AddItem(new Item("śmieci", "Śmieci"));
            AddItem(new Item("poradnik hydrauliczny", "Poradnik hydrauliczny"));
            AddItem(new Item("Reklama 1", "Reklama zespołu muzycznego") { IsAd = true });
            AddItem(new Item("Reklama 2", "Reklama papieru toaletowego") { IsAd = true });
            AddItem(new Item("Reklama 3", "Reklama.") { IsAd = true });
            AddItem(new Item("Reklama 4", "Reklama proszku do prania") { IsAd = true });
            AddItem(new Item("Reklama 5", "Reklama reklamy spotów do rozwieszania.. reklam!") { IsAd = true });
            AddItem(new Item("niesamowita niebieska łapka na owady", "Tak niesamowita, że dzięki niej pokonasz osy w skeundę."));
            AddItem(new Item("Czapka", "Chyba ktoś jej szuka"));

        for (char c = 'A'; c <= 'L'; c++)
        {
            string keyName = $"Klucz {c}";
            string keyDescription = $"Klucz do modułu {c}";
            var it = new Item(keyName, keyDescription);
            AddItem(it);
            KeyItems.Add(it);
                
        }



        // rozłożenie przedmiotów po pokojach
            Rooms["Łazienka C"].Items.Add(Items["płyn do mycia szyb"]);
            Rooms["Łazienka B"].Items.Add(Items["przepychaczka"]);
            Rooms["Składzik"].Items.Add(Items["syfon"]);
            Rooms["Kuchnia K"].Items.Add(Items["śmieci"]);


        }

        private void InitializePlayer()
        {
        // Gracz zaczyna przed akademikiem
        if (Rooms.TryGetValue("Przed akademikiem", out var startRoom))
        {
            Player = new Player(startRoom, this);
            //Player.CurrentRoom.Display();
            Console.WriteLine(Player.CurrentRoom.Describe(Player));
            foreach (var npc in Player.CurrentRoom.NPCs)
            {
                npc.TalkTo(Player);
            }

        }
        else
            throw new Exception("Brak pokoju startowego: 'Przed akademikiem'");
            
        }

        private void AddNPC(NPC npc)
        {
            if (!NPCs.ContainsKey(npc.Name))
                NPCs[npc.Name] = npc;
        }

        private void InitializeNPCs()
        {
            if (Rooms.TryGetValue("Przed akademikiem", out var outside))
            {
                if (Items.TryGetValue("karta mieszkańca", out var karta) &&
                    Items.TryGetValue("poradnik hydrauliczny", out var poradnik) &&
                    Items.TryGetValue("Reklama 1", out var ad1) &&
                    Items.TryGetValue("Reklama 2", out var ad2) &&
                    Items.TryGetValue("Reklama 3", out var ad3) &&
                    Items.TryGetValue("Reklama 4", out var ad4) &&
                    Items.TryGetValue("Reklama 5", out var ad5))
                {

                    var questsMarek = new List<Quest>
                {
                     
                    new Quest("Odetkaj zlew", "Odetkaj zlew w module G", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Zlew" && !obj.GetState<bool>("zatkany"))),
                
    
                    new Quest("Wymień syfon", "Wymień syfon w prysznicu w łazience J", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Prysznic" && !obj.GetState<bool>("zepsuty"))),

                    new Quest("Napraw pralkę", "Napraw pralkę w pralni", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Pralka 1" && obj.GetState<bool>("naprawiona"))),

                    new Quest("Odpowietrz grzejnik", "Odpowietrz zapowietrzony grzejnik w pokoju 203", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Grzejnik" && !obj.GetState<bool>("zapowietrzony")))
                };

                    var questsCamille = new List<Quest>
                {
                    new Quest("Wyjmij rzeczy z pralek", "Wyjmij wszystkie rzeczy z pralek i nastaw je na długi program", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    (obj.Name == "Pralka 2" && !obj.GetState<bool>("program")) || (obj.Name == "Pralka 3" && !obj.GetState<bool>("program")))),

                    new Quest("Puść muzykę", "Puść muzykę z głośników w całym budynku, Cieć chętnie ci w tym pomoże :).", player => player.MusicOn),

                    new Quest("Ignoruj NPC", "Ignoruj 2 NPC", player => player.IgnoredDialogueCount >= 2),

                    new Quest("Wywieś reklamy", "Wywieś reklamy na 5 pustych ścianach", player => player.AdsCnt == 5)
                };

                    var r = Rooms["Łazienka K"];
                    var questsMaja = new List<Quest>
                {
                    new Quest("Umyj okna", "Umyj okna w pokoju 105", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Okna" && !obj.GetState<bool>("brudne"))),

                    new Quest("Wynieś śmieci", "Wynieś śmieci do zsypu", player =>
                    player.CurrentRoom.Objects.Any(obj =>
                    obj.Name == "Kontener na śmieci" && obj.GetState<bool>("pełny"))),

                    new Quest("Pożegnaj współlokatorke", "Pożegnaj współlokatorke z pokoju 308", player =>
                    player.TalkwMaelle == true),
                    
                    new Quest("Pokonaj mrówki", "Spryskaj mrówki w naszej łazience", player =>
                    r.CreatureInRoom == null)
                };

                    // Itemy dla NPC - kopiujemy referencje (lub jeśli itemy są unikalne, możesz stworzyć nowe kopie)
                    var itemsMarek = new List<Item> { karta, poradnik };
                    var itemsCamille = new List<Item> { karta, ad1, ad2, ad3, ad4, ad5 };
                    var itemsMaja = new List<Item> { karta };

                    // Tworzymy NPC
                    var marek = new NPC("Marek", questsMarek, outside, itemsMarek);
                    var camille = new NPC("Camille", questsCamille, outside, itemsCamille);
                    var maja = new NPC("Maja", questsMaja, outside, itemsMaja);

                    // Dodajemy NPC do pokoju i globalnej listy NPCs
                    outside.NPCs.Add(marek);
                    outside.NPCs.Add(camille);
                    outside.NPCs.Add(maja);


                    AddNPC(marek);
                    AddNPC(maja);
                    AddNPC(camille);
                }
            }

            if (Rooms.TryGetValue("Recepcja", out var receptionRoom))
            {
                var responses = new[] { "Zapytaj o klucz A-L (np. *D*), pralnię (*pralnia*) lub muzykę (*muzyka*)." };

                var cieć = new CiecNPC(
                    name: "Cieć",
                    dialogueText: "Dzień dobry! Jak mogę pomóc?",
                    responses: responses,
                    currentRoom: receptionRoom,
                    offeredItems: KeyItems ?? new List<Item>()
                );

                receptionRoom.NPCs.Add(cieć);
                AddNPC(cieć);
            }

            if (Rooms.TryGetValue("Korytarz 1", out var korytarz))
            {
                if (Items.TryGetValue("spray na mrówki", out var spray) &&
                    Items.TryGetValue("niesamowita niebieska łapka na owady", out var nnłp))
                {
                    var zosia = new DialogueNPC(
                         name: "Zofia",
                         dialogueText: "Uważaj, w akademiku roi się od mrówek, lepiej weź ze sobą ten spray, wystarczy ci go, aż do końca roku.",
                         responses: new string[] { "Dzięki.", "Myślę, że da się przeżyć bez... No dobra wezmę go." },
                         offeredItems: new List<Item> {spray},
                         currentRoom: korytarz
                     );

                     korytarz.NPCs.Add(zosia);
                     AddNPC(zosia);

                    var krzysiek = new DialogueNPC(
                         name: "Krzysztof",
                         dialogueText: "Nie zapomnij o osach, przyda ci sie ta łapka.",
                         responses: new string[] {"Dzięki.", "Mam doświadczenie w radzeniu sobie z osami. Wezmę, ale tylko tak na wszelki wypadek." },
                         offeredItems: new List<Item> {nnłp},
                         currentRoom: korytarz
                     );

                    korytarz.NPCs.Add(krzysiek);
                    AddNPC(krzysiek);
                }
            }

            if (Rooms.TryGetValue("pokój 107", out var room107))
            {
                if (Items.TryGetValue("klucz do pralni", out var kluczdopralni))
                {

                    var ola = new DialogueNPC(
                        name: "Ola",
                        dialogueText: "Cześć, pewnie jesteś tu po klucz do pralni. Sorki, faktycznie zapomniałam go odnieść, proszę.",
                        responses: new string[] {"Dzięki, nic się nie stało.", "Masz szczęście, że mieszkasz na 1 piętrze." },
                        offeredItems: new List<Item> {kluczdopralni},
                        currentRoom: room107
                    );

                    room107.NPCs.Add(ola);
                    AddNPC(ola);
                }
            }

            if (Rooms.TryGetValue("Składzik", out var storage))
            {
                if (Items.TryGetValue("Plakietka ppoż", out var plak) &&
                    Items.TryGetValue("Czapka", out var czap))
                {
                    var jann = new DialogueNPC(
                        name: "Janek",
                        dialogueText: "Przez te mrówki zgubiłem czapkę, gdzieś na prawo od korytarza na 3 piętrze, jak pomożesz mi ją odzyskać to chętnie przekażę ci swoją plakietkę ppoż, jest bardzo ładna",
                        responses: new string[] {"Jasne, trzymaj czapkę.", "Nie wiem czy to dobry deal, ale niech będzie." },                     
                        currentRoom: storage,
                        requiredItem: czap,
                        rewardItem: plak
                    );

                    storage.NPCs.Add(jann);
                    AddNPC(jann);
                }
            }

            if (Rooms.TryGetValue("Kuchnia K", out var kitchK))
            {
                if (Items.TryGetValue("płyn do mycia szyb", out var płynn))
                {
                    var natalia = new DialogueNPC(
                        name: "Natalia",
                        dialogueText: "Szukasz płynu, żeby umyć okna? Faktycznie powinien gdzieś tu być....Aaaa już wiem, znajoma z modułu C go pożyczała, poszukaj go tam.",
                        responses: new string[] {"Dzięki, sprawdzę to.", "Mam nadzieje, że był kupiony za twoje pieniądze..." },                     
                        offeredItems: new List<Item> {płynn},
                        currentRoom: kitchK
                    );

                    kitchK.NPCs.Add(natalia);
                    AddNPC(natalia);
                }
            }

            if (Rooms.TryGetValue("pokój 308", out var room308))
            {
                var maelle = new DialogueNPC(
                    name: "Maelle",
                    dialogueText: "Hej, właśnie wychodzę, znajomi już czekają na dole, aby pomóc mi z walizkami. Dzięki za wspólnie spędzony czas, ty i maja byłyście naprawdę wspaniałymi współlokatorkami. Będe tęsknić <3.",
                    responses: new string[] {"My też, au revoir!", "Mam nadzieję, że kiedyś jeszcze odwiedzisz Polskę. Pa pa." },
                    currentRoom: room308
                );

                room308.NPCs.Add(maelle);
                AddNPC(maelle);
            }

            //reszta npc nie ma udzialu w zadnych questach, generuje ich losowo
            var npcLocations = new[]
            {
                "Kuchnia B", "Zsyp 1",
                "Przedpokój EF", "klatkaSchodowa2", "pokój 207", "pokój 208",
                "Korytarz 3", "Korytarz 3", "Korytarz 3",
                "pokój 301"
            };

            var names = new List<string>
            {
                "Zenon", "Ania",
                "Tomek", "Ewa", "Mateusz", "Basia",
                "Karolina", "Grzegorz", "Magda",
                "Paweł"
            };

            var dialogueTemplates = new List<(string text, string[] responses)>
            {
                ("Ale dziś pada, co?", new[] { "Tak, masakra.", "Nie zauważyłem.", "*Ignoruj*" }),
                ("Lubisz deszczowe dni?", new[] { "Tak, mają klimat.", "Nie znoszę ich.", "*Ignoruj*" }),
                ("Widziałeś ostatnio tego kota z parteru?", new[] { "Tak, słodki!", "Nie, a co z nim?", "*Ignoruj*" }),
                ("Coś dziwnie pachnie na korytarzu, też czujesz?", new[] { "Tak, to pewnie kuchnia.", "Nie, nic nie czuję.", "*Ignoruj*" }),
                ("Słyszałeś imprezę wczoraj wieczorem?", new[] { "Tak, nie dało się spać.", "Nie, spałem jak kamień.", "*Ignoruj*" }),
                ("Zauważyłeś, że światło miga na klatce?", new[] { "Tak, strasznie to wygląda.", "Nie, pierwszy raz słyszę.", "*Ignoruj*" }),
                ("Co sądzisz o nowych lokatorach?", new[] { "Wyglądają sympatycznie.", "Jeszcze ich nie poznałem.", "*Ignoruj*" }),
                ("Masz jakąś ulubioną kuchnię w akademiku?", new[] { "Tak, Kuchnia K jest najlepsza!", "Nie, wszystkie są brudne.", "*Ignoruj*" }),
                ("Czasem myślę, że ten budynek żyje własnym życiem...", new[] { "Haha, serio.", "Trochę przesadzasz.", "*Ignoruj*" })
            };

            var rand = new Random();

            for (int i = 0; i < npcLocations.Length && i < names.Count; i++)
            {
                var roomName = npcLocations[i];
                if (Rooms.TryGetValue(roomName, out var room))
                {
                    string name = names[i];

                    var (dialogueText, responses) = dialogueTemplates[rand.Next(dialogueTemplates.Count)];

                    var npc = new DialogueNPC(
                        name: name,
                        dialogueText: dialogueText,
                        responses: responses,
                        currentRoom: room
                    );

                    room.NPCs.Add(npc);
                    AddNPC(npc);
                }
            }
        }

        private InteractiveObject CreateLaundryMachine(string name)
        {
            var pralka = new InteractiveObject(name, "Stoi stara pralka.");

            var poczatkoweRzeczy = name == "Pralka 2"
                ? new List<Item> { new Item("Kolorowe pranie", "Tylko nie z białym! :)") }
                : new List<Item> { new Item("Białe pranie", "Wyobraż sobie, co by się stało gdyby pomieszać je z kolorowym!") };

            pralka.SetState("rzeczy", poczatkoweRzeczy);
            pralka.SetState("pracuje", false);
            pralka.SetState("program", 0); //brak programu

            pralka.AddOption(
                "Wyjmij przedmiot z pralki",
                player => ((List<Item>)pralka.GetState<List<Item>>("rzeczy")).Count > 0,
                player =>
                {
                    var rzeczy = pralka.GetState<List<Item>>("rzeczy");
                    Console.WriteLine("Wybierz numer przedmiotu do wyjęcia:");
                    for (int i = 0; i < rzeczy.Count; i++)
                        Console.WriteLine($"{i + 1}. {rzeczy[i].Name}");

                    string? input = Console.ReadLine()?.Trim();
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= rzeczy.Count)
                    {
                        var item = rzeczy[choice - 1];
                        rzeczy.RemoveAt(choice - 1);
                        player.AddItem(item);
                        Console.WriteLine($"Wyjąłeś z pralki: {item.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wybór.");
                    }
                });

            pralka.AddOption(
                "Włóż przedmiot do pralki",
                player => player.Inventory.Count > 0,
                player =>
                {
                    Console.WriteLine("Wybierz numer przedmiotu z ekwipunku do włożenia:");
                    for (int i = 0; i < player.Inventory.Count; i++)
                        Console.WriteLine($"{i + 1}. {player.Inventory[i].Name}");

                    string? input = Console.ReadLine()?.Trim();
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= player.Inventory.Count)
                    {
                        var item = player.Inventory[choice - 1];
                        player.Inventory.RemoveAt(choice - 1);
                        var rzeczy = pralka.GetState<List<Item>>("rzeczy");
                        rzeczy.Add(item);
                        Console.WriteLine($"Włożyłeś do pralki: {item.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wybór.");
                    }
                });

            // Opcje programów:
            pralka.AddOption(
                "Ustaw program 1h",
                player => !pralka.GetState<bool>("pracuje"),
                player =>
                {
                    pralka.SetState("program", 1);
                    Console.WriteLine("Ustawiono program na 1 godzinę.");
                });

            pralka.AddOption(
                "Ustaw program 3h",
                player => !pralka.GetState<bool>("pracuje"),
                player =>
                {
                    pralka.SetState("program", 3);
                    Console.WriteLine("Ustawiono program na 3 godziny.");
                    player.CheckAllQuests();
                });

            pralka.AddOption(
                "Ustaw program 5h",
                player => !pralka.GetState<bool>("pracuje"),
                player =>
                {
                    pralka.SetState("program", 5);
                    Console.WriteLine("Ustawiono program na 5 godzin.");
                    player.CheckAllQuests();
                });

            pralka.AddOption(
                "Uruchom pralkę",
                player =>
                {
                    var rzeczy = pralka.GetState<List<Item>>("rzeczy") ?? new List<Item>();
                    var pracuje = pralka.GetState<bool>("pracuje");
                    var program = pralka.GetState<int>("program");
                    return rzeczy.Count > 0 && !pracuje && program > 0;
                },
                player =>
                {
                    pralka.SetState("pracuje", true);
                    Console.WriteLine("Pralka uruchomiona.");
                });

            return pralka;
        }

        private void InitializeObjects()
        {
            if (Rooms.TryGetValue("Pralnia", out var laundry))
            {
                var pralka1 = new InteractiveObject("Pralka 1", "Stoi stara pralka. Wygląda na zepsutą.");
                pralka1.SetState("naprawiona", false);

                pralka1.AddOption("Napraw pralkę",
                    player => player.HasItem("poradnik hydrauliczny") && !pralka1.GetState<bool>("naprawiona"),
                    player =>
                    {
                        pralka1.SetState("naprawiona", true);
                        Console.WriteLine("Naprawiłeś pralkę korzystając z Poradnika hydraulika. Teraz działa!");
                        player.CheckAllQuests();
                    });

                pralka1.AddOption("Włącz pralkę",
                    player => !pralka1.GetState<bool>("włączona"),
                    player =>
                    {
                        pralka1.SetState("włączona", true);
                        Console.WriteLine("Pralka została włączona.");
                    });

                pralka1.AddOption("Wyłącz pralkę",
                    player => pralka1.GetState<bool>("włączona"),
                    player =>
                    {
                        pralka1.SetState("włączona", false);
                        Console.WriteLine("Pralka została wyłączona.");
                    });

                laundry.AddObject(pralka1);

                var pralka2 = CreateLaundryMachine("Pralka 2");
                var pralka3 = CreateLaundryMachine("Pralka 3");

                laundry.AddObject(pralka2);
                laundry.AddObject(pralka3);
            }

            if (Rooms.TryGetValue("Zsyp 1", out var zs1))
            {
                var kontener = new InteractiveObject("Kontener na śmieci", "Możesz go nakarmić!");
                kontener.SetState("pełny", false);
                kontener.SetState("śmieci", new List<Item>());

                kontener.AddOption("Wyrzuć śmieci",
                    player => player.HasItem("Śmieci") && !kontener.GetState<bool>("pełny"),
                    player =>
                    {
                        var item = player.Inventory.Find(i => i.Name == "Śmieci");
                        if (item != null)
                        {
                            player.Inventory.Remove(item);
                            kontener.GetState<List<Item>>("śmieci").Add(item);
                            kontener.SetState("pełny", true);
                            Console.WriteLine("Wyrzuciłeś śmieci.");
                            player.CheckAllQuests();
                        }
                    });

                zs1.AddObject(kontener);
            }

            if (Rooms.TryGetValue("Zsyp 3", out var zs3))
            {
                var kontener2 = new InteractiveObject("Kontener na śmieci", "Jest już pełny, spróbuj może na 1 piętrze...");
                kontener2.SetState("pełny", true);
                zs3.AddObject(kontener2);
            }

            if (Rooms.TryGetValue("Kuchnia G", out var kuchniaG))
            {
                var zlew = new InteractiveObject("Zlew", "Jeszcze z czasów PRL.");
                zlew.SetState("zatkany", true);

                zlew.AddOption("Odetkaj",
                    player => player.HasItem("Przepychaczka") && zlew.GetState<bool>("zatkany"),
                    player =>
                    {
                        zlew.SetState("zatkany", false);
                        Console.WriteLine("Odetkałeś zlew!");
                        player.CheckAllQuests();
                    });

                kuchniaG.AddObject(zlew);
            }

            if (Rooms.TryGetValue("pokój 203", out var pokoj203))
            {
                var grzejnik = new InteractiveObject("Grzejnik", "Ładny, biały.");
                grzejnik.SetState("zapowietrzony", true);

                grzejnik.AddOption("Napraw",
                    player => player.HasItem("Poradnik hydraulika") && grzejnik.GetState<bool>("zapowietrzony"),
                    player =>
                    {
                        grzejnik.SetState("zapowietrzony", false);
                        Console.WriteLine("Grzejnik znowu śmiga!");
                        player.CheckAllQuests();
                    });

                pokoj203.AddObject(grzejnik);
            }

            if (Rooms.TryGetValue("Kuchnia K", out var kuchniaK))
            {
                var smietnik = new InteractiveObject("Śmietnik", "..");
                smietnik.SetState("pełny", true);
                smietnik.SetState("śmieci", new List<Item>());

                smietnik.AddOption("Wyjmij śmieci",
                    player => smietnik.GetState<List<Item>>("śmieci").Count > 0,
                    player =>
                    {
                        var smieci = smietnik.GetState<List<Item>>("śmieci");
                        Console.WriteLine("Wybierz numer przedmiotu do wyjęcia:");
                        for (int i = 0; i < smieci.Count; i++)
                            Console.WriteLine($"{i + 1}. {smieci[i].Name}");

                        var input = Console.ReadLine()?.Trim();
                        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= smieci.Count)
                        {
                            var item = smieci[choice - 1];
                            smieci.RemoveAt(choice - 1);
                            player.AddItem(item);
                            Console.WriteLine("Opróżniłeś śmietnik");
                            smietnik.SetState("pełny", false);
                        }
                    });

                kuchniaK.AddObject(smietnik);
            }

            if (Rooms.TryGetValue("Łazienka J", out var lazienkaJ))
            {
                var prysznic = new InteractiveObject("Prysznic", "Troszkę brudny.");
                prysznic.SetState("zepsuty", true);

                prysznic.AddOption("Wymień syfon",
                    player => player.HasItem("Nowy syfon") && prysznic.GetState<bool>("zepsuty"),
                    player =>
                    {
                        prysznic.SetState("zepsuty", false);
                        Console.WriteLine("Naprawiłeś prysznic!");
                        player.CheckAllQuests();
                    });

                lazienkaJ.AddObject(prysznic);
            }

            if (Rooms.TryGetValue("pokój 307", out var pokoj307))
            {
                var okna = new InteractiveObject("Okna", "Widać z nich plac Grunwaldzki");
                okna.SetState("brudne", true);

                okna.AddOption("Umyj",
                    player => player.HasItem("płyn do mycia szyb ") && okna.GetState<bool>("brudne"),
                    player =>
                    {
                        okna.SetState("brudne", false);
                        Console.WriteLine("Okna są czyściutkie!");
                        player.CheckAllQuests();
                    });

                pokoj307.AddObject(okna);
            }     
            //puste sciany - miejsca na reklame
            Random rng = new Random();

            foreach (var room in Rooms.Values)
            {
                if (rng.NextDouble() <= 0.3) // 30% szans
                {
                    var adSpot = new AdSpot();
                    room.Objects.Add(adSpot);
                }
            }
        }
        private void InitializeCreatures()
        {
            foreach (var room in Rooms.Values)
            {
                room.GenerateCreatures();
            }

            // w przedpokoju KL jest specjalna mrówka
            var targetRoom = Rooms["Przedpokój KL"];
            var specialIt = Items["Czapka"];
            var specialAnt = new Ant();
            specialAnt.stolenItems.Add(specialIt);
            targetRoom.AddCreatureInRoom(specialAnt);

            // w lazeince K musi byc mrowka
            var mrowkawlazience = new Ant();
            var laz = Rooms["Łazienka K"];
            laz.AddCreatureInRoom(mrowkawlazience);
            
        }
        
        private void GameLoop()
        {
            CommandHandler commandHandler = new CommandHandler(this);
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                commandHandler.HandleInput(input);
                Player.CheckAllQuests();
                if (Player.AllQuestsCompleted) EndGame("Ukończyłeś wszystkie questy, a tym samym całą grę!");
            }
        }
        public static void EndGame(string reason)
        {
            Console.WriteLine("=== KONIEC GRY ===");
            Console.WriteLine(reason);
            Environment.Exit(0);
        }

    }

