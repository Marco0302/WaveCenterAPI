using System.ComponentModel.DataAnnotations;
using WaveCenter.Model;

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
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public int NIF { get; set; }
        public string? Morada { get; set; }
        public int? IdTipoUser { get; set; }
    }
}
