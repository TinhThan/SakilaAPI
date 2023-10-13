using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila_B.Core.Entities
{
    public class ActorFilmEntity
    {
        [Column("actor_id")]
        public int ActorId { get; set; }

        [Column("film_id")]
        public int FilmId { get; set; }
        
        [Column("last_update")]
        [Required]
        public DateTime LastUpdate { get; set; }

        public virtual ActorEntity Actor { get; set; }
        public virtual FilmEntity Film { get; set; }
    }
}
