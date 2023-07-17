using Microsoft.AspNetCore.Mvc;
using SandesForU.Repositories.Interfaces;

namespace SandesForU.Components
{
    public class CategoriaMenu: ViewComponent
    {
        private readonly ICategoriaRepository categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = categoriaRepository.Categorias.OrderBy( x=> x.CategoriaNome );
            return View( categorias );
        }
    }
}
