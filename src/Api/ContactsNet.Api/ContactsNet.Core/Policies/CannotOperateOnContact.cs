using ContactsNet.Core.Dal.Repositories;

namespace ContactsNet.Core.Policies;

public class CannotOperateOnContact : ICannotOperateOnContact
{
    private readonly IUserContactRepository _userContactRepository;

    public CannotOperateOnContact(IUserContactRepository userContactRepository)
    {
        _userContactRepository = userContactRepository;
    }
    
    public async Task<bool> CheckIfUserCanOperateOnContact(Guid userId, Guid contactId, CancellationToken cancellationToken = default)
    {
        var userContact = await _userContactRepository.GetAsync(contactId, cancellationToken);
        return userContact?.UserId == userId;
    }
}