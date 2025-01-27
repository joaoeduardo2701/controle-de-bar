using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Compartilhado;

namespace ControleDeBar.Infraestrutura.ModuloMesa;
public class RepositorioMesaEmOrm : IRepositorioMesa
{
    ControleDeBarDbContext dbContext;

    public RepositorioMesaEmOrm(ControleDeBarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Inserir(Mesa registro)
    {
        dbContext.Mesas.Add(registro);

        dbContext.SaveChanges();
    }

    public bool Editar(Mesa registroOriginal, Mesa registroAtualizado)
    {
        if (registroOriginal == null || registroAtualizado == null)
            return false;

        registroOriginal.AtualizarInformacoes(registroAtualizado);
        
        dbContext.Mesas.Update(registroOriginal);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Mesa registro)
    {
        if (registro  == null) 
            return false;

        dbContext.Mesas.Remove(registro);

        dbContext.SaveChanges();

        return true;
    }

    public Mesa SelecionarPorId(int id)
    {
        return dbContext.Mesas.Find(id)!;
    }

    public List<Mesa> SelecionarTodos()
    {
        return dbContext.Mesas.ToList();
    }
}
