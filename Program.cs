#nullable enable
using System;


    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();

            var handler = new CommandHandler(game);

            while (true)
            {
                Console.Write("\n> ");
                string? input = Console.ReadLine();
                handler.HandleInput(input);
            }
        }
    }
