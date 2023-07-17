using SandesForU.Context;
using SandesForU.Models;
using SandesForU.Repositories.Interfaces;

namespace SandesForU.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext context;
        private readonly CarrinhoCompra carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            this.context = context;
            this.carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            context.Add(pedido);
            context.SaveChanges();

            var carrinhoCompraItens = carrinhoCompra.CarrinhoCompraItems;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco
                };
                context.PedidoDetalhes.Add(pedidoDetalhe);
            }
            context.SaveChanges();
        }
    }
}
