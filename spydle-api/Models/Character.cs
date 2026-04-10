using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class Character
    {
        [Key]
        public int Code { get; set; }
        public bool IsActive { get; set; }
    }
}
