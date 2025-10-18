using Gestor_De_Pule.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Gestor_De_Pule.src.Controllers
{
    internal class AnimalController
    {
        static List<Animal> _animals = new();
        public static Animal? Animal { get; private set; } = new();
        internal static void LoadAnimais()
        {
            _animals = Animal.ReadAnimals();
        }
       public static List<Animal> Animals { get { return _animals; } }

        internal static string Salvar(int número, string nome, string proprietário, string treinador, string jockey, string cidade)
        {
            bool sucess = false;
            Animal animal = new Animal(número, nome, proprietário,treinador, jockey, cidade);
            sucess = Animal.Save(animal);
            if (sucess)
                return $"Animal {animal.Nome} Salvo com sucesso!";
            else
                return "Erro ao Salvar o animal";
        }

        internal static void AnimalSelecionado(object animalSelecionadoUi)
        {
            Animal? animalSelecionado = null;
            if (animalSelecionadoUi is Animal)
            {
                animalSelecionado = animalSelecionadoUi as Animal;
                Animal? animalConsultado = Animal.Consultar(animalSelecionado);
                if (animalConsultado is not null) Animal = animalConsultado;
                else Animal = null;
            }
        }

        internal static string Atualizar(int número, string nome, string proprietário, string treinador, string cidade, string jockey)
        {
            bool sucess = false;
            if(Animal is not null)
            {
                if (número != Animal.Número)
                    Animal.Número = número;
                else if (!String.IsNullOrEmpty(nome) && nome != Animal.Nome)
                    Animal.Nome = nome;
                else if (!String.IsNullOrEmpty(proprietário) && proprietário != Animal.Proprietário)
                    Animal.Proprietário = proprietário;
                else if (!String.IsNullOrEmpty(treinador) && treinador != Animal.Treinador)
                    Animal.Treinador = treinador;
                else if (!String.IsNullOrEmpty(cidade) && cidade != Animal.Cidade)
                    Animal.Cidade = cidade;
                else if (!String.IsNullOrEmpty(jockey) && jockey != Animal.Jockey)
                    Animal.Jockey = jockey;
                else
                    return "Precisa mudar algum dado!";
                sucess = Animal.Update(Animal);
                if (sucess)
                    return "Atualização realizado com sucesso!";
                else
                    return "Algo deu Errado Para Atualizar!";
            

            }
            return "";
        }

        internal static string DeleteAnimal(object animalSelecionadoUi)
        {
            Animal? animal = animalSelecionadoUi as Animal;
            bool sucess = false;
            if(animal is not null)
                sucess = Animal.Delete(animal);
            if (sucess) return "Removido Com Sucesso!";
            else return "Erro ao Remover!";

        }
    }
}
