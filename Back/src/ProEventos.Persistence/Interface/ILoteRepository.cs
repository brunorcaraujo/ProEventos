using ProEventos.Domain.Entidades;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface
{
    public interface ILoteRepository
    {
        Task<Lote[]> GetLotesByEventoIdAsync(int idEvento);
        Task<Lote> GetLoteByIdsAsync(int idEvento, int idLote);
    }
}
