using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContactsNet.Core.Dal.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ContactsNet.Core.Authentication;

internal class JwtProvider : IJwtProvider
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtProvider(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
            new(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_authenticationSettings.JwtExpiredDays));

        var toke = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(toke);
    }
}