namespace WaveCenter.Model;

public partial class Equipamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Descricao { get; set; } = "";
    public string Estado { get; set; } = "";
    public int IdCategoriaEquipamento { get; set; }
    public CategoriaEquipamento? CategoriaEquipamento { get; set; }
    public int IdPedidoReparacao { get; set; }
}

