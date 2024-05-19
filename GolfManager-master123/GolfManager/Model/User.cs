using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WaveCenter.Model
{
    public class User : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public int NIF { get; set; }
        public Galeria? Galeria { get; set; }
        public int? IdAvatar { get; set; }
        public string? Morada { get; set; }
        public bool Ativo { get; set; }
        public ICollection<ClientesMarcacao>? ClientesMarcacoes { get; set; }
    }
}
