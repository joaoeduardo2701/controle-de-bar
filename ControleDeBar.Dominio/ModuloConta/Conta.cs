using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Dominio.ModuloConta;
public class Conta : EntidadeBase
{
    public string Titular { get; set; }
    public Mesa Mesa { get; set; }
    public Garcom Garcom { get; set; }
    public DateTime Abertura { get; set; }
    public DateTime Fechamento { get; set; }
    public bool EstaAberta { get; set; }
    public List<Pedido> Pedidos { get; set; }

    public Conta()
    {
        Pedidos = new List<Pedido>();
    }

    public Conta(string titular, Mesa mesa, Garcom garcom) : this()
    {
        Titular = titular;
        Mesa = mesa;
        Garcom = garcom;

        Abrir();
    }

    public void Abrir()
    {
        EstaAberta = true;
        Abertura = DateTime.Now;

        if (Mesa != null) 
            Mesa.Ocupar();
    }

    public void Fechar()
    {
        EstaAberta = false;
        Fechamento = DateTime.Now;

        Mesa.Desocupar();
    }

    public Pedido RegistrarPedido(Produto produto, int quantidadeEscolhida)
    {
        Pedido novoPedido = new Pedido(produto, quantidadeEscolhida);

        Pedidos.Add(novoPedido);

        return novoPedido;
    }

    public void RemoverPedido(Pedido pedido)
    {
        Pedidos.Remove(pedido);
    }

    public decimal CalcularValorTotal()
    {
        return Pedidos.Sum(p => p.CalcularTotalParcial());
    }

    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Conta contaAtualizada = (Conta)registroAtualizado;

        EstaAberta = contaAtualizada.EstaAberta;
        Fechamento = contaAtualizada.Fechamento;
        Pedidos = contaAtualizada.Pedidos;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Titular.Length <= 3)
            erros.Add("O campo \"Titular\" é obrigatório");
        if (Garcom == null)
            erros.Add("O campo \"Garçom\" é obrigatorio");
        if (Mesa == null)
            erros.Add("O campo \"Mesa\" é obrigatorio");

        return erros;
    }

    public override string ToString()
    {
        return Titular;
    }
}
