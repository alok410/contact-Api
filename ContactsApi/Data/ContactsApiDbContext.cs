using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data
{
    public class ContactsApiDbContext : DbContext
    {
        public ContactsApiDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Contact> Contacts { get; set; }
    }
}
