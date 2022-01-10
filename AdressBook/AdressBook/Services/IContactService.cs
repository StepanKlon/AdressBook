using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;

namespace AdressBook.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactAsync();
        IEnumerable<Contact> GetAllContact(string name);
        Task<Contact?> GetContactAsync(long id);
        Task<bool> AddContactAsync(ContactViewModel contact);
        Task<bool> RemoveContactAsync(long id);
        Task<bool> UpdateContactAsync(ContactViewModel model);
    }
}
