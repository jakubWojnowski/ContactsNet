namespace ContactsNet.Core.Policies;

public interface ICannotOperateOnContact
{
    Task<bool> CheckIfUserCanOperateOnContact(Guid userId, Guid contactId, CancellationToken cancellationToken = default);
}