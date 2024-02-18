using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ContactsNet.Core.Contexts;

public class Context : IContext
{
    public Guid Id { get; }
    public string FullName { get; }
    public string Token { get; }

    public Context(IHttpContextAccessor contextAccessor)
    {
        Id = Guid.Parse(contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
        FullName = contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? throw new UnauthorizedAccessException();
        Token = contextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "") ?? throw new UnauthorizedAccessException();
    }
}