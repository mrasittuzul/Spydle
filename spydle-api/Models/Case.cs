using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class Case
    {
        [Key]
        public DateTime Date { get; set; }
        public string RngSeed { get; set; } = null!;
    }
}
