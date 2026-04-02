using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class AnimalService
    {
        private AnimalRepository _animalRepository;
       
        public Animal? Animal;
        /// <summary>
        /// Campo onde contem os animais em memória
        /// </summary>
        
        public List<Animal>? Animals;

        private readonly PuleService _puleService;
        private readonly ApostadorService _apostadorService;
        public AnimalService(DataBase data)
        {
            _animalRepository = new AnimalRepository(data);
            if(_puleService is null)
                _puleService = new PuleService(data);
            if(_apostadorService is null)
                _apostadorService = new ApostadorService(data);
            
        }
        public AnimalService(object data, PuleService puleService, ApostadorService apostadorService): this((DataBase)data)
        {
            
            _puleService = puleService;
            _apostadorService = apostadorService;
        }

        /// <summary>
        /// LoadAnimais com pules
        /// </summary>
        /// <returns>List animals with pules</returns>
        public List<Animal> LoadAnimaisWithPules()
        {
            Animals =  _animalRepository.LoadAnimaisWithPules();
            return Animals;
        }
        /// <summary>
        /// Chama o repository para buscar o animal pelo id
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns>O animal encontrado ou não no banco</returns>
        internal Animal? GetAnimalById(int animalId)
        {
            Animal? animal;

            if (Animals?.Count > 0)
            {
                animal = Animals.FirstOrDefault(a => a.Id == animalId);
            }
            else if (Animals is not null && !Animals.Any(a => a.Id == animalId))
            {
                animal = _animalRepository.GetAnimalById(animalId);
                if (animal is not null)
                    Animals.Add(animal);
            }
            else
            {
                animal = _animalRepository.GetAnimalById(animalId);
                if (Animals is null)
                    Animals = new List<Animal>();
                if (animal != null)
                    Animals.Add(animal);
            }

            Animal = animal;
            return animal;

        }

        internal List<Animal>? GetAnimalsByIdPule(int id)
        {
            List<Animal>? animais;
            animais = _animalRepository.GetAnimalsByIdPule(id);
            return animais;


        }
        /// <summary>
        /// Carrega na lista Animals os animais selecionados
        /// </summary>
        /// <param name="items"></param>
        internal void LoadAnimalsSelecionados(ListBox.ObjectCollection items)
        {
            List<Animal>? animalSelecionado;
            var animaisUi = items.Cast<Animal>().ToList();
            if (Animals is not null)
            {
                animalSelecionado = Animals.Where(a => animaisUi.Any(an => an.Id == a.Id)).ToList();
                if (animalSelecionado.Count > 0)
                {
                    Animals = animalSelecionado;
                }
            }
            else
            {
                Animals = _animalRepository.LoadAnimais(items);

            }

        }
        /// <summary>
        /// Verifica se existe animais no cache, caso não tenha procura no banco;
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        /// <returns>List de animais pesquisado</returns>
        internal List<Animal>? GetAnimalsByIdList(List<int> animaisSelecionados)
        {
            List<Animal>? animais;
            if(Animals is not null)
            {
                animais = Animals.Where(a => animaisSelecionados.Contains(a.Id)).Distinct().ToList();
            }
            else
            {
                animais = _animalRepository.GetAnimalByIdList(animaisSelecionados);
            }

                return animais;
        }
        /// <summary>
        /// set Animals and call repository
        /// </summary>
        /// <param name="pulesIds"></param>
        internal void LoadAnimaisWithPules(List<int> pulesIds)
        {
            Animals = _animalRepository.LoadAnimaisWithPules(pulesIds);
        }
        /// <summary>
        /// Load in cache Animals
        /// </summary>
        internal void GetAnimals()
        {
            Animals = _animalRepository.ReadAnimals();
        }
        /// <summary>
        /// Load animals in cache 
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        internal void LoadAnimais(ListBox.ObjectCollection animaisSelecionados)
        {
            Animals = _animalRepository.LoadAnimais(animaisSelecionados);
        }
        /// <summary>
        /// Obtem em memória os animais referente a Disputa relacionada
        /// </summary>
        /// <param name="id"> identificador da Disputa</param>
        /// <returns>List com os Animais encontrados.</returns>
        internal List<Animal> GetAnimalsWithDisputaId(int id)
        {
            List<Animal> animais = new();
            if (Animals is not null)
                animais = Animals.Where(an => an.Resultados.Any(res => res.DisputaId == id)).DistinctBy(a => a.Id).ToList();
            return animais;
        }
        /// <summary>
        /// check if animal is null
        /// </summary>
        /// <returns>true if animal is null or false</returns>
        internal bool AnimalIsNull()
        {
            if (Animals is null)
                return true;
            else
                return false;
        }
      
        /// <summary>
        /// Gets a formatted string containing the animal's number and name, or a placeholder if the animal is null.
        /// </summary>
        /// <returns>A string in the format "Número - Nome" if the animal is not null; otherwise, a placeholder string.</returns>
        internal string GetNúmeroNome()
        {
            if (Animal is null)
                return "-----------";
            else
                return $"{Animal.Número} - {Animal.Nome}";
        }
        /// <summary>
        /// Loads pules associated with the specified animal identifier.
        /// </summary>
        /// <param name="animalId">The unique identifier of the animal.</param>
        /// <returns>A list pules with associet animal</returns>
        internal List<Pule> PulesWithAnimalId(int animalId)
        {
            return _puleService.LoadPulesWithAnimalById(animalId);
        }
        /// <summary>
        /// Retrieves the total number of pules from the underlying service.
        /// </summary>
        /// <returns>The total number of pules.</returns>
        internal int GetTotalPules()
        {
            return _puleService.GetTotalPules();
        }
        /// <summary>
        /// Loads apostadores associated with the specified animal identifier.
        /// </summary>
        /// <param name="animalId">The identifier of the animal to filter apostadores.</param>
        internal void LoadApostadoresWithAnimalId(int animalId)
        {
            _apostadorService.LoadApostadoresWithAnimalId(animalId);
        }
        /// <summary>
        /// Detect if apostadores from service is null
        /// </summary>
        /// <returns> true if <see cref="_apostadorService.Apostadores"/> is null or  false otherwite</returns>
        internal bool ApostadoresIsNull()
        {

            if(_apostadorService.Apostadores is null)
                return true;
            else return false;
        }
        /// <summary>
        /// Retrieves a list of bettors who have placed bets on the specified animal.
        /// </summary>
        /// <param name="animalId">The identifier of the animal to filter bettors by.</param>
        /// <returns>A list of bettors associated with the specified animal. Returns an empty list if none are found.</returns>
        internal List<Apostador> Apostadores(int animalId)
        {
            List<Apostador> apostadors = new List<Apostador>();
            if(_apostadorService.Apostadores.Count > 0)
            {

                apostadors = _apostadorService.Apostadores.Where(ap => ap.Pules.Any(p => p.Animais.Any(a => a.Id == animalId))).ToList();

               
            }
            return apostadors;
        }

      

        /// <summary>
        /// Check if <see cref="PuleService.Pules"/> is null
        /// </summary>
        /// <returns>true if is null or false otherwite</returns>
        internal bool PulesIsNull()
        {
            if(_puleService.Pules is null)
                return true;
            else return false;
        }
        /// <summary>
        /// Calculates the total amount wagered for the specified animal.
        /// </summary>
        /// <param name="animalId">The identifier of the animal.</param>
        /// <returns>The total amount wagered as a decimal value.</returns>
        internal decimal TotalApostado(int animalId)
        {
            decimal total =0.0m;
            if(_puleService.Pules.Count > 0)
            {
                foreach (var pule in _puleService.Pules)
                {
                    if (pule is not null && pule.Animais is not null && pule.Animais.Any(a=> a.Id == animalId))
                    {
                        total += pule.Valor;
                    }
                }
            }
            return total;
        }
    }
}
