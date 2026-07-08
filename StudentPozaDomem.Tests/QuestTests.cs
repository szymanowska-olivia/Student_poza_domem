using System;
using NUnit.Framework;

namespace StudentPozaDomem.Tests
{
    [TestFixture]
    public class QuestTests
    {
        private Player? _fakePlayer;
        private Room? _fakeRoom;

        [SetUp]
        public void Setup()
        {
            _fakeRoom = new Room("Baza", "Baza");
            var fakeGame = new Game(); 
            _fakePlayer = new Player(_fakeRoom, fakeGame);
        }

        [Test]
        public void CheckCompletion_SetsCompletedToTrue_WhenConditionIsMet()
        {
            // Arrange (Delegata zawsze zwraca true - np. gracz zebrał wszystko co miał mieć)
            var quest = new Quest("Test Questa", "Opis Questa", player => true);

            // Act
            quest.CheckCompletion(_fakePlayer!);

            // Assert
            Assert.That(quest.IsComplete(), Is.True, "Quest powinien zarejestrować, że warunek osiągnięcia sukcesu został spełniony.");
        }

        [Test]
        public void CheckCompletion_DoesNotSetCompletedToTrue_WhenConditionIsNotMet()
        {
            // Arrange
            var quest = new Quest("Trudny Quest", "Opis", player => false);

            // Act
            quest.CheckCompletion(_fakePlayer!);

            // Assert
            Assert.That(quest.IsComplete(), Is.False, "Quest nie powinien zakończyć się, skoro warunki nie są spełnione.");
        }
    }
}