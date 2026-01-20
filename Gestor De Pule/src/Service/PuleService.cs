using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    class PuleService
    {
        internal static List<Pule>? ObterPulesSelecionados(object puleSelecionadosUi)
        {
            var pules = PuleRepository.BuscarPorIds(puleSelecionadosUi);
            if (pules == null) return null;
            else 
                return pules;
        }
    }
}
