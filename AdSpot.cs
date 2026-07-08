#nullable enable
using System;
using System.Linq;

public class AdSpot : InteractiveObject
{
    public Item? HungAd { get; private set; } = null;

    public AdSpot() : base("Pusta ściana...","Na prawdę pusta")
    {
    }

    public override void Interact(Player player)
    {
        if (HungAd != null)
        {
            Console.WriteLine("Na tym miejscu jest już powieszona reklama: " + HungAd.Name);
            return;
        }

        var ad = player.Inventory.FirstOrDefault(item => item.IsAd);

        if (ad == null)
        {
            Console.WriteLine("Nie masz żadnej reklamy do powieszenia.");
            return;
        }

        HungAd = ad;
        player.RemoveItem(ad);
        Console.WriteLine($"Powiesiłeś reklamę '{ad.Name}'");
        player.AdsCnt++;
        var quest = player.ActiveQuests.FirstOrDefault(q => q.Name == "Wywieś reklamy");
        if (quest != null)
        {
            quest.CheckCompletion(player);
        }
    }
}
