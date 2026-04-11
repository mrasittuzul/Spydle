using Microsoft.EntityFrameworkCore;
using spydle_api.Models;
using System.Reflection.Metadata;

namespace spydle_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Case> Cases { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterEyeColor> CharacterEyeColors { get; set; }
        public DbSet<CharacterSkinColor> CharacterSkinColors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CharacterEyeColor>(b =>
            {
                b.HasData(
                    new CharacterEyeColor { Id = 0, Name = "Brown", Red = 97, Green = 58, Blue = 30 },
                    new CharacterEyeColor { Id = 1, Name = "Blue", Red = 46, Green = 87, Blue = 209 },
                    new CharacterEyeColor { Id = 2, Name = "Green", Red = 125, Green = 212, Blue = 59 },
                    new CharacterEyeColor { Id = 3, Name = "Grey", Red = 155, Green = 166, Blue = 199 }
                );
            });

            modelBuilder.Entity<CharacterSkinColor>(b =>
            {
                b.HasData(
                    new CharacterSkinColor { Id = 0, Name = "Cyan", Red = 41, Green = 214, Blue = 214 },
                    new CharacterSkinColor { Id = 1, Name = "Yellow", Red = 255, Green = 217, Blue = 15 },
                    new CharacterSkinColor { Id = 2, Name = "Red", Red = 214, Green = 41, Blue = 73 },
                    new CharacterSkinColor { Id = 3, Name = "Green", Red = 52, Green = 214, Blue = 41 }
                );
            });
        }
    }
}
