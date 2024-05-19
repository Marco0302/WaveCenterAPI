namespace WaveCenter.Model
{
    public partial class ClientesMarcacao
    {
        public int Id { get; set; }
        public int MarcacaoId { get; set; }
        public string UserId { get; set; }
        public string? Status { get; set; }
        public double Preco {  get; set; }
        public double Rating { get; set; }
        public int NumeroParticipantesUser { get; set; }
    }
}
