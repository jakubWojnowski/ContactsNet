using System.Linq.Expressions;
using ContactsNet.Core.Dal.Entities;

namespace ContactsNet.Core.Dal.Repositories;

public interface IUserContactRepository
{
    Task<UserContact?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<UserContact>> GetAllUserContactsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(UserContact userContact, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserContact userContact, CancellationToken cancellationToken = default);
    Task DeleteAsync(UserContact userContact, CancellationToken cancellationToken = default);
    Task<UserContact?> GetRecordByFilterAsync(Expression<Func<UserContact, bool>> filter, CancellationToken cancellationToken = default);
}