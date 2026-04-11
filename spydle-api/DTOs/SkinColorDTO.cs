using System.ComponentModel.DataAnnotations;

namespace spydle_api.DTOs
{
    public class SkinColorDTO
    {
        public int Id { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}
