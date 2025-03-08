using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.ModuloProduto;
public class Produto : EntidadeBase
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }

    public Produto() { }

    public Produto(string nome, decimal valor)
    {
        Nome = nome;
        Valor = valor;
    }

    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Produto produtoAtualizado = (Produto)registroAtualizado;

        produtoAtualizado.Nome = Nome;
        produtoAtualizado.Valor = Valor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Nome))
            erros.Add("O campo \"NOME\" é obrigatório!");
        if (Valor < 0)
            erros.Add("O valor informado deve ser maior que zero");

        return erros;
    }

    public override string ToString()
    {
        return Nome;
    }
}
