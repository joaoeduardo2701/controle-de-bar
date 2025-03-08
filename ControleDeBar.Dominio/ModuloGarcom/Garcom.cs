using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.ModuloGarcom;
public class Garcom : EntidadeBase
{
    public string Nome { get; set; }
    public string CPF { get; set; }

    public Garcom () { }

    public Garcom(string nome, string cpf)
    {
        Nome = nome;
        CPF = cpf;
    }

    public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
    {
        Garcom garcomAtualizado = (Garcom)registroAtualizado;

        Nome = garcomAtualizado.Nome;
        CPF = garcomAtualizado.CPF;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrEmpty(Nome))
            erros.Add("O campo \"NOME\" é obrigatório");
        if (string.IsNullOrEmpty(CPF))
            erros.Add("O campo \"CPF\" é obrigatório");

        return erros;
    }

    public override string ToString()
    {
        return Nome;
    }
}
