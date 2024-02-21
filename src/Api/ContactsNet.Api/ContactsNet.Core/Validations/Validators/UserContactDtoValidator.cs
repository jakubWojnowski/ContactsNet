using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using FluentValidation;

namespace ContactsNet.Core.Validations.Validators;

public class UserContactDtoValidator : AbstractValidator<UserContactDto>
{

    public UserContactDtoValidator(IUserContactRepository contactRepository, CancellationToken ct = default)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress() .Custom((value, context) =>
        {
            var user =  contactRepository.GetRecordByFilterAsync(u => u.Email == value, ct);
            if (user.Result is not null) context.AddFailure("Email", "Contact already exists");
        });;
        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^\d{9}$");
        RuleFor(x => x.BirthDateTime).NotEmpty().Custom((value, context) =>
        {
            if (value >= DateTime.Now) context.AddFailure("BirthDateTime", "Birth date cannot be in the future or now");
        });
        RuleFor(x => x.ContactType).IsInEnum();
    }
    
}