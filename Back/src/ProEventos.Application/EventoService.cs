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

        public EventoService(IGeralRepository geralRepository, IEventoRepository eventoRepository)
        {
            _geralRepository = geralRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralRepository.Add<Evento>(model);
                if (await _geralRepository.SaveChangesAsync())
                    return await _eventoRepository.GetAllEventoByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int idEvento, Evento model)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(idEvento, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralRepository.Update<Evento>(model);
                if (await _geralRepository.SaveChangesAsync())
                    return await _eventoRepository.GetAllEventoByIdAsync(model.Id, false);

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
                if (evento == null) throw new Exception("Evento não encontrado!");

                _geralRepository.Delete<Evento>(evento);
                return await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Evento> GetAllEventoByIdAsync(int idEvento, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventoByIdAsync(idEvento, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
