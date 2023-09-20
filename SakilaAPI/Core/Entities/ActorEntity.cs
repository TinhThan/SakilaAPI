using System.ComponentModel.DataAnnotations.Schema;

namespace SakilaAPI.Core.Entities
{
    public class ActorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
