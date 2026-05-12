using Microsoft.AspNetCore.Identity;
using spydle_api.Entities;

namespace spydle_api.Models
{
    public class User : IdentityUser
    {
        public ICollection<Interrogation> Interrogations { get; set; } = null!;
    }
}
