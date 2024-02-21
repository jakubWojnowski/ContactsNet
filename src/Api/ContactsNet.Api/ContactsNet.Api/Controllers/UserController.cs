using ContactsNet.Core.Contexts;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNet.Api.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IContext _context;
    private readonly IUserContactService _userContactService;


    public UserController(IUserService userService, IContext context, IUserContactService userContactService)
    {
        _userService = userService;
        _context = context;
        _userContactService = userContactService;
    }

    [AllowAnonymous]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpGet("GetUserById{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return OkOrNotFound(user);
    }

    [Authorize]
    [HttpDelete("DeleteLoggedInUser")]
    public async Task<ActionResult> DeleteLoggedInUser(CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(_context.Id, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("UpdateLoggedInUser")]
    public async Task<ActionResult> UpdateLoggedInUser(UserDto dto, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(_context.Id, dto, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPost("AddUserContact")]
    public async Task<ActionResult> AddUserContact(UserContactDto userContactDto, CancellationToken cancellationToken)
    {
        await _userContactService.AddUserContact(_context.Id, userContactDto, cancellationToken);
        return Ok(userContactDto);
    }

    [Authorize]
    [HttpDelete("DeleteUserContact")]
    public async Task<ActionResult> DeleteUserContact(Guid id, CancellationToken cancellationToken)
    {
        await _userContactService.DeleteUserContact(_context.Id, id, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("UpdateUserContact")]
    public async Task<ActionResult> UpdateUserContact(UserContactDto userContactDto,
        CancellationToken cancellationToken)
    {
        await _userContactService.UpdateUserContact(_context.Id, userContactDto, cancellationToken);
        return NoContent();
    }

    [Authorize]
    [HttpGet("GetAllUserContacts")]
    public async Task<ActionResult<IEnumerable<UserContactDto>>> GetAllUserContacts(CancellationToken cancellationToken)
    {
        var userContacts = await _userContactService.GetAllUserContacts(_context.Id, cancellationToken);
        return Ok(userContacts);
    }

    [Authorize]
    [HttpGet("GetUserContactById{id}")]
    public async Task<ActionResult<UserContactDto>> GetUserContactById(Guid id)
    {
        var userContact = await _userContactService.GetUserContactById(id);
        return OkOrNotFound(userContact);
    }
}