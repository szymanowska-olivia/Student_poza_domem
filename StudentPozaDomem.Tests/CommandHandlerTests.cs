using System;
using System.IO;
using NUnit.Framework;

namespace StudentPozaDomem.Tests
{
    [TestFixture]
    public class CommandHandlerTests
    {
        private CommandHandler _commandHandler = null!;
        private StringWriter _consoleOutput = null!;
        private TextWriter _originalOutput = null!;

        [SetUp]
        public void SetUp()
        {
            _consoleOutput = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_consoleOutput);

            var room = new Room("Start", "Opis");
            var game = new Game(); 
            game.Player = new Player(room, game);

            _commandHandler = new CommandHandler(game);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_originalOutput);
            _consoleOutput.Dispose();
        }

        [Test]
        public void HandleInput_HelpCommand_OutputsAvailableCommands()
        {
            // Act
            _commandHandler.HandleInput("help");

            // Assert
            var output = _consoleOutput.ToString();
            Assert.That(output, Does.Contain("Dostępne komendy:"), "Komenda 'help' powinna wyświetlać listę instrukcji.");
            Assert.That(output, Does.Contain("- idz [kierunek]"));
        }

        [Test]
        public void HandleInput_UnknownCommand_OutputsErrorMessage()
        {
            // Act
            _commandHandler.HandleInput("niestworzona_komenda");

            // Assert
            var output = _consoleOutput.ToString();
            Assert.That(output, Does.Contain("Nieznana komenda."), "Błędne wejście powinno poinformować gracza o braku komendy.");
        }

        [TestCase("HELP")]
        [TestCase("hElP")]
        [TestCase("Help")]
        public void HandleInput_IsCaseInsensitive_ForCommands(string inputCommand)
        {
            // Act
            _commandHandler.HandleInput(inputCommand);

            // Assert
            var output = _consoleOutput.ToString();
            Assert.That(output, Does.Contain("Dostępne komendy:"), "Metoda powinna ignorować wielkość liter ('ToLower').");
        }

        [Test]
        public void HandleInput_PodniesCommand_WithoutArgument_OutputsError()
        {
            // Act
            _commandHandler.HandleInput("podnieś");

            // Assert
            var output = _consoleOutput.ToString();
            Assert.That(output, Does.Contain("Musisz podać nazwę przedmiotu"), "Obsługa braku argumentu w komendzie dwuczłonowej.");
        }
    }
}