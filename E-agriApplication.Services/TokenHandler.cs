using EcommerceApplication.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceApplication.Repositories
{
    public interface ITokenHandler
    {
        string CreateToken(Users users);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(Users users)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, users.Username));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: Credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
