using ContactsNet.Core.Dal.Entities;

namespace ContactsNet.Core.Authentication;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}