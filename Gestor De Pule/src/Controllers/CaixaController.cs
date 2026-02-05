using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Models;
namespace Gestor_De_Pule.src.Controllers
{
    internal class CaixaController
    {
        private CaixaRepository _caixaRepository;
        public CaixaRepository GetCaixaRepository() {  return _caixaRepository; }

        internal void NovoCaixa()
        {
            Caixa caixa = new Caixa();
            caixa.Open = Caixa.IsOpen.Open;
            caixa.Taxa = 0.01m;
            caixa.DateOpen = DateTime.Now;
            _caixaRepository.Save(caixa);
        }

        public CaixaController(object data)
        {
            _caixaRepository = new CaixaRepository(data);
        }
        public CaixaController() { 
            _caixaRepository = new CaixaRepository();
        
        }
    }
}
