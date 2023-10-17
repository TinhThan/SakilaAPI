namespace SakilaAPI.Core.Models.User
{
    public class RegisterModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> Permission { get; set; }
    }
}
