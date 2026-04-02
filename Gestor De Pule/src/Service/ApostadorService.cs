
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class ApostadorService
    {
        private readonly ApostadorRepository _repository;
        public List<Apostador> Apostadores;
        public ApostadorService(object repository)
        {
            _repository = new(repository);
            Apostadores = new List<Apostador>();
            
        }
        /// <summary>
        /// Loads apostadores associated with the specified animal identifier, either from the repository or by
        /// filtering the existing collection.
        /// </summary>
        /// <param name="animalId">The identifier of the animal to filter apostadores by.</param>
        internal void LoadApostadoresWithAnimalId(int animalId)
        {
            //memória vazia
            if (Apostadores.Count == 0)
            {
                Apostadores = _repository.LoadWithAnimalId(animalId);
            }
            else
            {
                //mémoria com dados
                if(!Apostadores.Any(ap=> ap.Pules.Any(p=> p.Animais.Any(a=> a.Id == animalId))))//caso não houver carrega do banco ou rastreados
                    Apostadores = _repository.LoadWithAnimalId(animalId);
            }
        }
    }
}
