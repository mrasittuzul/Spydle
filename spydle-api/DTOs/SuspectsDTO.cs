using System.ComponentModel.DataAnnotations;

namespace spydle_api.DTOs
{
    public class SuspectsDTO
    {
        [Required]
        [Length(1, 2)]
        public int[] SuspectCodes { get; set; } = null!;
    }
}