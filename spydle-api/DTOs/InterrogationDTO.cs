using spydle_api.Models;
using System.ComponentModel.DataAnnotations;

namespace spydle_api.DTOs
{
    public class InterrogationDTO
    {
        public long DateTimeInMiliseconds;
        public int[] Suspects { get; set; } = null!;
        public bool FoundMatchingTrait { get; set; }
    }
}
