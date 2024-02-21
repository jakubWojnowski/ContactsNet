namespace ContactsNet.Core.Policies;

public interface ICannotAddContact
{
    Task<bool> CheckIfEmailIsNotUsers(Guid userId, string email, CancellationToken cancellationToken = default);
}