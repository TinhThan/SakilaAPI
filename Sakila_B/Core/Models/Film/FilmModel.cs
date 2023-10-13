using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sakila_B.Core.Models.Film
{
    public class FilmModel
    {
        public int FilmId { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public short? ReleaseYear { get; set; }

        public byte LanguageId { get; set; }

        public byte? OriginalLanguageId { get; set; }

        public byte RentalDuration { get; set; } = 3;

        public decimal RentalRate { get; set; } = 4.99m;

        public short? Length { get; set; }

        public decimal ReplacementCost { get; set; } = 19.99m;

        public string Rating { get; set; } = "G";

        public string SpecialFeatures { get; set; } = "Trailers,Commentaries,Deleted Scenes,Behind the Scenes";

        public DateTime LastUpdate { get; set; }
    }
}
