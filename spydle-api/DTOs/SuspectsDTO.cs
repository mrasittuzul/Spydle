using System.ComponentModel.DataAnnotations;

namespace spydle_api.DTOs
{
    public class SuspectsDTO
    {
        [Required]
        public int[] SuspectCodes { get; set; } = null!;
    }
}