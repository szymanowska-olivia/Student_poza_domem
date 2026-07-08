using System;
using NUnit.Framework;

namespace StudentPozaDomem.Tests
{
    [TestFixture]
    public class RoomTests
    {
        private Room? _room;
        private Player? _fakePlayer;

        [SetUp]
        public void Setup()
        {
            _room = new Room("Sala", "Wykrywasz aurę zaliczeń!");
            _fakePlayer = new Player(_room, new Game());
        }

        [Test]
        public void Describe_ReturnsCorrectBasicDescription_WhenRoomIsEmpty()
        {
            // Act
            var description = _room!.Describe(_fakePlayer!);

            // Assert
            Assert.Multiple((Action)(() =>
            {
                Assert.That(description, Does.Contain("Sala"));
                Assert.That(description, Does.Contain("Wykrywasz aurę zaliczeń!"));
                Assert.That(description.Contains("Widzisz tutaj przedmioty"), Is.False);
            }));
        }

        [Test]
        public void Describe_IncludesItemsInOutput_WhenItemsArePresent()
        {
            // Arrange
            _room!.AddItem(new Item("Indeks", string.Empty));
            _room.AddItem(new Item("Długopis", string.Empty));

            // Act
            var description = _room.Describe(_fakePlayer!);

            // Assert
            Assert.That(description, Does.Contain("Widzisz tutaj przedmioty: Indeks, Długopis."), 
                "System opisowy pokoju musi wylistować przedmioty, gdy te znajdują się na podłodze.");
        }
    }
}