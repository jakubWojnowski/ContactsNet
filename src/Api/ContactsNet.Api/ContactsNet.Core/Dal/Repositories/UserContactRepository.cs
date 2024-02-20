using System.Linq.Expressions;
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

    public async Task<UserContact?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserContacts.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<List<UserContact>> GetAllUserContactsAsync(Guid userId,CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserContacts.AsQueryable().Where(u => u.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Guid> AddAsync(UserContact userContact, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserContacts.AddAsync(userContact, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return userContact.Id;
    }

    public async Task UpdateAsync(UserContact userContact, CancellationToken cancellationToken = default)
    {
        _dbContext.UserContacts.Update(userContact);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(UserContact userContact, CancellationToken cancellationToken = default)
    {
        _dbContext.UserContacts.Remove(userContact);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<UserContact?> GetRecordByFilterAsync(Expression<Func<UserContact, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserContacts.AsQueryable().FirstOrDefaultAsync(filter, cancellationToken: cancellationToken);
    }
}