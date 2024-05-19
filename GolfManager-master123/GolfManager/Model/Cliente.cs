namespace WaveCenter.Model;

public partial class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Apelido { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; }
    public string Morada { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int NIF { get; set; }
    public Media? Galeria { get; set; }
    public int? IdAvatar { get; set; }
    public bool Ativo { get; set; }

}
