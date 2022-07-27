using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ProEventosContext _context;

        public EventoRepository(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                                    .Include(e => e.Lotes)
                                                    .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoByIdAsync(int idEvento, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(e => e.Lotes)
                                        .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == idEvento);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                                                    .Include(e => e.Lotes)
                                                    .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id)
                        .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}
