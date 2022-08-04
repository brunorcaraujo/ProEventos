namespace ProEventos.Application.DTO
{
    public class PalestranteEventoDTO
    {
        public int PalestranteId { get; set; }
        public PalestranteDTO Palestrante { get; set; }
        public int EventoId { get; set; }
        public EventoDTO Evento { get; set; }
    }
}