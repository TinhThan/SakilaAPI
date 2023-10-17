using System.ComponentModel.DataAnnotations.Schema;

namespace SakilaAPI.Core.Models.User
{
    public class UserModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> Permission { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
