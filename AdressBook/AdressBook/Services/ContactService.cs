using AdressBook.Core.IConfiguration;
using AdressBook.Models.Entities;
using AdressBook.Models.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace AdressBook.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddContactAsync(ContactViewModel contactVM)
        {
            try
            {
                var contact = _mapper.Map<Contact>(contactVM);
                var isAdded = await _unitOfWork.Contacts.Add(contact);
                await _unitOfWork.CompleteAsync();
                return isAdded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Contact>?> GetAllContactAsync()
        {
            return await _unitOfWork.Contacts.All();
        }

        public IEnumerable<Contact>? GetAllContact(string name)
        {
            return _unitOfWork.Contacts.All(name);
        }

        public async Task<Contact?> GetContactAsync(long id)
        {
            return await _unitOfWork.Contacts.GetById(id);
        }

        public async Task<bool> RemoveContactAsync(long id)
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

        public async Task<bool> UpdateContactAsync(ContactViewModel model)
        {
            try
            {
                var contact = _mapper.Map<Contact>(model);
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
