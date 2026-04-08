using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class Case
    {
        [Key]
        public DateTime Date;
        public string RngSeed = null!;
    }
}
