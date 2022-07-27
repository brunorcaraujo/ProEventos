using ProEventos.Domain.Entidades;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface
{
    public interface IPalestranteRepository
    {
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetAllPalestranteByIdAsync(int idPalestrante, bool includeEventos = false);
    }
}
