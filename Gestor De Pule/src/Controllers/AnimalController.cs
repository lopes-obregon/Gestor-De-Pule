using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using System.Data;

namespace Gestor_De_Pule.src.Controllers
{
    internal class AnimalController
    {
      
       public  List<Animal> Animals { get; private set; } = new List<Animal>();
        public Animal? Animal { get; private set; } = new();
        //repository
        private AnimalRepository _animalRepository { get;  set; }
        //Controllers
        public PuleController? _puleController { get; private set; } = null;
        
        public AnimalController()
        {
            _animalRepository = new AnimalRepository();
            //_puleController = new PuleController();
        }
        public AnimalController(object data)
        {
            _animalRepository  = new AnimalRepository(data);
        }
        public AnimalController(PuleController puleController)
        {
            _puleController = puleController;
            _animalRepository = new AnimalRepository();
        }

        internal  void LoadAnimais()
        {
            
            
                Animals = _animalRepository.ReadAnimals().ToList();
            
        }

        internal  string Salvar(int número, string nome, string proprietário, string treinador, string jockey, string cidade)
        {
            bool sucess = false;
            Animal animal = new Animal(número, nome, proprietário,treinador, jockey, cidade);
            //sucess = Animal.Save(animal);
            if(_animalRepository is not null)
                sucess = _animalRepository.Save(animal);
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
                Animal? animalConsultado = _animalRepository.Consultar(animalSelecionado);
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
                sucess = _animalRepository.Update(Animal);
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
                sucess = _animalRepository.Delete(animal);
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
                Animal? TrackAnimal = _animalRepository.IsTracked(animalSelecionado);
                Animal = TrackAnimal;
            }
        }

        internal void LoadLists()
        {
            
            LoadAnimais();
            
            _puleController.LoadPules();
            
        }

        internal void LoadListsResultado()
        {
            if(Animal is not null)
            {
                Animal.Resultados = _animalRepository.LoadResultados(Animal);
            }
        }

        

        internal Animal? IsTracked(Animal animalUi)
        {
            Animal? animal = null;
            animal = _animalRepository?.IsTracked(animalUi);
            return animal;
        }

        internal List<Pule> GetPules(Animal animal)
        {
            var pules = _animalRepository.GetPules(animal);
            if (pules == null)
                return new List<Pule>();
            else
                return pules.ToList();

        }
    }
}
