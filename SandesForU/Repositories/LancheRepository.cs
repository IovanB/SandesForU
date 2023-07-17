using Microsoft.EntityFrameworkCore;
using SandesForU.Context;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;

namespace SandesForU.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
            .Where(w => w.IsLanchePreferido == true)
            .Include(c => c.Categoria); 

        public Lanche GetLancheById(int id)
        {
            return _context.Lanches.FirstOrDefault(w => w.LancheId == id);
        }
    }
}
