using ContactsNet.Core.Dal.Enums;

namespace ContactsNet.Core.Dto;

public class UserContactDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDateTime { get; set; }
    public Category ContactType { get; set; }
    public string? SubContactCategory { get; set; }

}