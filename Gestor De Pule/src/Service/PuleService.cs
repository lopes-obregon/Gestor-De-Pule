using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    class PuleService
    {
        private readonly PuleRepository _repository;
        public PuleService(object context)
        {
            _repository = new PuleRepository(context);
        }

        internal static List<Pule>? ObterPulesSelecionados(object puleSelecionadosUi)
        {
            var pules = PuleRepository.BuscarPorIds(puleSelecionadosUi);
            if (pules == null) return null;
            else 
                return pules;
        }
        /// <summary>
        /// Get pule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Pule or null</returns>
        internal Pule? GetById(int id)
        {
            return _repository.GetPuleById(id);
        }
    }
}
