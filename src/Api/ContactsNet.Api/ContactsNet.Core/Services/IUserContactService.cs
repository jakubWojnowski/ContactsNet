using ContactsNet.Core.Dto;

namespace ContactsNet.Core.Services;

public interface IUserContactService
{
    Task<UserContactDto> GetUserContactById(Guid id);
    Task<IEnumerable<UserContactDto>> GetAllUserContacts(Guid userId, CancellationToken cancellationToken = default);

    Task<UserContactDto> AddUserContact(Guid userId, UserContactDto userContactDto,
        CancellationToken cancellationToken = default);

    Task UpdateUserContact(Guid userId, UserContactDto userContactDto, CancellationToken cancellationToken = default);
    Task DeleteUserContact(Guid userId, Guid userContactId, CancellationToken cancellationToken= default);
}