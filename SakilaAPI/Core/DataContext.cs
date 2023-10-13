using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Entities;

namespace SakilaAPI.Core
{
    /// <summary>
    /// Datacontext quản lý tương tác với database
    /// </summary>
    public partial class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual DbSet<ActorEntity> Actors { get; set; }
        public virtual DbSet<FilmEntity> Films { get; set; }
        public virtual DbSet<ActorFilmEntity> ActorFilms { get; set; }

        /// <summary>
        /// Config datacontext
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Sakila");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        /// <summary>
        /// Cấu hình cấu trúc database, ánh xạ entity với các table, thiết lập quy tắc và liên kết các table
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorEntity>(entity =>
            {
                entity.ToTable("actor");
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).HasColumnName("actor_id");
                entity.Property(t => t.FirstName).HasColumnName("first_name");
                entity.Property(t => t.LastName).HasColumnName("last_name");
                entity.Property(t => t.LastUpdate).HasColumnName("last_update");
            });

            modelBuilder.Entity<FilmEntity>(entity =>
            {
                entity.ToTable("film");
                entity.HasKey(e => e.FilmId);
            });

            modelBuilder.Entity<ActorFilmEntity>(entity =>
            {
                entity.ToTable("film_actor");
                entity.HasKey(e => new { e.ActorId,e.FilmId});
                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.FilmActors)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_film_actor_actor");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.ActorFilms)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_film_actor_film");
            });
        }
    }
}
