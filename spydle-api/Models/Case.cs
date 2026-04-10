using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class Case
    {
        [Key]
        public DateTime Date { get; set; }
        public int RngSeed { get; set; }
        public int CharacterCode { get; set; }
    }
}
