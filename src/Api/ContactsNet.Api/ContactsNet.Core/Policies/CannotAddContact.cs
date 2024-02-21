using ContactsNet.Core.Dal.Repositories;

namespace ContactsNet.Core.Policies;

public class CannotAddContact : ICannotAddContact
{
    private readonly IUserRepository _userRepository;

    public CannotAddContact(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CheckIfEmailIsNotUsers(Guid userId, string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(userId, cancellationToken);

        return user?.Email == email;
    }
}