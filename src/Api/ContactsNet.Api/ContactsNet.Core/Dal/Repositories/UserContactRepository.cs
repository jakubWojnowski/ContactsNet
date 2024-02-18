using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactsNet.Core.Dal.Repositories;

internal class UserContactRepository : IUserContactRepository
{       
    private readonly ContactsNetDbContext _dbContext;

    public UserContactRepository(ContactsNetDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserContact?> GetAsync(Guid id)
    {
        return await _dbContext.UserContacts.FindAsync(id);
    }

    public async Task<List<UserContact>> GetAllAsync()
    {
        return await _dbContext.UserContacts.AsQueryable().ToListAsync();
    }
    
    public async Task<Guid> AddAsync(UserContact userContact)
    {
        await _dbContext.UserContacts.AddAsync(userContact);
        await _dbContext.SaveChangesAsync();
        return userContact.Id;
    }
    
    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}