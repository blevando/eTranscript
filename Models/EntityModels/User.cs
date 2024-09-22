using Microsoft.AspNetCore.Identity;

namespace eTranscript.Models.EntityModels
{
    public class User : IdentityUser
    {
        public string? MatricNumber { get; set; }
    }
}
