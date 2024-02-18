using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ContactsNet.Core.Mappers;
[Mapper]
public partial class UserMapper
{
    public partial User MapRegistrationDtoToUser(RegisterUserDto registerUserDto);
    public partial UserDto MapUserToUserDto(User user);
    
    public partial IReadOnlyCollection<UserDto> MapUsersToUserDtos(IEnumerable<User> users);
    
    
}