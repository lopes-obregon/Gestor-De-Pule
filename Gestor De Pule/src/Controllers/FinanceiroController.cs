
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;

namespace Gestor_De_Pule.src.Controllers
{
     internal  class FinanceiroController
    {
        public   Caixa? Caixa {  set; get; } = null;
        private CaixaRepository _caixaRepository;
        private Repository _repository;
        /// <summary>
        /// Service <see cref="Caixa"/>
        /// </summary>
        private CaixaService _caixaService;
        public FinanceiroController() 
        {
            _repository = new Repository();
            var db = _repository.GetDataBase();
            _caixaRepository = new CaixaRepository(db);
            //Caixa = _caixaRepository.GetCaixa();
            _caixaService = new CaixaService(db);
        }
        /// <summary>
        /// Delegate init caixa
        /// </summary>
        internal  void InitCaixa()
        {
            _caixaService.GetCaixa();
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="taxa"></param>
        internal  void SaveOrAttTaxa(decimal taxa)
        {
            if(Caixa is not null && Caixa.Taxa != taxa){
                Caixa.Taxa = taxa;
                //Caixa.save();
                _caixaRepository.Save();
               // _caixaRepository.Save(Caixa);
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
        /// <summary>
        /// 
        /// </summary>
        internal void Dispose()
        {
            _repository.GetDataBase().Dispose();
        }
        /// <summary>
        /// Delegate to <see cref="_caixaService"/> to check
        /// </summary>
        /// <returns>true if <see cref="Caixa"/> null or false if <see cref="Caixa"/> not null</returns>
        internal bool CaixaIsNull()
        {
            bool result =  _caixaService.CaixaIsNull();
            return result;
        }
        /// <summary>
        /// Delegate load <see cref="CaixaService"/> to call loads 'disputs'
        /// </summary>
        internal void LoadDisputs()
        {
            _caixaService.LoadDisputs();
        }
        /// <summary>
        /// Delegate to <see cref="_caixaService"/> to get 'totalEmCaixa'.
        /// </summary>
        /// <returns>A string with a value coin.</returns>
        internal string GetTotalEmCaixa()
        {
            return _caixaService.GetTotalEmCaixa();
        }

        internal string GetEntradaDeApostas()
        {
            return _caixaService.GetEntradaDeApostas();
        }
        /// <summary>
        /// Delegate service to get prize
        /// </summary>
        /// <returns>A string with prize value</returns>
        internal string PremioApagar()
        {
            return _caixaService.PrêmioParaPagar();
        }
    }
}
