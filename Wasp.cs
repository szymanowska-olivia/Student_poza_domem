using System;
using System.Linq;

public class Wasp : Creature
{
    private static Random rand = new Random();
    public override void Interact(Player player, Room room, Game game)
    {
        Console.WriteLine("Zaatakowała Cię osa!");

        var specialItem = player.Inventory.FirstOrDefault(i => i.Name.ToLower() == "niesamowita niebieska łapka na owady");

        if (specialItem != null)
        {
            Console.WriteLine("Używasz niesamowitej niebieskiej łapki na owady i pokonujesz osę bez problemu.");
            room.RemoveCreature();
            return;
        }

        bool survived = rand.NextDouble() < 0.1; // 10% szans

        if (survived)
        {
            Console.WriteLine("Udało Ci się przegonić osę! Miałeś dużo szczęścia!");
        }
        else
        {
            Game.EndGame("Osa Cię ukąsiła! Karetka zabiera cię do szspitala. Ta sytuacja wywołuje u ciebie traume i już nie chcesz wracać do akademika.");

        }
    }

}
