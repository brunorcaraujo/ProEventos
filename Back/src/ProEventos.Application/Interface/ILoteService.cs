using ProEventos.Application.DTO;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface
{
    public interface ILoteService
    {
        Task<LoteDTO[]> SaveLotes(int idEvento, LoteDTO[] models);
        Task<bool> DeleteLote(int idEvento, int idLote);
        Task<LoteDTO[]> GetLotesByEventoIdAsync(int idEvento);
        Task<LoteDTO> GetLoteByIdsAsync(int idEvento, int idLote);
    }
}
