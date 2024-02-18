using Castle.Core.Logging;
using ContactsNet.Core.Authentication;
using ContactsNet.Core.CustomExceptions;
using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Mappers;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ContactsNet.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<RegisterUserDto> _registerValidator;
    private readonly IValidator<LoginUserDto> _loginValidator;
    private readonly IJwtProvider _jwtProvider;
    private static readonly UserMapper Mapper = new();

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IValidator<RegisterUserDto> registerValidator,
        IValidator<LoginUserDto> loginValidator, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _jwtProvider = jwtProvider;
    }
    
    public async Task RegisterUserAsync(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception(errorMessage);
        }
        var user = Mapper.MapRegistrationDtoToUser(dto);
        var encoded = _passwordHasher.HashPassword(user, dto.Password);
        user.EncodedPassword = encoded;
        await _userRepository.AddAsync(user, cancellationToken);
        
    }
    
    public async Task<UserDto> LoginUserAsync(LoginUserDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _loginValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new LoggerException(errorMessage);
        }
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Email == dto.Email, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException("There is no account for that email");
        }
        var userDto = Mapper.MapUserToUserDto(user);
        userDto.Token = _jwtProvider.GenerateJwtToken(user);
        return userDto;
        
        
        
    }

}