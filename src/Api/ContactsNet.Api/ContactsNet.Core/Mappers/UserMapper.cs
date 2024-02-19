using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ContactsNet.Core.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial User MapRegistrationDtoToUser(RegisterUserDto registerUserDto);
    public partial UserDto MapUserToUserDto(User user);
    public partial UserDetailsDto MapUserToUserDetailsDto(User user);

    public partial IReadOnlyCollection<UserDto> MapUsersToUserDtos(IList<User?> users);

    public User MapAndUpdateUserFromUserDto(UserDto userDto, User user)
    {
        user.Name = userDto.Name;
        user.Surname = userDto.Surname;
        user.BirthDateTime = userDto.BirthDateTime;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        return user;
        
    }

}