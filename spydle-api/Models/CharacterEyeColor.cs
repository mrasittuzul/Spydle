using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class CharacterEyeColor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}
