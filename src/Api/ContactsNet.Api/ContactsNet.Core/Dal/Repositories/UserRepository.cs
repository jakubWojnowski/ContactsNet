using System.Linq.Expressions;
using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactsNet.Core.Dal.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ContactsNetDbContext _dbContext;

    public UserRepository(ContactsNetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<IList<User?>> GetAllAsync()
    {
        return await _dbContext.Users.AsQueryable().ToListAsync();
    }
    
    public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
    
    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<User?> GetRecordByFilterAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AsQueryable().FirstOrDefaultAsync(filter, cancellationToken: cancellationToken);
    }
}