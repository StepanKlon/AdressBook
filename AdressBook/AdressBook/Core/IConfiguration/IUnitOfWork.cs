using AdressBook.Core.IRepositories;

namespace AdressBook.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IContactRepository Contacts { get; }
        Task CompleteAsync();
    }
}
