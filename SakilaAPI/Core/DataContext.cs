using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Entities;

namespace SakilaAPI.Core
{
    /// <summary>
    /// Datacontext quản lý tương tác với database
    /// </summary>
    public partial class DataContext : DbContext
    {
        /// <summary>
        /// create Dbset entity actors
        /// </summary>
        public virtual DbSet<ActorEntity> Actors { get; set; }

        /// <summary>
        /// Config datacontext
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=sakila;User=root;Password=12345678;";
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
                entity.Property(t=>t.Id).HasColumnName("actor_id");
                entity.Property(t => t.FirstName).HasColumnName("first_name");
                entity.Property(t => t.LastName).HasColumnName("last_name");
                entity.Property(t => t.LastUpdate).HasColumnName("last_update");
            });
        }
    }
}
