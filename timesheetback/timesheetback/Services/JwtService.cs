using System;
using System.IdentityModel.Tokens.Jwt;

namespace timesheetback.Services
{
	public class JwtService : IJwtService
	{
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public JwtService()
		{
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GetClaimFromJWT(string jwtToken, string claimName)
        {
            var jwtTokenObject = _tokenHandler.ReadJwtToken(jwtToken);

            var claim = jwtTokenObject.Claims.FirstOrDefault(claim => claim.Type == claimName)?.Value;

            if (!string.IsNullOrEmpty(claim))
            {
                return claim;
            }
            else
            {
                throw new Exception($"{claimName} claim not found in the token.");
            }
        }
    }
}

