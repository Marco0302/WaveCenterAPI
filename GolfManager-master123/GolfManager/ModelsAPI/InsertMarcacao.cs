using WaveCenter.Model;

namespace WaveCenter.ModelsAPI
{
    public class InsertMarcacao
    {
        public int Id { get; set; }
        public int IdExperiencia { get; set; }
        public DateTime Data { get; set; }
        // Em horas e.g 1.5 -> hora e meia 
        public double HoraInicio { get; set; }
        public double HoraFim { get; set; }
        public int NumeroParticipantes { get; set; }
        public double Preco { get; set; }
        public double Rating { get; set; }
        public bool ExperienciaPartilhada { get; set; }
    }
}
