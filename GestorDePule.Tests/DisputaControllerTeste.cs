using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Windows.Controls;

namespace GestorDePule.Tests
{
    public  class DisputaControllerTeste
    {
        [Fact]
        public void CadastroDisputa()
        {
            var options = new DbContextOptionsBuilder<DataBase>()
           .UseInMemoryDatabase("TestePules")
           .Options;
            using var contex = new DataBase(options);
            DisputaController disputaController = new DisputaController(contex);
            
            System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();

            Animal animal = new Animal(1, "Animal1", "proprietário1", "treinador1", "jockey1", "cidade1");
            Animal animal2 = new Animal(2, "Animal2", "proprietário2", "treinador2", "jockey2", "cidade1");
            Animal animal3 = new Animal(3, "Animal3", "proprietário3", "treinador3", "jockey3", "cidade1");
           AnimalRepository animalRepository = new AnimalRepository();
           contex.Animals.Add(animal);
           contex.Animals.Add(animal2);
           contex.Animals.Add(animal3);
            contex.SaveChanges();
            var repoAnimal = new AnimalRepository(contex);
            var resultadoAnimal = repoAnimal.ReadAnimals();

            disputaController.LoadLists();

            Assert.NotNull(resultadoAnimal);
            Assert.Equal(1, resultadoAnimal[0].Número);
            listBox.Items.Add(resultadoAnimal[0]);
            listBox.Items.Add(resultadoAnimal[1]);
            disputaController.Cadastrar("Disputa1", DateTime.Now, listBox.Items, 2);

            var disputasRepo = new DisputaRepository(contex);
            var disputas = disputaController.DisputaRepository.GetDisputas();

            Assert.NotNull(disputas);
            Assert.Equal(true, disputas.Count == 1);
            //Assert.Equal("Disputa1", disputas.First().Nome);


            
        }
    }
}
