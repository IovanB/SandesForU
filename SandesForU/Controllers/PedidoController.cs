using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;

namespace SandesForU.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly CarrinhoCompra carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            this.pedidoRepository = pedidoRepository;
            this.carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            List<CarrinhoCompraItem> items = carrinhoCompra.GetCarrinhoCompraItens();
            carrinhoCompra.CarrinhoCompraItems = items;

            if (carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Teu carrinho está vazio. Que tal incluir um lanche?");
            }

            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Quantidade * item.Lanche.Preco);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if(ModelState.IsValid)
            {
                pedidoRepository.CriarPedido(pedido);

                //Definir mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido!";
                ViewBag.TotalPedido = carrinhoCompra.GetCarrinhoCompraTotal();

                //Limpar o carrinho
                carrinhoCompra.LimparCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);
        }
    }
}
