namespace ContactsNet.Core.Policies;

public interface ICannotAddContact
{
    Task<bool> CheckIfContactExists(Guid userId, string email, CancellationToken cancellationToken = default);
}