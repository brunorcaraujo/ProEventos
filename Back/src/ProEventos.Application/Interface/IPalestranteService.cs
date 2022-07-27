using ProEventos.Domain.Entidades;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);
        Task<Palestrante> UpdatePalestrante(int idEvento, Palestrante model);
        Task<bool> DeletePalestrante(int idEvento);
        Task<Palestrante[]> GetAllPalestrantesByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes = false);
        Task<Palestrante> GetAllPalestranteByIdAsync(int id, bool includePalestrantes = false);
    }
}
