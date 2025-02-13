using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Testes.Unidade.ModuloConta;

[TestClass]
[TestCategory("Testes de Unidade - Conta")]
public sealed class ContaTests
{
    [TestMethod]
    public void Deve_Validar_Conta_Corretamente()
    {
        // Arrange 
        Conta contaInvalida = new Conta("", null, null);

        List<string> errosEsperados =
        [
            "O campo \"Titular\" é obrigatório",
            "O campo \"Garçom\" é obrigatorio",
            "O campo \"Mesa\" é obrigatorio"
        ];

        // Act

        List<string> erros = contaInvalida.Validar();

        // Assert

        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Abrir_Conta_Corretamente()
    {
        // Arrange

        Mesa mesa = new Mesa("01");

        Garcom garcom = new Garcom("Juninho Testes", "115.234.232-23");

        // Act

        Conta conta = new Conta("João Eduardo", mesa, garcom);

        // Assert

        Assert.IsTrue(conta.EstaAberta);
        Assert.IsTrue(mesa.Ocupada);
        Assert.AreNotEqual(DateTime.MinValue, conta.Abertura);
    }

    [TestMethod]
    public void Deve_Fechar_Conta_Corretamente()
    {
        // Arrange

        Mesa mesa = new Mesa("01");

        Garcom garcom = new Garcom("Juninho Testes", "115.234.232-23");

        Conta conta = new Conta("João Eduardo", mesa, garcom);

        // Act

        conta.Abrir();
        conta.Fechar();

        // Assert

        Assert.IsFalse(conta.EstaAberta);
        Assert.IsFalse(mesa.Ocupada);
        Assert.AreNotEqual(DateTime.MinValue, conta.Abertura);
    }

    [TestMethod]
    public void Deve_Calcular_Total_Corretamente()
    {
        // Arrange

        Mesa mesa = new Mesa("01");

        Garcom garcom = new Garcom("Juninho Testes", "115.234.232-23");

        Conta conta = new Conta("João Eduardo", mesa, garcom);

        Produto produto1 = new Produto("Coca Lata 350ml", 5.00m);
        Produto produto2 = new Produto("Água mineral sem gás", 4.50m);

        conta.RegistrarPedido(produto1, 2);

        conta.RegistrarPedido(produto2, 3);

        // Act

        decimal total = conta.CalcularValorTotal();

        // Assert 

        decimal totalEsperado = 23.50m;

        Assert.AreEqual(totalEsperado, total);
    }
}
