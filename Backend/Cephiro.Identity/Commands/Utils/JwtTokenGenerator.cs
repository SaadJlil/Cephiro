using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cephiro.Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cephiro.Identity.Commands.Utils;

public class JwtTokenGenerator
{
    private readonly JwtConfig _jwtSettings;
    private readonly DateTimeProvider _dateTimeProvider;
    public JwtTokenGenerator(IOptionsMonitor<JwtConfig> jwtOptions, DateTimeProvider dateTimeProvider)
    {
        _jwtSettings = jwtOptions.CurrentValue;
        _dateTimeProvider = dateTimeProvider;
    }
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(_jwtSettings.Secret)!)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!.ToString()),
            new Claim("HasCreditCard", user.HasCreditCard.ToString()),
            new Claim("EmailConfirmed", user.EmailConfirmed.ToString()),
            new Claim("PhoneNumberConfirmed", user.PhoneNumberConfirmed.ToString()),
        };


        var securityToken = new JwtSecurityToken
        (
            issuer: _jwtSettings.Issuer,
            expires: _dateTimeProvider.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}