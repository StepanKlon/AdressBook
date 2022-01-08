using AdressBook.Models.Entities;

namespace AdressBook.Core.IRepositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        IEnumerable<Contact> All(string search);
    }
}
