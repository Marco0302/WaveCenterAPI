namespace WaveCenter.Model
{
    public partial class EquipamentoMarcacao
    {
        public int Id { get; set; }
        public int IdMarcacao { get; set; }
        public Marcacao Marcacao { get; set; } 
        public int IdEquipamento { get; set; }
        public Equipamento Equipamento { get; set; } 
    }
}
