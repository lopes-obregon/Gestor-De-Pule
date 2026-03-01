using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class ResultadoService
    {
        private ResultadoRepository _repository;
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
            List<Resultado> resultados = new List<Resultado>();
            var db = _repository.ReadResultadosByidRodada(idRodada);
            if (db != null)
            {
                resultados = db;
            }
            return resultados;
        }
    }
}
