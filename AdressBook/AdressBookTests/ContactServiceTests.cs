using AdressBook.Core.IConfiguration;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AdressBook.Services;
using Moq;
using Xunit;

namespace AdressBookTests
{
    public class ContactServiceTests
    {
        [Fact]
        public void GetAllContactAsync_AddingIsCorrect_ReturnTrue()
        {
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.Add(It.IsAny<Contact>())).ReturnsAsync(true);

            var contactService = new ContactService(mockService.Object);
            var result = contactService.AddContactAsync(new ContactViewModel()).Result;

            Assert.True(result);
        }
    }
}