using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Controllers
{
    internal class ResultadoController
    {

        public ResultadoRepository ResultadoRepository { get; private set; }
        public Resultado? Resultado { get; private set; }
        public List<Resultado> Resultados { get; set; }
        public ResultadoController(object data) { 
        
            ResultadoRepository = new ResultadoRepository(data);
            Resultados = new List<Resultado>();
        }
        public ResultadoController() { ResultadoRepository = new ResultadoRepository();}

        internal void NovoResultado()
        {
            Resultado = new Resultado();
            ResultadoRepository.AddContext(Resultado);
            Resultados.Add(Resultado);
            
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
    }
}
