using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;
using System.Data;

namespace Gestor_De_Pule.src.Controllers
{
    internal class AnimalController
    {

        public List<Animal>? Animals => _animalService.Animals;
        public Animal? Animal => _animalService.Animal;
        //repository
        private AnimalRepository _animalRepository { get;  set; }
        private readonly Repository _context;

        //Controllers
        public PuleController? _puleController { get; private set; } = null;
        private AnimalService _animalService { get; set; }
        public AnimalController()
        {
            _context = new Repository();
            var context = _context.GetDataBase();
            PuleService puleService = new(context);
            _animalRepository = new AnimalRepository();

            _animalService = new(context, puleService);
            //_puleController = new PuleController();
        }
        public AnimalController(object data)
        {
            PuleService puleService = new(data);
            _animalRepository  = new AnimalRepository(data);
            _animalService = new AnimalService(data, puleService);
        }
     
        /// <summary>
        /// Load in Animals list animals
        /// </summary>
        internal  void LoadAnimais()
        {
            if (Animals is null || Animals.Count == 0)
                _animalService.GetAnimals();
                //Animals = _animalRepository.ReadAnimals().ToList();
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

        internal  void AnimalSelecionado(int animalId)
        {
            _animalService.GetAnimalById(animalId);
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
               // Animal = TrackAnimal;
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
        /// <summary>
        /// Loads animals from the repository based on the selected items and assigns them to the Animals property.
        /// </summary>
        /// <param name="animaisSelecionados">The collection of selected animal items to load.</param>
        internal void LoadAnimais(ListBox.ObjectCollection animaisSelecionados)
        {
            _animalService.LoadAnimais(animaisSelecionados);
           // Animals = _animalRepository.LoadAnimais(animaisSelecionados);
        }

       
        /// <summary>
        /// Dispose Context;
        /// </summary>
        internal void Clear()
        {
            _animalRepository.Clear();
        }
        /// <summary>
        /// Pega os items de uma lista faz um cast para animal e devolve como uma lista
        /// </summary>
        /// <param name="items"></param>
        /// <returns>inteiro list </returns>
        internal List<int> GetListId(ListBox.ObjectCollection items)
        {
            List<int> animals = new ();
            List<Animal>? animals1 = items.Cast<Animal>().ToList();
            if (animals1 != null)
            {
                animals = animals1.Select(a=> a.Id).ToList();
            }
            return animals;
           
        }
        /// <summary>
        /// Chama o serviço que busca os animais
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        /// <returns>Os Animais buscados</returns>
        internal List<Animal>? GetAnimals(List<int> animaisSelecionados)
        {
            if (animaisSelecionados.Count > 0)
            {
                return _animalService.GetAnimalsByIdList(animaisSelecionados);

            }
            else return null;
        }
        /// <summary>
        /// objets To animals in memory
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        /// <returns>List animals or new list</returns>
        internal List<Animal> GetAnimals(ListBox.ObjectCollection animaisSelecionados)
        {
            var animais = animaisSelecionados.Cast<Animal>().ToList();
            List<Animal> animals = new List<Animal>();
            if(animais != null && animais.Count > 0)
            {
                animals = Animals.Where(a => animais.Any(an=> an.Id == a.Id)).ToList();
            }
            return animals;
        }
        /// <summary>
        /// Load animals with pules
        /// </summary>
        internal void LoadAnimaisWithPules()
        {
            _animalService.LoadAnimaisWithPules();
        }
        /// <summary>
        /// Pesquisa no cache caso não tenha vai verificar no banco
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de animais que estão linkado com o pule</returns>
        internal List<Animal> GetAnimalsByIdPule(int id)
        {
            List<Animal> animais = new();
            if(Animals != null)
                try
                {
                    animais =  Animals.Where(a=> a.Pules.Any(pu=> pu.Id == id)).ToList();

                }
                catch (ArgumentNullException)
                {
                    animais = _animalService.GetAnimalsByIdPule(id);

                }
            
            if (animais is null)
                animais = new List<Animal>();
            //Animals = animais.Cast<Animal>().ToList();
            return animais;
        }
        /// <summary>
        /// Busca o animal pelo id fornescido, busca no Animals caso não tenha busca no banco
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns>O animal buscado ou null se não encontrar</returns>
        internal Animal? GetAnimalById(int animalId)
        {
            return _animalService.GetAnimalById(animalId);
        }
        /// <summary>
        /// call service to load animals in cache
        /// </summary>
        /// <param name="pulesIds"></param>
        internal void LoadAnimaisWithPulesId(List<int> pulesIds)
        {
            _animalService.LoadAnimaisWithPules(pulesIds);
        }
        /// <summary>
        /// Indicates whether the associated animal is null.
        /// </summary>
        /// <returns>true if the animal is null; otherwise, false.</returns>
        internal bool IsNull()
        {
            return _animalService.AnimalIsNull();
        }
      
        /// <summary>
        /// Retrieves the animal's number and name as a string.
        /// </summary>
        /// <returns>A string containing the animal's number and name.</returns>
        internal string GetAnimalNúmeroNome()
        {
            return _animalService.GetNúmeroNome();
        }

        internal void LoadPules(int animalId)
        {
            _animalService.PulesWithAnimalId(animalId);
        }
        /// <summary>
        /// Gets the total number of pules from the animal service.
        /// </summary>
        /// <returns>The total count of pules.</returns>
        internal int TotalPules()
        {
            return _animalService.GetTotalPules();
        }
    }
}
