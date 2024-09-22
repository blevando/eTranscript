using System.ComponentModel.DataAnnotations;

namespace eTranscript.Models.DomainModels
{
    public class LoginRequestDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
