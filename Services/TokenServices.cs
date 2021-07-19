

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop_Correto.Models;

namespace Shop_Correto.Services
{
  public static class TokenService
  {
    public static string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();  //gera realmente o token
      var key = Encoding.ASCII.GetBytes(Settings.Secret);  //nossa chave (o segredo) que esta la em Settings Secret
      var tokenDescriptor = new SecurityTokenDescriptor   //descrição que vai ter dentro do token
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Username.ToString()),
          new Claim(ClaimTypes.Role, user.Role.ToString()),

        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}