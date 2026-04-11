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

            List<Character> characters = new List<Character>(64);
            int characterCode = 0;
            for (int eyeColor = 0; eyeColor < 4; eyeColor++)
            {
                for (int skinColor = 0; skinColor < 4; skinColor++)
                {
                    for (int age = 0; age < 2; age++)
                    {
                        for (int sex = 0; sex < 2; sex++)
                        {
                            characterCode = 0;
                            characterCode |= eyeColor << 4;
                            characterCode |= skinColor << 2;
                            characterCode |= age << 1;
                            characterCode |= sex;
                            characters.Add(new Character { Code = characterCode, IsActive = true });
                        }
                    }
                }
            }
            modelBuilder.Entity<Character>(b => b.HasData(characters));

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
