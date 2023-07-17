using SandesForU.Context;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;

namespace SandesForU.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
