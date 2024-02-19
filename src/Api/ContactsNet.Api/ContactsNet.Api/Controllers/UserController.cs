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


    public UserController(IUserService userService, IContext context)
    {
        _userService = userService;
        _context = context;
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
}