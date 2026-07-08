using NUnit.Framework;

namespace StudentPozaDomem.Tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void Constructor_SetsNameAndDescriptionCorrectly()
        {
            // Arrange
            string expectedName = "Klucz";
            string expectedDescription = "Zardzewiały stary klucz.";

            // Act
            var item = new Item(expectedName, expectedDescription);

            // Assert
            Assert.Multiple((System.Action)(() =>
            {
                Assert.That(item.Name, Is.EqualTo(expectedName));
                Assert.That(item.Description, Is.EqualTo(expectedDescription));
                Assert.That(item.IsAd, Is.False, "Domyślnie IsAd powinno wynosić false.");
            }));
        }

        [Test]
        public void Inspect_ReturnsProperlyFormattedString()
        {
            // Arrange
            var item = new Item("Mapa", "Mapa kampusu");
            string expectedString = "Mapa: Mapa kampusu";

            // Act
            string result = item.Inspect();

            // Assert
            Assert.That(result, Is.EqualTo(expectedString));
        }

        [Test]
        public void IsAd_Property_CanBeModified()
        {
            // Arrange
            var item = new Item("Ulotka", "Reklama szkoły");

            // Act
            item.IsAd = true;

            // Assert
            Assert.That(item.IsAd, Is.True);
        }
    }
}