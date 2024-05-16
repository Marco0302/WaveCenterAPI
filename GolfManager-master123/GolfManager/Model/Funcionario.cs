namespace WaveCenter.Model;

public partial class Funcionario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Apelido { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Morada { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int Nif { get; set; }
    public bool Verificado { get; set; }
    public int IdTipoFuncionario { get; set; }
    public TipoFuncionario? TipoFuncionario { get; set; }
}


//public List<TipoFuncionario> customerAddresses { get; set; }
//public virtual Customer? IdclubNavigation { get; set; } = null!;