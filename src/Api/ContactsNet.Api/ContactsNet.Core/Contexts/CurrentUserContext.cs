namespace ContactsNet.Core.Contexts;

public class CurrentUserContext
{
    public CurrentUserContext(string? fullname, string? id, string token)
    {
        FullName = fullname;
        Id = id;
        Token = token;
    }
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string Token { get; set; }
}