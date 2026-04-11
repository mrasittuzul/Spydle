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
    }
}
