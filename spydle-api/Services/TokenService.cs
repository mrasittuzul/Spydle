using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using spydle_api.Data;
using spydle_api.Helpers;
using spydle_api.Interfaces;
using spydle_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace spydle_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext _context;

        public TokenService(IConfiguration configuration, ApplicationDbContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        public Task<GenerateTokenResponse> GenerateToken(User user)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]));

            var dateTimeNow = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: new List<Claim> 
                    {
                        new Claim("userId", user.Id)
                    },
                    notBefore: dateTimeNow,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(configuration.GetValue<long>("Jwt:ExpirationInMinutes"))),
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return Task.FromResult(new GenerateTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                TokenExpireDateInMiliseconds = TimeHelper.GetTimeSinceUnixEpochInMilisecondsFromDateTime(jwt.ValidTo)
            });
        }

        public async Task<User> GetUserFromHTTPContext(HttpContext httpContext)
        {
            string userId = httpContext.User.Claims.First(c => c.Type == "userId").Value;
            var user = await _context.Users.Include(u => u.Interrogations).FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
