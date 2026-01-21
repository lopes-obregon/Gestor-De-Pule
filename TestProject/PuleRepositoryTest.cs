namespace TestProject
{
    public class PuleRepositoryTest
    {
        [Fact]
        public void ReadPulesDeveRetornarListaComDados()
        {
            //arrange configurar contexto em memoria
            var options = new DbContextOptionsBuilder<Pule>()
            .UseInMemoryDatabase(databaseName: "TestePules")
            .Options;


        }
    }
}
