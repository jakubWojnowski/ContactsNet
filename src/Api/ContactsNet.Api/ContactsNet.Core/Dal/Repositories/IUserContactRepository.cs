using ContactsNet.Core.Dal.Entities;

namespace ContactsNet.Core.Dal.Repositories;

internal interface IUserContactRepository
{
    Task<UserContact?> GetAsync(Guid id);
    Task<List<UserContact>> GetAllAsync();
    Task<Guid> AddAsync(UserContact userContact);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}