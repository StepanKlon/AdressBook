using AdressBook.Models.Entities;

namespace AdressBook.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public string? Search { get; set; }
    }
}
