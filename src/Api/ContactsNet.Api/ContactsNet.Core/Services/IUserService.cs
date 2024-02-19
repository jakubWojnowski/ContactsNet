using ContactsNet.Core.Dto;

namespace ContactsNet.Core.Services;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<UserDetailsDto> LoginUserAsync(LoginUserDto dto, CancellationToken cancellationToken);
    Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task UpdateUserAsync(Guid id,UserDto dto, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}