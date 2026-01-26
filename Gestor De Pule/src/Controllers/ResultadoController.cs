using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Controllers
{
    internal class ResultadoController
    {

        public ResultadoRepository ResultadoRepository { get; private set; }
        public ResultadoController(object data) { 
        
            ResultadoRepository = new ResultadoRepository(data);
        }
        public ResultadoController() { ResultadoRepository = new ResultadoRepository(); }
    }
}
