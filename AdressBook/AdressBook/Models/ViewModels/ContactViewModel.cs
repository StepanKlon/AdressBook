using AdressBook.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdressBook.Models.ViewModels
{
    public class ContactViewModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(40,MinimumLength = 2)]
        public string FirstName { get; set; } = "";
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string LastName { get; set; } = "";
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        [Phone]
        [StringLength(40)]
        public string? PhoneNumber { get; set; } = "";
        [StringLength(40, MinimumLength = 10)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string? Email { get; set; } = "";
        [StringLength(40)]
        public string? Adress { get; set; } = "";
        [StringLength(40)]
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
