using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interface;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Interface;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _autoMapper;

        public EventoService(IGeralRepository geralRepository, IEventoRepository eventoRepository, IMapper autoMapper)
        {
            _geralRepository = geralRepository;
            _eventoRepository = eventoRepository;
            _autoMapper = autoMapper;
        }

        public async Task<EventoDTO> AddEvento(EventoDTO eventoDto)
        {
            try
            {
                var evento = _autoMapper.Map<Evento>(eventoDto);

                _geralRepository.Add<Evento>(evento);
                if (await _geralRepository.SaveChangesAsync())
                {
                    var retorno = await _eventoRepository.GetAllEventoByIdAsync(evento.Id, false);
                    return _autoMapper.Map<EventoDTO>(retorno);
                }                    

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<EventoDTO> UpdateEvento(int idEvento, EventoDTO model)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(idEvento, false);
                if (evento == null) return null;

                var resultado = _autoMapper.Map<EventoDTO>(evento);

                model.Id = evento.Id;

                _autoMapper.Map(model, evento);

                _geralRepository.Update<Evento>(evento);
                if (await _geralRepository.SaveChangesAsync())
                {
                    var retorno = await _eventoRepository.GetAllEventoByIdAsync(evento.Id, false);
                    return _autoMapper.Map<EventoDTO>(retorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int idEvento)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(idEvento, false);
                if (evento == null) throw new Exception("EventoDTO não encontrado!");

                _geralRepository.Delete<Evento>(evento);
                return await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                var resultado = _autoMapper.Map<EventoDTO[]>(eventos);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<EventoDTO> GetAllEventoByIdAsync(int idEvento, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(idEvento, includePalestrantes);
                if (evento == null) return null;

                var resultado = _autoMapper.Map<EventoDTO>(evento);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<EventoDTO[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                var resultado = _autoMapper.Map<EventoDTO[]>(eventos);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
