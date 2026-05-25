using spydle_api.Models;

namespace spydle_api.Interfaces
{
    public interface ITokenService
    {
        public Task<GenerateTokenResponse> GenerateToken(User user);
        public Task<User> GetUserFromHTTPContext(HttpContext httpContext);
    }
}
