using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System.Data;

namespace Gestor_De_Pule.src.Controllers
{
    internal class AnimalController
    {
      
       public  List<Animal> Animals { get; private set; } = new List<Animal>();
        public Animal? Animal { get; private set; } = new();
        //repository
        private AnimalRepository _repository { get; set; } = new AnimalRepository();
        //Controllers
        public PuleController _puleController { get; private set; } = new PuleController();
        internal  void LoadAnimais()
        {
            
            
                Animals = _repository.ReadAnimals().ToList();
            
        }

        internal  string Salvar(int número, string nome, string proprietário, string treinador, string jockey, string cidade)
        {
            bool sucess = false;
            Animal animal = new Animal(número, nome, proprietário,treinador, jockey, cidade);
            //sucess = Animal.Save(animal);
            sucess = _repository.Save(animal);
            if (sucess)
                return $"Animal {animal.Nome} Salvo com sucesso!";
            else
                return "Erro ao Salvar o animal";
        }

        internal  void AnimalSelecionado(object animalSelecionadoUi)
        {
            Animal? animalSelecionado = null;
            if (animalSelecionadoUi is Animal)
            {
                animalSelecionado = animalSelecionadoUi as Animal;
                //Animal? animalConsultado = Animal.Consultar(animalSelecionado);
                Animal? animalConsultado = _repository.Consultar(animalSelecionado);
                if (animalConsultado is not null) Animal = animalConsultado;
                else Animal = null;
            }
        }

        internal  string Atualizar(int número, string nome, string proprietário, string treinador, string cidade, string jockey)
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
                //sucess = Animal.Update(Animal);
                sucess = _repository.Update(Animal);
                if (sucess)
                    return "Atualização realizado com sucesso!";
                else
                    return "Algo deu Errado Para Atualizar!";
            

            }
            return "";
        }

        internal  string DeleteAnimal(object animalSelecionadoUi)
        {
            Animal? animal = animalSelecionadoUi as Animal;
            bool sucess = false;
            if (animal is not null)
                sucess = _repository.Delete(animal);
                //sucess = Animal.Delete(animal);
            if (sucess) return "Removido Com Sucesso!";
            else return "Erro ao Remover!";

        }

        internal void LoadListsAnimalAndPules()
        {
            LoadAnimais();
            
            //PuleController.LoadPules();
            _puleController.LoadPules();
            /*if (_puleController.Pules != null)
                Pules = _puleController.Pules.ToList();*/
        }

        internal object? SearchPule(Pule pule)
        {
            object puleEncontrado = null;
            if(pule is not null)
                puleEncontrado =  _puleController.FindPule(pule);
            return puleEncontrado;
        }

        internal void LoadAnimal(object animalSelecionadoUi)
        {
            Animal? animalSelecionado = animalSelecionadoUi as Animal;
            if (animalSelecionado != null)
            {
                if (Animal is not null)
                {
                    //verificar se é o mesmo animal
                    //se for diferente temos que dar o load no animal 
                    if (Animal.Id != animalSelecionado.Id)
                    {
                        Animal = Animal.GetAnimal(animalSelecionado);
                    }

                }
            }
        }

        internal void LoadLists()
        {
            
            LoadAnimais();
            
            _puleController.LoadPules();
            
        }
    }
}
