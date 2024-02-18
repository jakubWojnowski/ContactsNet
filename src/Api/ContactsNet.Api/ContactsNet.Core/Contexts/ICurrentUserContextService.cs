namespace ContactsNet.Core.Contexts;

public interface ICurrentUserContextService
{
    CurrentUserContext? GetCurrentUser();
}