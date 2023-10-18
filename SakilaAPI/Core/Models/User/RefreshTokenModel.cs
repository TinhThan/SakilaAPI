namespace SakilaAPI.Core.Models.User
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set;}
    }

    public class RefreshTokenModel : AccessTokenResponse
    {
    }
}
