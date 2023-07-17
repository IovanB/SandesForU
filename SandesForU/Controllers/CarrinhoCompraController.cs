using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;
using SandesForU.ViewModels;

namespace SandesForU.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository lancheRepository;
        private readonly CarrinhoCompra carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            this.lancheRepository = lancheRepository;
            this.carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = carrinhoCompra.GetCarrinhoCompraItens();
            carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = carrinhoCompra,
                CarrinhoCompraTotal = carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        [Authorize]
        public IActionResult AdicionarItensNoCarrinho(int lancheId)
        {
            var lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(s => s.LancheId == lancheId);

            if(lancheSelecionado != null)
            {
                carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);  
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItensNoCarrinho(int lancheId)
        {
            var lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(s => s.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
