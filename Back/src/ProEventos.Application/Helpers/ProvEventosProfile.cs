using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Domain.Entidades;

namespace ProEventos.Application.Helpers
{
    public class ProvEventosProfile : Profile
    {
        public ProvEventosProfile()
        {
            CreateMap<Evento, EventoDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>().ReverseMap();
        }
    }
}
