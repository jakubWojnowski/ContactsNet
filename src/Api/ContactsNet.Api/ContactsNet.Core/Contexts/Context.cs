using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ContactsNet.Core.Contexts;

public class Context : IContext
{
    public Guid Id { get; }
    public string? FullName { get; }
    public string? Token { get; }

    public Context(IHttpContextAccessor contextAccessor)
    {
        var guidParse = Guid.Parse(contextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
        Id = guidParse != Guid.Empty ? guidParse : Guid.Empty;
        FullName = contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        Token = contextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
    }
}