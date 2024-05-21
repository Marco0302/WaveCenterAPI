namespace WaveCenter.Model;

public partial class Equipamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int IdCategoriaEquipamento { get; set; }
    public CategoriaEquipamento? CategoriaEquipamento { get; set; }
}

