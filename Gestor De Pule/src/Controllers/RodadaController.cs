using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RodadaController
    {
        public Rodada? Rodada { get; private set; }
        public RodadaRepository RodadaRepository { get; private set; }
        public RodadaController(object data)
        {
            RodadaRepository = new RodadaRepository(data);
        }
        public RodadaController()
        {
            RodadaRepository = new RodadaRepository();
        }
        internal void NovaRodada(Disputa disputa, int quantidadeRodadas, List<Pule>? pules, List<Resultado>? resultadoList)
        {
           Rodada = new Rodada(disputa,  quantidadeRodadas, pules, resultadoList);
        }

        internal void NovaRodada(int quantidadeRodadas)
        {
            if(quantidadeRodadas > 1)
            {
                Rodada = new Rodada(quantidadeRodadas);
                RodadaRepository.AddContext(Rodada);
            }
        }
    }
}
