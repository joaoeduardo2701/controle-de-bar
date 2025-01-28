using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta;
public class Pedido
{
    public int id { get; set; }
    public Produto Produto { get; set; }
    public int QuantidadeSolicitada { get; set; }

    public Pedido() { }

    public Pedido(int id, Produto produto, int quantidadeSolicitada)
    {
        this.id = id;
        Produto = produto;
        QuantidadeSolicitada = quantidadeSolicitada;
    }

    public decimal CalculaTotalParcial()
    {
        return Produto.Valor * QuantidadeSolicitada;
    }
}
