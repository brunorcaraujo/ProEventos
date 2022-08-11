using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entidades;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class LoteRepository :  ILoteRepository
    {
        private readonly ProEventosContext _context;

        public LoteRepository(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<Lote> GetLoteByIdsAsync(int idEvento, int idLote)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventoId == idEvento && lote.Id == idLote).OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int idEvento)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventoId == idEvento).OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}
