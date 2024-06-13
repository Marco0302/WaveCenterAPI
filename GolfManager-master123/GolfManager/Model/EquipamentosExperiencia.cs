namespace WaveCenter.Model
{
    public class EquipamentosExperiencia
    {
        public int Id { get; set; }
        public Experiencia? Experiencia { get; set; }
        public int IdExperiencia { get; set; }
        public CategoriaEquipamento? CategoriaEquipamento { get; set; }
        public int IdCategoriaEquipamento{ get; set; }
        public int QuantidadeNecessaria { get; set; }
    }
}
