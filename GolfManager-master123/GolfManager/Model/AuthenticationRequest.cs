using System.ComponentModel.DataAnnotations;

namespace WaveCenter.Model
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
