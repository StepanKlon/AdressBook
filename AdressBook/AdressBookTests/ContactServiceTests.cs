using AdressBook.Core.IConfiguration;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AdressBook.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AdressBookTests
{
    public class ContactServiceTests
    {

        //AddContactAsync

        [Fact]
        public void AddContactAsync_AddingIsCorrect_ReturnTrue()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.Add(It.IsAny<Contact>())).ReturnsAsync(true);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.AddContactAsync(new ContactViewModel()).Result;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void AddContactAsync_AddingIsNotCorrect_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.Add(It.IsAny<Contact>())).ReturnsAsync(false);
            var contactService = new ContactService(mockService.Object);
            
            //Act
            var result = contactService.AddContactAsync(new ContactViewModel()).Result;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddContactAsync_AddingThrewException_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.Add(It.IsAny<Contact>())).ReturnsAsync(true);
            mockService.Setup(x => x.CompleteAsync()).Callback(() => throw new Exception());
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.AddContactAsync(new ContactViewModel()).Result;

            //Assert
            Assert.False(result);
        }

        //GetAllContactAsync

        [Fact]
        public void GetAllContactAsync_ContactsAreCorrectlyLoaded_ReturnListOfContacts()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contacts = new List<Contact> { new Contact { FirstName = "Stepan", LastName = "Klon" } };
            mockService.Setup(x => x.Contacts.All()).ReturnsAsync(contacts);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.GetAllContactAsync().Result;

            //Assert
            Assert.Equal(contacts,result);
        }

        //GetAllContact

        [Fact]
        public void GetAllContact_ContactsAreCorrectlyLoaded_ReturnListOfContact()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contacts = new List<Contact> { new Contact { FirstName = "Stepan", LastName = "Klon" } };
            mockService.Setup(x => x.Contacts.All(It.IsAny<string>())).Returns(contacts);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.GetAllContact("Stepan");

            //Assert
            Assert.Equal(contacts, result);
        }

        //GetContactAsync

        [Fact]
        public void GetContactAsync_ContactFound_ReturnContact()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new Contact { FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync(contact);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.GetContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.Equal(contact, result);
        }

        [Fact]
        public void GetContactAsync_ContactNotFound_ReturnNull()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync((Contact?)null);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.GetContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.Null(result);
        }

        //RemoveContactAsync

        [Fact]
        public void RemoveContactAsync_ContactFoundAndRemoved_ReturnTrue()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new Contact { FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync(contact);
            mockService.Setup(x => x.Contacts.Delete(It.IsAny<Contact>())).Returns(true);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.RemoveContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveContactAsync_ContactNotFound_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync((Contact?)null);
            mockService.Setup(x => x.Contacts.Delete(It.IsAny<Contact>())).Returns(true);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.RemoveContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveContactAsync_ContactFoundAndNotDeleted_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new Contact { FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync(contact);
            mockService.Setup(x => x.Contacts.Delete(It.IsAny<Contact>())).Returns(false);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.RemoveContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveContactAsync_CompleteAsyncThrewExeption_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new Contact { FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.GetById(It.IsAny<long>())).ReturnsAsync(contact);
            mockService.Setup(x => x.Contacts.Delete(It.IsAny<Contact>())).Returns(true);
            mockService.Setup(x => x.CompleteAsync()).Callback(() => throw new Exception());
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.RemoveContactAsync(It.IsAny<long>()).Result;

            //Assert
            Assert.False(result);
        }

        //UpdateContactAsync

        [Fact]
        public void UpdateContactAsync_ContactUpdatedCorrectly_ReturnTrue()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new ContactViewModel {Id = 1 ,FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.Update(It.IsAny<Contact>())).Returns(true);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.UpdateContactAsync(contact).Result;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateContactAsync_ContactViewModelIsNull_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(x => x.Contacts.Update(It.IsAny<Contact>())).Returns(true);
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.UpdateContactAsync(It.IsAny<ContactViewModel>()).Result;

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateContactAsync_CompleteAsyncThrewExeption_ReturnFalse()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            var contact = new ContactViewModel { Id = 1, FirstName = "Stepan", LastName = "Klon" };
            mockService.Setup(x => x.Contacts.Update(It.IsAny<Contact>())).Returns(true);
            mockService.Setup(x => x.CompleteAsync()).Callback(() => throw new Exception());
            var contactService = new ContactService(mockService.Object);

            //Act
            var result = contactService.UpdateContactAsync(contact).Result;

            //Assert
            Assert.False(result);
        }
    }
}