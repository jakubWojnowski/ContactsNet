using ContactsNet.Core.Dal.Entities;
using ContactsNet.Core.Dto;
using Riok.Mapperly.Abstractions;

namespace ContactsNet.Core.Mappers;
[Mapper]
public partial class UserContactMapper
{
    
    public partial UserContact MapUserContactDtoToUserContact(UserContactDto userContactDto);
    public partial UserContactDto MapUserContactToUserContactDto(UserContact userContact);
    public partial IReadOnlyCollection<UserContactDto> MapUserContactsToUserContactDtos(IList<UserContact?> userContacts);

    public  UserContact MapAndUpdateUserContactFromUserContactDto(UserContactDto userContactDto,
        UserContact userContact)
    {
        userContact.Name = userContactDto.Name;
        userContact.Surname = userContactDto.Surname;
        userContact.BirthDateTime = userContactDto.BirthDateTime;
        userContact.PhoneNumber = userContactDto.PhoneNumber;
        userContact.Email = userContactDto.Email;
        return userContact;
        
    }
    
}