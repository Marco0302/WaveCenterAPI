using System.ComponentModel.DataAnnotations;

namespace WaveCenter.ModelsAPI
{
    public class SimpleUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
