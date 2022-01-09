using AdressBook.Core.IConfiguration;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using System.Collections.Generic;

namespace AdressBook.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddContactAsync(ContactViewModel contact)
        {
            try
            {
                if (contact == null)
                    return false;
                var isAdded = await _unitOfWork.Contacts.Add(new Contact(contact));
                await _unitOfWork.CompleteAsync();
                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Contact>> GetAllContactAsync()
        {
            var contacts = await _unitOfWork.Contacts.All();
            IEnumerable<Contact> emptyContacts = new List<Contact>();
            if (contacts is null)
                return emptyContacts;
            return contacts;
        }

        public IEnumerable<Contact> GetAllContact(string name)
        {
            var contacts = _unitOfWork.Contacts.All(name);
            if (contacts is null || contacts.Count() == 0)
                return new List<Contact>();
            return contacts;
        }

        public async Task<Contact?> GetContactAsync(long id)
        {
            return await _unitOfWork.Contacts.GetById(id);
        }

        public async Task<bool> RemoveContact(long id)
        {
            try
            {
                var contact = await _unitOfWork.Contacts.GetById(id);
                if (contact is null)
                    return false;
                var isRemoved = _unitOfWork.Contacts.Delete(contact);
                await _unitOfWork.CompleteAsync();
                return isRemoved;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateContact(ContactViewModel model)
        {
            try
            {
                var contact = new Contact(model);
                var isUpdated = _unitOfWork.Contacts.Update(contact);
                await _unitOfWork.CompleteAsync();
                return isUpdated;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
