using ContactsNet.Core.Contexts;
using ContactsNet.Core.Dto;
using ContactsNet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNet.Api.Controllers;
[Authorize]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    private readonly IContext _context;

    public UserController(IUserService userService, IContext context)
    {
        _userService = userService;
        _context = context;
    }
    

    [HttpDelete("DeleteUser")]
    public async Task<ActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(_context.Id, cancellationToken);
        return NoContent();
    }
    
    [HttpPatch("UpdateUser")]
    public async Task<ActionResult> UpdateUser( UserDto dto, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(_context.Id, dto, cancellationToken);
        return NoContent();
    }
    
    
    
}