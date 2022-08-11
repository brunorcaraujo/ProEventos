using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interface;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralRepository _geralRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IMapper _mapper;

        public LoteService(IGeralRepository geralRepository, ILoteRepository loteRepository, IMapper mapper)
        {
            _geralRepository = geralRepository;
            _loteRepository = loteRepository;
            _mapper = mapper;
        }

        public async Task<LoteDTO[]> SaveLotes(int idEvento, LoteDTO[] models)
        {
            try
            {
                var lotes = await _loteRepository.GetLotesByEventoIdAsync(idEvento);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(idEvento, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(x => x.Id == model.Id);
                        model.EventoId = idEvento;
                        _mapper.Map(model, lote);

                        _geralRepository.Update<Lote>(lote);
                        await _geralRepository.SaveChangesAsync();
                    }
                }

                var loteRetorno = await _loteRepository.GetLotesByEventoIdAsync(idEvento);
                return _mapper.Map<LoteDTO[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddLote(int idEvento, LoteDTO model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = idEvento;
                _geralRepository.Add<Lote>(lote);

                await _geralRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
                

        public async Task<bool> DeleteLote(int idEvento, int idLote)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(idEvento, idLote);
                if (lote == null) throw new Exception("Lote para deletar não foi encontrado.");

                _geralRepository.Delete<Lote>(lote);
                return await _geralRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO> GetLoteByIdsAsync(int idEvento, int idLote)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(idEvento, idLote);
                if (lote == null) return null;

                var result = _mapper.Map<LoteDTO>(lote);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDTO[]> GetLotesByEventoIdAsync(int idEvento)
        {
            try
            {
                var lote = await _loteRepository.GetLotesByEventoIdAsync(idEvento);
                if (lote == null) return null;

                var result = _mapper.Map<LoteDTO[]>(lote);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
