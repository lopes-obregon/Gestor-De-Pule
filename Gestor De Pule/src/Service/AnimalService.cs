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

        public AnimalService(object data)
        {
            _animalRepository = new AnimalRepository(data);
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
        
            if (Animals is not null && Animals.Count > 0)
            {
                animal = Animals.FirstOrDefault(a => a.Id == animalId);
            }
            else if(Animals is not null && !Animals.Any(a=> a.Id == animalId))
            {
                animal = _animalRepository.GetAnimalById(animalId);
                if(animal is not null)
                    Animals.Add(animal);
            }
            {
                if (Animal is not null && Animal.Id == animalId)
                    animal = Animal;
                else
                {
                    animal = _animalRepository.GetAnimalById(animalId);
                    if (Animals is null)
                        Animals = new List<Animal>();
                    if (animal != null)
                        Animals.Add(animal);
                }

            }

          

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
    }
}
