using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Entities;

namespace SakilaAPI.Core
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<ActorEntity> Actors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=sakila;User=root;Password=12345678;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorEntity>(entity =>
            {
                entity.ToTable("actor");
                entity.HasKey(e => e.Id);
                entity.Property(t=>t.Id).HasColumnName("actor_id");
                entity.Property(t => t.FirstName).HasColumnName("first_name");
                entity.Property(t => t.LastName).HasColumnName("last_name");
                entity.Property(t => t.LastUpdate).HasColumnName("last_update");
            });
        }
    }
}
