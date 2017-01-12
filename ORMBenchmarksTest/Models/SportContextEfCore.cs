using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ORMBenchmarksTest.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SportContextEfCore : DbContext
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>()
                .HasMany(e => e.Teams);


            modelBuilder.Entity<Team>()
                .HasMany(e => e.Players);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
