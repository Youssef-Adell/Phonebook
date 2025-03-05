using Microsoft.EntityFrameworkCore;
using Phonebook.Domain.Entities;

namespace Phonebook.Infrastructure.Persistence.EFConfig;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}
