using ContactsNet.Core.Dto;
using ContactsNet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNet.Api.Controllers;
[AllowAnonymous]
public class AnonymousController : BaseController
{
    private readonly IUserService _userService;

    public AnonymousController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("GetAllUsers")]

    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        return Ok(users);
    }
    
    [HttpGet("GetUserById{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return OkOrNotFound(user);
    }

    
}