using Microsoft.EntityFrameworkCore;
using Sakila_B.Core.Entities;

namespace Sakila_B.Core
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
        public virtual DbSet<FilmEntity> Films { get; set; }

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

            modelBuilder.Entity<FilmEntity>(entity =>
            {
                entity.ToTable("film");
                entity.HasKey(e => e.FilmId);
            });
        }
    }
}
