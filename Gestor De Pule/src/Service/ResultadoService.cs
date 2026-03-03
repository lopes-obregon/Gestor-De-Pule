using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class ResultadoService
    {
        private ResultadoRepository _repository;
        public List<Resultado>? Resultados;
        public ResultadoService(object data)
        {
            _repository = new ResultadoRepository(data);
        }
        /// <summary>
        /// Busca no banco os resultados com o id da rodada informado
        /// </summary>
        /// <param name="idRodada"></param>
        /// <returns>retorna a lista buscada o nova lista vazia</returns>
        internal List<Resultado> GetResultadosByidRodada(int idRodada)
        {
            List<Resultado> resultados;
            if (Resultados != null)
            {
                resultados = Resultados.Where(res => res.RodadaId == idRodada).ToList();
            }
            else
            {
                resultados = _repository.ReadResultadosByidRodada(idRodada);
            }
            if(resultados is null)
                resultados = new List<Resultado>();
            return resultados;



         
        }
        /// <summary>
        /// Load Resultados in Resultados cache;
        /// </summary>
        internal void LoadResultados()
        {
            if(Resultados is null || Resultados.Count == 0)
            {
                Resultados = _repository.ReadResultados();
            }
        }
    }
}
