namespace ControleDeBar.Dominio.ModuloConta;
public interface IRepositorioConta 
{
    void Inserir(Conta conta);
    bool AtualizarPedidos(Conta conta, List<Pedido> pedidosRemovidos);
    void AtualizarStatus(Conta contaFechada);

    Conta SelecionarPorId(int id);
    List<Conta> SelecionarContas();
    List<Conta> SelecionarContasEmAberto();
    List<Conta> SelecionarContasFechadas();
    List<Conta> SelecionarContasFaturamento();
}
