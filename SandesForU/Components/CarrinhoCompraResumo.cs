using Microsoft.AspNetCore.Mvc;
using SandesForU.Models;
using SandesForU.ViewModels;

namespace SandesForU.Components
{
    public class CarrinhoCompraResumo: ViewComponent
    {
        private readonly CarrinhoCompra carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            this.carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
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

    }
}
