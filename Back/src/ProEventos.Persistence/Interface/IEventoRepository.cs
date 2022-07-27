using ProEventos.Domain.Entidades;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interface
{
    public interface IEventoRepository
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetAllEventoByIdAsync(int idEvento, bool includePalestrantes = false);
    }
}
