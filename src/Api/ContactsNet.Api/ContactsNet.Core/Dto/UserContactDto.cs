using ContactsNet.Core.Dal.Enums;

namespace ContactsNet.Core.Dto;

public class UserContactDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime BirthDateTime { get; set; }
    public Category ContactType { get; set; }
    public string? SubContactCategory { get; set; }

}