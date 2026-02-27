using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class AnimalService
    {
        private AnimalRepository _animalRepository;
       

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
            return _animalRepository.LoadAnimaisWithPules();
        }

        internal List<Animal>? GetAnimalsByIdPule(int id)
        {
            List<Animal>? animais;
            animais = _animalRepository.GetAnimalsByIdPule(id);
            return animais;


        }
    }
}
