using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace HomeLoanAPI.AuthorizationLogic
{
    internal class GenerateToken
    {
        private readonly IConfiguration _config;
        private JwtSecurityToken _token;
        public GenerateToken(string emailId, IConfiguration config, string role) 
        {
            _config = config;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.Email, emailId),
               new Claim(ClaimTypes.Role,role),
            };

            _token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);
        }
        public string GetToken()
        {
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}
