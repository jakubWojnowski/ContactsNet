namespace ContactsNet.Core.Dto;

public class UserDto
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDateTime { get; set; }
}

public class UserDetailsDto : UserDto
{
    public string? Token { get; set; }
}