using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Models;
using System.Diagnostics.CodeAnalysis;
namespace Gestor_De_Pule.src.Controllers
{
    internal class CaixaController
    {
        private CaixaRepository _caixaRepository;
        public CaixaRepository GetCaixaRepository() {  return _caixaRepository; }
        public Caixa Caixa {  get;  private set; }

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
        public void LoadCaixa()
        {
            if (Caixa is null)
                Caixa = _caixaRepository.GetCaixa();
        }

        internal void LoadCaixaWithDisput(int id)
        {
            if (Caixa is null)
                Caixa = _caixaRepository.GetCaixaWithDisput(id);
        }
        /// <summary>
        /// Remove disput in caixa.Disputs
        /// </summary>
        /// <param name="id"></param>
        internal bool RemoveDisput(int id)
        {
            bool sucss = false;
            if(Caixa is not null)
            {
                var disputas = Caixa.Disputs;
                if(disputas is not null && disputas.Count > 0)
                {
                    foreach (var disput in disputas)
                    {
                        if(disput is not null && disput.Id == id)
                        {
                            disput.Caixa = null;
                            disputas.Remove(disput);
                            sucss = true;
                            if (disputas.Count == 0)
                                break;
                        }
                    }
                }
            }
            return sucss;
        }
    }
}
