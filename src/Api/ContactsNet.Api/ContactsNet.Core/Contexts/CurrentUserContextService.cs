using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ContactsNet.Core.Contexts
{
    public class CurrentUserContextService : ICurrentUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public CurrentUserContext? GetCurrentUser()
        {
            var user = _contextAccessor.HttpContext?.User;
            var token = _contextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (user is null || !user.Claims.Any())
            {
                return null;
            }

            var fullName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (fullName is null || id is null)
            {
                throw new UnauthorizedAccessException();
            }

            return new CurrentUserContext(fullName, id, token!);
        }
    }
}