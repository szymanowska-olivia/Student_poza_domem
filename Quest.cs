using System;
using System.Linq;
public class Quest
{
    public string Name { get; }
    public string Description { get; }
    private bool completed;
    private Func<Player, bool> completionCondition;

    public Quest(string name, string description, Func<Player, bool> completionCondition)
    {
        Name = name;
        Description = description;
        this.completionCondition = completionCondition;
        completed = false;
    }

    public bool IsComplete()
    {
        return completed;
    }

    public void CheckCompletion(Player player)
    {
        if (!completed && completionCondition(player))
        {
            completed = true;
            Console.WriteLine($"Zadanie '{Name}' zostało ukończone!");
        }
    }
}
