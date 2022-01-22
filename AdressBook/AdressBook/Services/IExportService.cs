using System.Data;

namespace AdressBook.Services
{
    public interface IExportService
    {
        Task<DataTable> getData();
    }
}
