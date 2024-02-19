using ContactsNet.Core.CustomExceptions;
using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Mappers;

namespace ContactsNet.Core.Services;

public class UserContactService
{
    private readonly IUserContactRepository _userContactRepository;
    private static readonly UserContactMapper Mapper = new();

    public UserContactService(IUserContactRepository userContactRepository)
    {
        _userContactRepository = userContactRepository;
    }
    
    public async Task<UserContactDto> GetUserContactById(Guid id)
    {
        var userContact = await _userContactRepository.GetAsync(id);
        if (userContact is null)
        {
            throw new ContactNotFoundException($"User contact with id {id} not found");
        }
        return Mapper.MapUserContactToUserContactDto(userContact);
   
    }
    //
    // public async Task<IEnumerable<UserContactDto>> GetAllUserContacts(Guid userId)
    // {
    //     var userContacts = await _userContactRepository.GetRecordByFilterAsync(u => u.UserId == userId);
    //     
    //    
    // }

    
    
}