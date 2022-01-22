using ClosedXML.Excel;
using FastMember;
using System.Data;

namespace AdressBook.Services
{
    public class XLSXService : IExportService
    {
        private IContactService _contactService;

        public XLSXService(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<DataTable> getData()
        {
            var adresses = await _contactService.GetAllContactAsync();
            DataTable table = new DataTable();
            table.TableName = "Adresses";
            using (var reader = ObjectReader.Create(adresses))
            {
                table.Load(reader);
            }
            table.AcceptChanges();
            return table;
        }
    }
}
