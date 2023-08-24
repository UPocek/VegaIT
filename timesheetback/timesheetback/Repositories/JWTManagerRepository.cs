using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using timesheetback.DTOs;
using timesheetback.Models;
using timesheetback.Services;

namespace timesheetback.Repositories
{
	public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IUserService _userService;

        public JWTManagerRepository(IConfiguration iconfiguration, IUserService userService)
        {
            _iconfiguration = iconfiguration;
            _userService = userService;
        }

        public UserCredentialsDTO Authenticate(LoginCredentialsDTO loginCredentials)
        {
            Employee? userToLogin = _userService.ProccessUserLogin(loginCredentials);

            if (userToLogin is null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Email, userToLogin.Email),
             new Claim(ClaimTypes.GivenName, userToLogin.Name),
             new Claim(ClaimTypes.Name, userToLogin.Username),
             new Claim(ClaimTypes.Role, userToLogin.Role.Name),
              }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserCredentialsDTO { Token = tokenHandler.WriteToken(token) };

        }
    }
}

