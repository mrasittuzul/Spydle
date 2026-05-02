using spydle_api.Models;
using System.ComponentModel.DataAnnotations;

namespace spydle_api.Entities
{
    public class Interrogation
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CaseDate { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public int[] Suspects { get; set; } = null!;
        public bool FoundMatchingTrait { get; set; }

        public Case Case { get; set; }
        public User User { get; set; }
    }
}
