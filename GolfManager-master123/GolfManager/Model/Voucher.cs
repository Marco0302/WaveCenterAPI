namespace WaveCenter.Model;

public partial class Voucher
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime? DataCriacao { get; set; }
    public float Valor { get; set; }
    public float ValorPercentagem { get; set; }
    public float LimiteGlobal { get; set; }
    public bool Ativo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string MarcacaoTotalMin { get; set; } = string.Empty;
    public int LimiteUsoCliente { get; set; }
    public int Funcionario { get; set; }
}

