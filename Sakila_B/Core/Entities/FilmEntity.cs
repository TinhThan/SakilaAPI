using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila_B.Core.Entities
{
    /// <summary>
    /// Entity film
    /// </summary>
    public class FilmEntity
    {

        public FilmEntity()
        {
            ActorFilms = new HashSet<ActorFilmEntity>();
        }

        [Column("film_id")]
        public int FilmId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; } = String.Empty;

        [Column("description")]
        public string Description { get; set; } = String.Empty;

        [Column("release_year")]
        public short? ReleaseYear { get; set; }

        [Required]
        [Column("language_id")]
        public byte LanguageId { get; set; }

        [Column("original_language_id")]
        public byte? OriginalLanguageId { get; set; }

        [Required]
        [Column("rental_duration")]
        public byte RentalDuration { get; set; } = 3;

        [Required]
        [Column("rental_rate", TypeName = "decimal(4, 2)")]
        public decimal RentalRate { get; set; } = 4.99m;

        [Column("length")]
        public short? Length { get; set; }

        [Required]
        [Column("replacement_cost", TypeName = "decimal(5, 2)")]
        public decimal ReplacementCost { get; set; } = 19.99m;

        [Column("rating")]
        [MaxLength(5)]
        public string Rating { get; set; } = "G";

        [Column("special_features")]
        public string SpecialFeatures { get; set; } = "Trailers,Commentaries,Deleted Scenes,Behind the Scenes";

        [Column("last_update")]
        [Required]
        public DateTime LastUpdate { get; set; }

        public virtual ICollection<ActorFilmEntity> ActorFilms { get; set; }
    }
}
