using spydle_api.Entities;
using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class Case
    {
        [Key]
        public DateTime Date { get; set; }
        public int RngSeed { get; set; }
        public int SpyCharacterCode { get; set; }
        public int[] IncludedCharacters { get; set; } = null!;

        public ICollection<Interrogation> Interrogations { get; set; }
    }
}
