using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;

namespace Gestor_De_Pule.src.Controllers
{
    internal class ResultadoController
    {

        public ResultadoRepository ResultadoRepository { get; private set; }
        public Resultado? Resultado { get; private set; }
        public List<Resultado> Resultados { get; set; }
        private ResultadoService _resultadoService {  get; set; }
        public ResultadoController(object data) { 
        
            ResultadoRepository = new ResultadoRepository(data);
            _resultadoService = new(data);
            Resultados = new List<Resultado>();
        }
        public ResultadoController() { ResultadoRepository = new ResultadoRepository();}

        internal Resultado NovoResultado()
        {
            Resultado = new Resultado();
            ResultadoRepository.AddContext(Resultado);
            Resultados.Add(Resultado);
            return Resultado;
            
        }

        internal void LoadResultados()
        {
            if (Resultados is null || Resultados.Count == 0)
            {
               // Resultados = new List<Resultado>();
                Resultados = ResultadoRepository.ReadResultados();
            }
            
        }
        /// <summary>
        /// Dispose context
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void Clear()
        {
            ResultadoRepository.Clear();
        }

        internal Resultado NovoResultado(Disputa disputa, Animal animal)
        {
            Resultado = new(disputa, animal);
            ResultadoRepository.AddContext(Resultado);
            return Resultado;
        }
        /// <summary>
        /// Procedimento que seta em Resultado a nova instancia
        /// </summary>
        /// <param name="disputa"></param>
        /// <param name="animal"></param>
        /// <param name="rodada"></param>
        /// <returns>Object Resultado</returns>
        internal Resultado NovoResultado(Disputa disputa, Animal animal, Rodada rodada)
        {
            Resultado = new(disputa, animal, rodada);
            ResultadoRepository.AddContext(Resultado);
            return Resultado;
        }
        /// <summary>
        /// Obtem os resultados do cache de Resultados ou consulta no banco
        /// </summary>
        /// <param name="idRodada"></param>
        /// <returns>Retorna os resultados encontrados </returns>
        internal List<Resultado>? GetResultados(int idRodada)
        {
            List<Resultado> resultados;
            if(Resultados != null)
            {
                resultados = Resultados.Where(res=> res.RodadaId == idRodada).ToList();
            }
            else
            {
                resultados = _resultadoService.GetResultadosByidRodada(idRodada);
            }
            return resultados;
        }
    }
}
