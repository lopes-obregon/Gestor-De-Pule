using Gestor_De_Pule.src.Persistencias;
using Rodada = Gestor_De_Pule.src.Models.Rodada;

namespace Gestor_De_Pule.src.Service
{
    internal class RodadaService
    {
        /// <summary>
        /// DataBase or context
        /// </summary>
        private RodadaRepository _repository;
        /// <summary>
        /// List in cache Rodadas
        /// </summary>
        public List<Rodada>? Rodadas;
        /// <summary>
        /// Property Rodada cache
        /// </summary>
        public Rodada? Rodada;
        public RodadaService(object data)
        {
            _repository = new RodadaRepository(data);
        }

       

        /// <summary>
        /// Pesquisa pelo id a rodada
        /// </summary>
        /// <param name="rodadaId"></param>
        /// <returns>Return null se não encontrar a rodada ou a rodada pesquisada se encontrar</returns>
        internal Rodada? GetById(int? rodadaId)
        {
            Rodada? rodada;
            if (rodadaId != null) {
                rodada = _repository.GetById(rodadaId);



            }
            else
            {
                rodada = null;
            }
            return rodada;
        }
        /// <summary>
        /// Faz a chamada do repository
        /// </summary>
        /// <param name="idDisputa"></param>
        /// <returns></returns>
        internal List<Rodada>? GetByIdRodadas(int idDisputa)
        {
            List<Rodada>? rodadas;
            if (Rodadas is not null && Rodadas.Count > 0)
            {
                rodadas = Rodadas.Where(r => r.DisputaId == idDisputa).ToList();
            }
            else
            {
                rodadas = _repository.GetByIdRodadas(idDisputa);
                Rodadas = rodadas;
            }
            return rodadas;
        }
        /// <summary>
        /// New Rodada in set Rodada property
        /// </summary>
        /// <param name="quantidadeRodadas"></param>
        internal void NewRodada(int quantidadeRodadas)
        {
            if (quantidadeRodadas > 0)
            {
                Rodada = new Rodada(quantidadeRodadas);
                _repository.AddContext(Rodada);
            }
        }
    }
}
