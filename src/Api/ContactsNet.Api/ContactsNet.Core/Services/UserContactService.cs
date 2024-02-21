using ContactsNet.Core.CustomExceptions;
using ContactsNet.Core.Dal.Repositories;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Mappers;
using ContactsNet.Core.Policies;
using FluentValidation;

namespace ContactsNet.Core.Services;

public class UserContactService : IUserContactService
{
    private readonly IUserContactRepository _userContactRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICannotAddContact _cannotAddContact;
    private readonly ICannotOperateOnContact _cannotOperateOnContact;
    private readonly IValidator<UserContactDto> _userValidator;
    private static readonly UserContactMapper Mapper = new();

    public UserContactService(IUserContactRepository userContactRepository, IUserRepository userRepository,
        ICannotAddContact cannotAddContact, ICannotOperateOnContact cannotOperateOnContact,
        IValidator<UserContactDto> userValidator)
    {
        _userContactRepository = userContactRepository;
        _userRepository = userRepository;
        _cannotAddContact = cannotAddContact;
        _cannotOperateOnContact = cannotOperateOnContact;
        _userValidator = userValidator;
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

    public async Task<IEnumerable<UserContactDto>> GetAllUserContacts(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var userContacts = await _userContactRepository.GetAllUserContactsAsync(userId, cancellationToken);
        return Mapper.MapUserContactsToUserContactDtos(userContacts);
    }

    public async Task<UserContactDto> AddUserContact(Guid userId, UserContactDto userContactDto,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _userValidator.ValidateAsync(userContactDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new CustomValidationException(errorMessage);
        }

        var user = await _userRepository.GetAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException($"User with id {userId} not found");
        }

        if (await _cannotAddContact.CheckIfEmailIsNotUsers(userId, userContactDto.Email, cancellationToken))
        {
            throw new CannotAddContactException(userContactDto.Email);
        }

        var userContact = Mapper.MapUserContactDtoToUserContact(userContactDto);
        userContact.UserId = userId;
        await _userContactRepository.AddAsync(userContact, cancellationToken);
        return Mapper.MapUserContactToUserContactDto(userContact);
    }

    public async Task UpdateUserContact(Guid userId, UserContactDto userContactDto,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _userValidator.ValidateAsync(userContactDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new CustomValidationException(errorMessage);
        }
        var userContact = await _userContactRepository.GetAsync(userContactDto.Id, cancellationToken);
        if (userContact is null)
        {
            throw new ContactNotFoundException($"User contact with id {userContactDto.Id} not found");
        }
        
        if (!await _cannotOperateOnContact.CheckIfUserCanOperateOnContact(userId, userContactDto.Id, cancellationToken))
        {
            throw new CannotUpdateContactException(userContactDto.Id);
        }

        if (await _cannotAddContact.CheckIfEmailIsNotUsers(userId, userContactDto.Email, cancellationToken))
        {
            throw new CannotAddContactException(userContactDto.Email);
        }

        var updatedUserContact = Mapper.MapAndUpdateUserContactFromUserContactDto(userContactDto, userContact);
        await _userContactRepository.UpdateAsync(updatedUserContact, cancellationToken);
    }

    public async Task DeleteUserContact(Guid userId, Guid userContactId, CancellationToken cancellationToken = default)
    {
        var userContact = await _userContactRepository.GetAsync(userContactId, cancellationToken);
        if (userContact is null)
        {
            throw new ContactNotFoundException($"User contact with id {userContactId} not found");
        }

        if (!await _cannotOperateOnContact.CheckIfUserCanOperateOnContact(userId, userContactId, cancellationToken))
        {
            throw new CannotDeleteContactException(userContactId);
        }

        await _userContactRepository.DeleteAsync(userContact, cancellationToken);
    }
}