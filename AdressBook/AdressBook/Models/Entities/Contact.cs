using AdressBook.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace AdressBook.Models.Entities
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public string? Comment { get; set; }

        public Contact(ContactViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            DateOfBirth = model.DateOfBirth;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Adress = model.Adress;
            Comment = model.Comment;
        }
        public Contact()
        {

        }
    }
}
