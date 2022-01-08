using AdressBook.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdressBook.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
