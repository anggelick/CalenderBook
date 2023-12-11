using Xunit;
using ConsoleApp;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ConsoleApp.Tests
{
    /// <summary>
    /// Testklass för AddressBook-funktionalitet.
    /// </summary>
    public class AddressBookTests
    {
        /// <summary>
        /// Test som verifierar att läggning av en kontakt ökar antalet kontakter.
        /// </summary>
        [Fact]
        public void AddContactShouldIncreaseContactCount()
        {
            // Arrange
            var addressBook = new Program.AddressBook();
            var contact = new Program.AddressBook.Contact
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789",
                Email = "john.doe@example.com",
                Address = "123 Main Street"
            };

            // Act
            addressBook.AddContact(contact);

            // Assert
            Assert.Contains(contact, addressBook.contacts);
        }
    }
}
