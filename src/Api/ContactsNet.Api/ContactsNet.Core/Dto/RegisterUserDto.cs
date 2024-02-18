namespace ContactsNet.Core.Dto;

public class RegisterUserDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDateTime { get; set; }
    
}