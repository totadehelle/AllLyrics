using System;
using System.Linq;
using AllLyrics.Core;
using Microsoft.EntityFrameworkCore;

namespace AllLyrics.Data
{
    public sealed class ApplicationContext :DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
            if (Admins.Any()) return;
            var admin = new Admin()
            {
                Login = "admin",
                Password = "dd0d146b96855c6007bc5906908421e4", //"qwerty"
                LastChanged = DateTime.UtcNow,
                Role = Role.Administrator
            };
            Admins.Add(admin);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Artist>()
                .HasMany(e => e.Songs)
                .WithOne(e => e.Artist)
                .HasForeignKey(e => e.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Artist>().HasIndex(u => u.Name);
            modelBuilder.Entity<Song>().HasIndex(u => u.Name);
        }
    }
}