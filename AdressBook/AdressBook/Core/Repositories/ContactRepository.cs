using AdressBook.Core.IRepositories;
using AdressBook.Data;
using AdressBook.Models.Entities;

namespace AdressBook.Core.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Contact>? All(string search)
        {
            try
            {
                return context.Contacts.Where(c =>
                    c.FirstName.ToLower().Contains(search.ToLower()) ||
                    c.LastName.ToLower().Contains(search.ToLower()));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
