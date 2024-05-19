namespace WaveCenter.Model;

public partial class Media
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string NomeOrigem { get; set; } = string.Empty;
    public string Caminho { get; set; } = string.Empty;
    public string Tamanho { get; set; } = string.Empty;
}
