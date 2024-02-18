namespace ContactsNet.Core.Dal.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime BirthDateTime { get; set; }
    public string? Email { get; set; }
    public string? EncodedPassword { get; set; }
    
    public virtual ICollection<UserContact> Contacts { get; set; }
    
}