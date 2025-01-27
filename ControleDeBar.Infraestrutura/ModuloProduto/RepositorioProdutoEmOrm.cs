using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Compartilhado;

namespace ControleDeBar.Infraestrutura.ModuloProduto;
public class RepositorioProdutoEmOrm : IRepositorioProduto
{
    ControleDeBarDbContext dbContext { get; set; }

    public RepositorioProdutoEmOrm(ControleDeBarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Inserir(Produto registro)
    {
        dbContext.Produtos.Add(registro);

        dbContext.SaveChanges();
    }

    public bool Editar(Produto registroOriginal, Produto registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroAtualizado.AtualizarInformacoes(registroAtualizado);

        dbContext.Produtos.Update(registroOriginal);

        dbContext.SaveChanges();

        return true;

    }

    public bool Excluir(Produto registro)
    {
        if (registro == null)
            return false;

        dbContext.Produtos.Remove(registro);

        dbContext.SaveChanges();

        return true;
    }

    public Produto SelecionarPorId(int id)
    {
        return dbContext.Produtos.Find(id)!;
    }

    public List<Produto> SelecionarTodos()
    {
        return dbContext.Produtos.ToList();
    }
}
