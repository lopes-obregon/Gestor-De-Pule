
using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace GestorDePule.Tests
{
    public class CaixaControllerTests
    {
        [Fact]
        public void ControllerTeste()
        {
            var options = new DbContextOptionsBuilder<DataBase>()
           .UseInMemoryDatabase("TestePules")
           .Options;
            using var contex = new DataBase(options);
            CaixaController caixaController = new CaixaController(contex);
            caixaController.NovoCaixa();

            var caixa = caixaController.GetCaixaRepository().GetCaixa();
            Assert.NotNull(caixa);

        }

        
    }
}
