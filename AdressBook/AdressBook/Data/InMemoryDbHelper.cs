using AdressBook.Models.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AdressBook.Data
{
    public static class InMemoryDbHelper
    {
        public static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));

            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
            var context = new AppDbContext(options);

            context.Database.OpenConnectionAsync();
            context.Database.EnsureCreatedAsync();
            InMemoryDbHelper.CreateBasicData(context);
        }

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
