using Domain;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PresentationWeb.Models
{
    public class apiContext : DbContext
    {
        public Config _config;
        public apiContext(DbContextOptions<apiContext> options, Config config) : base(options)
        {
            _config = config;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.connectionStrings.Database);
        }
    }
}
