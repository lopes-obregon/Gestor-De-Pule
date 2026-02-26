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
            if(quantidadeRodadas > 0)
            {
                Rodada = new Rodada(quantidadeRodadas);
                RodadaRepository.AddContext(Rodada);
            }
        }
        /// <summary>
        /// Carrega a rodada com base no id da disputa
        /// </summary>
        /// <param name="idDisputa"></param>
        internal void LoadRodada(int idDisputa)
        {
            Rodada = RodadaRepository.Load(idDisputa);
        }
        /// <summary>
        /// Seta Rodada com o Track do context
        /// </summary>
        internal void Track()
        {
            Rodada = RodadaRepository.isTrack(Rodada);
        }

      
        //gera nova rodada sem parametros
        internal Rodada NovaRodada()
        {
            Rodada = new();
            RodadaRepository.AddContext(Rodada);
            Rodada.ResultadoDestaRodada = new();
            return Rodada;
        }

        internal Rodada NovaRodada(Disputa disputa)
        {
            Rodada = new(disputa);
            RodadaRepository.AddContext(Rodada);
            //Rodada.ResultadoDestaRodada = new();
            return Rodada;
        }
        /// <summary>
        /// Dispose Data base
        /// </summary>
        internal void Clear()
        {
            RodadaRepository?.Clear();
        }

        internal Rodada NovaRodada(Disputa disputa, int nRodada)
        {
            Rodada = new(disputa, nRodada);
            RodadaRepository.AddContext(Rodada);
            return Rodada;
        }
    }
}
