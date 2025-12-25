using System;
using System.Collections.Generic;


    public class InteractiveObject
    {
        public string Name { get; }
        public string Description { get; }
        public List<Item> Items { get; } = new List<Item>();

        public Dictionary<string, object> State { get; } = new Dictionary<string, object>();

        private List<InteractionOption> options = new();

        public InteractiveObject(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void SetState(string key, object value) => State[key] = value;

        public T? GetState<T>(string key)
        {
            if (State.TryGetValue(key, out var value) && value is T typedValue)
                return typedValue;
            return default;
        }

        public void AddOption(string label, Func<Player, bool> condition, Action<Player> action)
        {
            options.Add(new InteractionOption(label, condition, action));
        }

        public virtual void Interact(Player player)
        {
            Console.WriteLine($"\n{ Name }: { Description }");

            for (int i = 0; i < options.Count; i++)
            {
                var opt = options[i];
                bool available = opt.IsAvailable(player);
                Console.WriteLine($"{i + 1}. {opt.Label}" + (available ? "" : " (niedostępne)"));
            }

            Console.Write("Wybierz opcję (lub wpisz 'q' aby wyjść): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input) || input.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Zrezygnowano z interakcji.");
                return;
            }

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Count)
            {
                var selected = options[choice - 1];
                if (selected.IsAvailable(player))
                    selected.Execute(player);
                else
                    Console.WriteLine("Nie możesz teraz tego zrobić.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
}
        }

        private class InteractionOption
        {
            public string Label { get; }
            private Func<Player, bool> Condition;
            private Action<Player> Action;

            public InteractionOption(string label, Func<Player, bool> condition, Action<Player> action)
            {
                Label = label;
                Condition = condition ?? (_ => true);
                Action = action;
            }

            public bool IsAvailable(Player player) => Condition(player);
            public void Execute(Player player) => Action(player);
        }
    }

