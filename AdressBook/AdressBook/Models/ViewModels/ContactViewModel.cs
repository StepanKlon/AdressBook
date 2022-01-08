using AdressBook.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdressBook.Models.ViewModels
{
    public class ContactViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; } = "";
        public string? Email { get; set; } = "";
        public string? Adress { get; set; } = "";
        public string? Comment { get; set; } = "";

        public ContactViewModel(Contact contact)
        {
            Id = contact.Id;
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            DateOfBirth = contact.DateOfBirth;
            PhoneNumber = contact.PhoneNumber;
            Email = contact.Email;
            Adress = contact.Adress;
            Comment = contact.Comment;
        }
        public ContactViewModel()
        {

        }
    }
}
