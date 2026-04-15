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
    }
}
