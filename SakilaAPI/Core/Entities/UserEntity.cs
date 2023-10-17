using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SakilaAPI.Core.Entities
{
    public class UserEntity
    {
        [Required]
        [Column("id_user")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("permission")]
        public string Permission { get; set; }

        [Column("refreshToken")]
        public string? RefreshToken { get; set; }

        [Column("last_update")]
        public DateTime? LastUpdate { get; set; }

        [Column("refreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
