namespace WaveCenter.Model;

public partial class PedidoReparacaoEstados
{
    public int Id { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int IdPedidoReparacao { get; set; }
    public PedidoReparacao? PedidoReparacao { get; set; }
    public DateTime Data { get; set; }
}
