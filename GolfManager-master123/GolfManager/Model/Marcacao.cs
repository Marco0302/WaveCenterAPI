namespace WaveCenter.Model
{
    public partial class Marcacao
    {
        public int Id { get; set; }
        public int IdExperiencia { get; set; }
        public DateTime Data { get; set; }
        public Experiencia? Experiencia { get; set; }
        // Em horas e.g 1.5 -> hora e meia 
        public double HoraInicio { get; set; }
        public double HoraFim { get; set; }
        public int NumeroParticipantesTotal {  get; set; }
        public bool ExperienciaPartilhada {  get; set; }
        public ICollection<ClientesMarcacao>? ClientesMarcacoes { get; set; }
    }
}
