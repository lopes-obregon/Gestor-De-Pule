
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Controllers
{
     internal  class FinanceiroController
    {
        public   Caixa? Caixa {  set; get; } = null;
        private CaixaRepository _caixaRepository;
        public FinanceiroController() 
        {
            _caixaRepository = new CaixaRepository();
        }
        /// <summary>
        /// Init caixa in instance
        /// </summary>
        internal  void InitCaixa()
        {
           if(Caixa is null)
            {
                var caixaDb = _caixaRepository.GetCaixa();
                if (caixaDb is not null)
                    Caixa = (Caixa?)caixaDb;
            }

        }
        /// <summary>
        /// carrega no caixa o primeiro caixa aberto;
        /// </summary>
        internal  void LoadCaixaInit()
        {
            if (Caixa is null)
                Caixa = _caixaRepository.LoadInit();
        }
        
       

        internal  void SaveOrAttTaxa(decimal taxa)
        {
            if(Caixa is not null && Caixa.Taxa != taxa){
                Caixa.Taxa = taxa;
                //Caixa.save();
                _caixaRepository.Save(Caixa);
            }
        }

        internal  void OpenNewCaixa()
        {
            Caixa caixa = new Caixa();
            caixa.Open = Caixa.IsOpen.Open;
            caixa.DateOpen = DateTime.Now;
            //caixa.save();
            _caixaRepository.Save(caixa);

            Caixa = caixa;
        }

        
    }
}
