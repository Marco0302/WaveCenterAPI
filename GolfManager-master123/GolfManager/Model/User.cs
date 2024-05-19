using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WaveCenter.Model
{
    public partial class User : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public int NIF { get; set; }
        public Media? Media { get; set; }
        public int? IdMedia { get; set; }
        public string Morada { get; set; } = string.Empty;
        public int IdTipoUser { get; set; }
        public TipoUser? TipoUser { get; set; }
        public bool Ativo { get; set; }
        public ICollection<ClientesMarcacao>? ClientesMarcacoes { get; set; }

    }
}
