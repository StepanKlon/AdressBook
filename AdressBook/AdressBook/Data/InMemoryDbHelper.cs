using AdressBook.Models.Entities;

namespace AdressBook.Data
{
    public static class InMemoryDbHelper
    {
        public static void CreateBasicData(AppDbContext context)
        {
            var contact = new Contact
            {
                FirstName = "Stepan",
                LastName = "Klon",
                Adress = "Korytná 401, 68752 Korytná",
                Email = "klonstepan@gmail.com",
                PhoneNumber = "+420 776 765 853",
                DateOfBirth = new DateTime(1994, 11, 24),
                Comment = "From Green Fox Academy"
            };
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
    }
}
