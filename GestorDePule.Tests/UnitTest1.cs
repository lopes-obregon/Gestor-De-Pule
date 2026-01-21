using Microsoft.EntityFrameworkCore;
using Gestor_De_Pule.src.Persistencias;

using Gestor_De_Pule.src.Model;
namespace GestorDePule.Tests;

public class UnitTest1
{
    [Fact]
    public void ReadPulesDeveRetornarListaComDados()
    {
        var options = new DbContextOptionsBuilder<DataBase>()
           .UseInMemoryDatabase( "TestePules")
           .Options;
        using var contex = new DataBase(options);
        contex.Pules.Add(new Pule
        {
            Apostador = null,
            StatusPagamento = StatusPagamento.Pendente,
            Animais = null,
            Valor = 10.0f,
            Date = DateTime.Now,
            Número = 2
        });

        contex.SaveChanges();
        var repo = new PuleRepository(contex);
        var resultado = repo.ReadPules();

        Assert.Single(resultado);
        Assert.Equal(2, resultado[0].Número);  

       

    }
}
