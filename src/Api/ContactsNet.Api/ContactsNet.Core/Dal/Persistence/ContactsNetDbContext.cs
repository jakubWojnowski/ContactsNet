using ContactsNet.Core.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsNet.Core.Dal.Persistence;

public class ContactsNetDbContext : DbContext
{
    public ContactsNetDbContext(DbContextOptions<ContactsNetDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
    
}