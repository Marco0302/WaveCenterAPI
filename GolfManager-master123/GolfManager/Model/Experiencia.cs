namespace WaveCenter.Model;

public partial class Experiencia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    //public Local Local { get; set; }
    //public int IdLocal { get; set; }
    public string Imagem { get; set; } = string.Empty;
    public int NumeroMinimoPessoas { get; set; }
    public int NumeroMaximoPessoas { get; set; }
    public int IdTipoExperiencia { get; set; }
    public TipoExperiencia? TipoExperiencia { get; set; }
    public int IdCategoriaExperiencia { get; set; }
    public CategoriaExperiencia? CategoriaExperiencia { get; set; }

    //public ExperienciaData ExperienciaData { get; set; }
    //public int IdExperienciaData { get; set; }
    public bool Ativo { get; set; }
}
