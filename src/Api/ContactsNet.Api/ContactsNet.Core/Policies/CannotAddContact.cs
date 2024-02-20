using ContactsNet.Core.Dal.Repositories;

namespace ContactsNet.Core.Policies;

public class CannotAddContact : ICannotAddContact
{
    private readonly IUserContactRepository _userContactRepository;

    public CannotAddContact(IUserContactRepository userContactRepository)
    {
        _userContactRepository = userContactRepository;
    }
  
    public async Task<bool> CheckIfContactExists(Guid userId, string email, CancellationToken cancellationToken = default)
    {
        var userContacts = await _userContactRepository.GetRecordByFilterAsync(u => u.UserId == userId && u.Email == email, cancellationToken);
        return userContacts is not null;
    }
}