namespace WaveCenter.Model;

public partial class PedidoReparacao
{
    public int Id { get; set; }
    public int IdEquipamento { get; set; }
    public Equipamento? Equipamento { get; set; }
    public int IdFuncionario { get; set; }
    public Funcionario? Funcionario { get; set; }
    public string Notas { get; set; } = string.Empty;
    public DateTime DataPedido { get; set; }
    public DateTime? DataConclusao { get; set; }
    public bool Estado { get; set; }
}
