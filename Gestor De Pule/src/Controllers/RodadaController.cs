using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RodadaController
    {
        /// <summary>
        /// Rodada cache
        /// </summary>
        public Rodada? Rodada { get; private set; }
        /// <summary>
        /// List cache Rodadas
        /// </summary>
        public List<Rodada> Rodadas { get; private set; }
        public RodadaRepository RodadaRepository { get; private set; }
        public RodadaService _rodaService { get; private set; }
        public RodadaController(object data)
        {
            RodadaRepository = new RodadaRepository(data);
            _rodaService = new(data);
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
        /// <summary>
        /// New Rodada
        /// </summary>
        /// <param name="disputa"></param>
        /// <param name="nRodada"></param>
        /// <returns>Instancia de rodada</returns>
        internal Rodada NovaRodada(Disputa disputa, int nRodada)
        {
            Rodada = new(disputa, nRodada);
            RodadaRepository.AddContext(Rodada);
            return Rodada;
        }
        /// <summary>
        /// set a Rodada in cache
        /// </summary>
        /// <param name="rodadaId"></param>
        /// <returns>O id da rodada pesquisada</returns>
        internal Rodada? GetById(int? rodadaId)
        {

            if(Rodadas is not null)
            {
                if(rodadaId is not  null)
                    Rodada = Rodadas.FirstOrDefault(r => r.Id == rodadaId);
            }
            else if (Rodada != null)
            {
                if(Rodada.Id != rodadaId)
                    Rodada = _rodaService.GetById(rodadaId);
            }
            else
            {
                Rodada = _rodaService.GetById(rodadaId);
            }
            return Rodada;
        }
    }
}
