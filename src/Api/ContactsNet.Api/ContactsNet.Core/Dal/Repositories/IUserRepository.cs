using System.Linq.Expressions;
using ContactsNet.Core.Dal.Entities;

namespace ContactsNet.Core.Dal.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
    Task<IList<User?>> GetAllAsync();
    Task<Guid> AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetRecordByFilterAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken);
}