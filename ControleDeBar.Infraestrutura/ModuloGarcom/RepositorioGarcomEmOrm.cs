using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrutura.Compartilhado;

namespace ControleDeBar.Infraestrutura.ModuloGarcom;
public class RepositorioGarcomEmOrm : IRepositorioGarcom
{
    ControleDeBarDbContext dbContext;

    public RepositorioGarcomEmOrm(ControleDeBarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Inserir(Garcom registro)
    {
        dbContext.Garcons.Add(registro);    

        dbContext.SaveChanges();
    }

    public bool Editar(Garcom registroOriginal, Garcom registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.AtualizarInformacoes(registroAtualizado);  

        dbContext.Garcons.Update(registroOriginal);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Garcom registro)
    {
        if (registro == null)
            return false;

        dbContext.Garcons.Remove(registro);

        dbContext.SaveChanges();

        return true;
    }

    public Garcom SelecionarPorId(int id)
    {
        return dbContext.Garcons.Find(id)!;
    }

    public List<Garcom> SelecionarTodos()
    {
        return dbContext.Garcons.ToList();
    }
}
