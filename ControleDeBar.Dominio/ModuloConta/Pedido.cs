using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta;
public class Pedido
{
    public int Id { get; set; }
    public Produto Produto { get; set; }
    public int QuantidadeSolicitada { get; set; }

    public Pedido() { }

    public Pedido(Produto produto, int quantidadeSolicitada)
    {
        Produto = produto;
        QuantidadeSolicitada = quantidadeSolicitada;
    }

    public decimal CalcularTotalParcial()
    {
        return Produto.Valor * QuantidadeSolicitada;
    }
}
