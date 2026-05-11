namespace spydle_api.Models
{
    public class GenerateTokenResponse
    {
        public string Token { get; set; }
        public long TokenExpireDateInMiliseconds { get; set; }
    }
}
