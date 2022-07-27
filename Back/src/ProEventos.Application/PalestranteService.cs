using ProEventos.Application.Interface;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly IPalestranteRepository _palestranteRepository;

        public PalestranteService(IGeralRepository geralRepository, IPalestranteRepository palestranteRepository)
        {
            _geralRepository = geralRepository;
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
            try
            {
                _geralRepository.Add<Palestrante>(model);
                if (await _geralRepository.SaveChangesAsync())
                    return await _palestranteRepository.GetAllPalestranteByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Palestrante> UpdatePalestrante(int idEvento, Palestrante model)
        {
            try
            {
                var evento = await _palestranteRepository.GetAllPalestranteByIdAsync(idEvento, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralRepository.Update<Palestrante>(model);
                if (await _geralRepository.SaveChangesAsync())
                    return await _palestranteRepository.GetAllPalestranteByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<bool> DeletePalestrante(int idEvento)
        {
            try
            {
                var evento = await _palestranteRepository.GetAllPalestranteByIdAsync(idEvento, false);
                if (evento == null) throw new Exception("Evento não encontrado!");

                _geralRepository.Delete<Palestrante>(evento);
                return await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _palestranteRepository.GetAllPalestranteByIdAsync(id, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _palestranteRepository.GetAllPalestrantesAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _palestranteRepository.GetAllPalestrantesByNomeAsync(tema, includePalestrantes);
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
