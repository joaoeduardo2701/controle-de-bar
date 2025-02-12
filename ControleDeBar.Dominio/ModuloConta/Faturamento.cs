namespace ControleDeBar.Dominio.ModuloConta;

public class Faturamento
{
    public TipoFaturamentoEnum TipoFaturamento { get; private set; }
    public List<Conta> ContasFechadas { get; private set; }

    public Faturamento(TipoFaturamentoEnum tipoFaturamento, List<Conta> contasFechadas)
    {
        TipoFaturamento = tipoFaturamento;
        ContasFechadas = contasFechadas;
    }

    public decimal CalcularTotal(out List<Conta> contasFiltradas)
    {
        decimal total = 0;

        DateTime dataAtual = DateTime.Now;

        DateTime inicioSemana = dataAtual.AddDays(-(int)dataAtual.DayOfWeek);
        DateTime fimSemana = inicioSemana.AddDays(7).AddSeconds(-1);

        DateTime inicioMes = new DateTime(dataAtual.Year, dataAtual.Month, 1);
        DateTime fimMes = inicioMes.AddMonths(1).AddSeconds(-1);

        contasFiltradas = new List<Conta>();

        if (TipoFaturamento == TipoFaturamentoEnum.Diario)
            contasFiltradas = ObterContasPorDia(dataAtual);

        else if (TipoFaturamento == TipoFaturamentoEnum.Semanal)
            contasFiltradas = ObterContasPorSemana(inicioSemana, fimSemana);

        else if (TipoFaturamento == TipoFaturamentoEnum.Mensal)
            contasFiltradas = ObterContasPorMes(inicioMes, fimMes);

        foreach (Conta conta in contasFiltradas)
            total += conta.CalcularValorTotal();

        return total;
    }

    public decimal CalcularTotalPeriodo(DateTime inicioPeriodo, DateTime finalPeriodo, out List<Conta> contasFiltradas)
    {
        decimal total = 0;

        contasFiltradas = ObterContasPorPeriodo(inicioPeriodo, finalPeriodo);

        foreach (Conta conta in contasFiltradas)
            total += conta.CalcularValorTotal();

        return total;
    }

    private List<Conta> ObterContasPorDia(DateTime dataAtual)
    {
        return ContasFechadas
                .Where(c => c.Fechamento.Date == dataAtual.Date)
                .ToList();
    }

    private List<Conta> ObterContasPorSemana(DateTime inicioSemana, DateTime fimSemana)
    {
        return ContasFechadas
            .Where(conta =>
            {
                return conta.Fechamento.Date >= inicioSemana &&
                    conta.Fechamento.Date <= fimSemana;
            })
            .ToList();
    }

    private List<Conta> ObterContasPorMes(DateTime inicioMes, DateTime fimMes)
    {
        return ContasFechadas
            .Where(conta =>
            {
                return conta.Fechamento.Date >= inicioMes &&
                    conta.Fechamento.Date <= fimMes;
            })
            .ToList();
    }

    private List<Conta> ObterContasPorPeriodo(DateTime inicioPeriodo, DateTime finalPeriodo)
    {
        return ContasFechadas
            .Where(c => c.Fechamento >= inicioPeriodo && c.Fechamento <= finalPeriodo)
            .ToList();
    }
}
