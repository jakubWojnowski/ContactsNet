using ContactsNet.Core.Dto;

namespace ContactsNet.Core.Services;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<UserDto> LoginUserAsync(LoginUserDto dto, CancellationToken cancellationToken);
}