using System.ComponentModel.DataAnnotations;

namespace spydle_api.DTOs
{
    public class SuspectsDTO
    {
        [Required]
        [Length(5, 5)]
        public int[] SuspectCodes { get; set; } = null!;
    }
}