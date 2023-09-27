using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakilaAPI.Core.Entities
{
    /// <summary>
    /// Entity film
    /// </summary>
    public class FilmEntity
    {
        /// <summary>
        /// Id film
        /// </summary>
        [Column("film_id")]
        public int FilmId { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        [Required]
        [Column("title")]
        public string Title { get; set; } = String.Empty;

        /// <summary>
        /// Mô tả
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Năm phát hành
        /// </summary>
        [Column("release_year")]
        public short? ReleaseYear { get; set; }

        /// <summary>
        /// Ngôn ngữ
        /// </summary>
        [Required]
        [Column("language_id")]
        public byte LanguageId { get; set; }

        /// <summary>
        /// Ngôn ngữ gốc
        /// </summary>
        [Column("original_language_id")]
        public byte? OriginalLanguageId { get; set; }

        /// <summary>
        /// Thời lượng thuê
        /// </summary>
        [Required]
        [Column("rental_duration")]
        public byte RentalDuration { get; set; } = 3;

        /// <summary>
        /// Giá thuê (theo đô la)
        /// </summary>
        [Required]
        [Column("rental_rate", TypeName = "decimal(4, 2)")]
        public decimal RentalRate { get; set; } = 4.99m;

        /// <summary>
        /// length
        /// </summary>
        [Column("length")]
        public short? Length { get; set; }

        /// <summary>
        /// Giá thay thế
        /// </summary>
        [Required]
        [Column("replacement_cost", TypeName = "decimal(5, 2)")]
        public decimal ReplacementCost { get; set; } = 19.99m;

        /// <summary>
        /// Đánh giá
        /// </summary>
        [Column("rating")]
        [MaxLength(5)]
        public string Rating { get; set; } = "G";

        /// <summary>
        /// Tính năng đặc biệt
        /// </summary>
        [Column("special_features")]
        public string SpecialFeatures { get; set; } = "Trailers,Commentaries,Deleted Scenes,Behind the Scenes";

        /// <summary>
        /// Lần cuối cập nhật
        /// </summary>
        [Column("last_update")]
        [Required]
        public DateTime LastUpdate { get; set; }
    }
}
