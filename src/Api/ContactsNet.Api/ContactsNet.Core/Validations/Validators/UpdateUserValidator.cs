using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using FluentValidation;

namespace ContactsNet.Core.Validations.Validators;

public class UpdateUserValidator : AbstractValidator<UserDto>
{
    public UpdateUserValidator(IUserRepository userRepository, CancellationToken ct = default)
    {
        RuleFor(x => x.BirthDateTime).NotEmpty().Custom((value, context) =>
        {
            if (value >= DateTime.Now) context.AddFailure("BirthDateTime", "Birth date cannot be in the future or now");
        });
        RuleFor(r => r.Email)
            .EmailAddress()
            .Custom((value, context) =>
            {
                var user = userRepository.GetRecordByFilterAsync(u => u.Email == value, ct);
                if (user.Result is not null) context.AddFailure("Email", "Email already exists");
            });
        RuleFor(x => x.PhoneNumber).Matches(@"^\d{9}$")
            .Custom((value, context) =>
            {
                var user = userRepository.GetRecordByFilterAsync(u => u.PhoneNumber == value, ct);
                if (user.Result is not null) context.AddFailure("PhoneNumber", "Phone number already exists");
            });
    }
}