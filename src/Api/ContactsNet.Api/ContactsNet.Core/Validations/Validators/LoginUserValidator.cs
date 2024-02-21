using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ContactsNet.Core.Validations.Validators;

internal class LoginUserValidator : AbstractValidator<LoginUserDto>, IValidator
{
    public LoginUserValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, CancellationToken ct = default)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom( (email, context) =>
            {
               
                var user =  userRepository.GetRecordByFilterAsync(u => u.Email == email, ct);
                if (user.Result == null)
                {
                    context.AddFailure("Email", "There is no account for that email");
                }
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .Custom( (password, context) =>
            {
                var email = context.InstanceToValidate.Email;
                var user =  userRepository.GetRecordByFilterAsync(u => u.Email == email, ct);
                if (user.Result != null)
                {
           
                    var result = passwordHasher.VerifyHashedPassword(user.Result, user.Result.EncodedPassword, password);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        context.AddFailure("Password", "Invalid Password");
                    }
                }
            });
    }
}