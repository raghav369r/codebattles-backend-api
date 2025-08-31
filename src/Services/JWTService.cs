using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeBattles_Backend.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CodeBattles_Backend.Services;

public class JWTService
{
  private readonly string _jwtKey;
  private readonly string _jwtIssuer;
  private readonly string _jwtAudience;
  private readonly int _expirationTime;

  public JWTService(IConfiguration configuration)
  {
    _jwtKey = configuration["JwtConfig:Key"] ?? "some-hard-to-crack-key-for-signining-tokens-actually-this-is-that-key";
    _jwtIssuer = configuration["JwtConfig:Issuer"] ?? "";
    _jwtAudience = configuration["JwtConfig:Audience"] ?? "";
    _expirationTime = Convert.ToInt32(configuration["JwtConfig:expires"]);
  }
  public string GenerateJwtToken(User user)
  {
    var claims = new[]
    {
            new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(user.Id)),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role,user.IsCreater?"Admin":"User")
        };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _jwtIssuer,
        audience: _jwtAudience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(_expirationTime),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
