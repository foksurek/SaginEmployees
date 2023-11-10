using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SaginEmployees.Services;

public class JwtTokenHandlerService
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenHandlerService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateJwtToken(ClaimsIdentity claims, DateTime expires)
    {
        var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = expires,
            SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public bool CheckJwtToken(string token)
    {
        var mySecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = mySecurityKey
            }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }
        return true;
    }

}