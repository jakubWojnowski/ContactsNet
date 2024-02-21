using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Validations.Helpers;
using FluentValidation;

namespace ContactsNet.Core.Validations.Validators;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserDto>, IValidator
{
    public RegisterUserValidator(IUserRepository userRepository, CancellationToken ct = default)
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) =>
            {
                var user =  userRepository.GetRecordByFilterAsync(u => u.Email == value, ct);
                if (user.Result is not null) context.AddFailure("Email", "Email already exists");
            });
        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Must(PasswordHelper.HasNumber).WithMessage("Password must contain at least one number")
            .Must(PasswordHelper.HasCapitals).WithMessage("Password must contain at least one capital letter")
            .Must(PasswordHelper.HasLowercase).WithMessage("Password must contain at least one lowercase letter")
            .Must(PasswordHelper.HasSpecialCharacters).WithMessage("Password must contain at least one special character");
        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .Equal(r => r.Password)
            .WithMessage("Passwords do not match");
        RuleFor(r => r.Name)
            .NotEmpty();
        RuleFor(r => r.Surname).NotEmpty();
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{9}$")
            .WithMessage("Phone number must contain 9 digits");
        RuleFor(r => r.BirthDateTime).NotEmpty();
    }
    
}