using ContactsNet.Core.Dto;
using ContactsNet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsNet.Api.Controllers;
[AllowAnonymous]
public class AuthenticationController : BaseController
{
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login( LoginUserDto dto, CancellationToken cancellationToken = default)
    {
        var token = await _userService.LoginUserAsync(dto, cancellationToken);
        return Ok(token);
    }
    

    [HttpPost("register")]
    public async Task<ActionResult> Register( RegisterUserDto dto, CancellationToken cancellationToken = default)
    {
        await _userService.RegisterUserAsync(dto, cancellationToken);
        return Ok();
    }
    
}