using AdressBook.Core.IConfiguration;
using AdressBook.Core.IRepositories;
using AdressBook.Core.Repositories;

namespace AdressBook.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public IContactRepository Contacts { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Contacts = new ContactRepository(_context);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
