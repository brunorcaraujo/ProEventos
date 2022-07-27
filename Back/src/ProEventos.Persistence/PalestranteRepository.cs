using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly ProEventosContext _context;

        public PalestranteRepository(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int idPalestrante, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                       .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                        .Where(p => p.Id == idPalestrante);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                    .Include(e => e.RedesSociais);

            if (includeEventos)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                   .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                        .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}
