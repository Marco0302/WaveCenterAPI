using WaveCenter.Model;

namespace WaveCenter.ModelsAPI
{
    public class ReturnedExperiencia
    {
        public Experiencia? Experiencia { get; set; } = new Experiencia() { Marcacoes = new List<Marcacao>() };
        public double AverageRating { get; set; }
        public int RatingCount { get; set; }
        public int TotalParticipants { get; set; }
    }
}
