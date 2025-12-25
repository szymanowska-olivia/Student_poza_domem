using System;
using System.Linq;

public abstract class Creature
{
     public Creature()
    {

    }

    public abstract void Interact(Player player, Room room, Game game);
}
