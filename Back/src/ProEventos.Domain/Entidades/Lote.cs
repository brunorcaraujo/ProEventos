using System;

namespace ProEventos.Domain.Entidades
{
    public class Lote
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }

        //[ForeignKey("Eventos")]
        public int EventoID { get; set; }
        public Evento Evento { get; set; }
    }
}