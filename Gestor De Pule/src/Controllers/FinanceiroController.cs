
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
      /// delegate <see cref="_caixaService"/> to set 'taxa'
      /// </summary>
      /// <param name="taxa"> A value of rate</param>
        internal  void SaveOrAttTaxa(decimal taxa)
        {
            /*if(Caixa is not null && Caixa.Taxa != taxa){
                Caixa.Taxa = taxa;
                //Caixa.save();
                _caixaRepository.Save();
               // _caixaRepository.Save(Caixa);
            }*/
            _caixaService.SetTaxa(taxa);
            _caixaService.Save();
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
        /// <summary>
        /// Delegate to <see cref="_caixaService"/> call Lucro function.
        /// </summary>
        /// <returns>A string with result operation</returns>
        internal string Lucro()
        {
            return _caixaService.Lucro();
        }
        /// <summary>
        /// in <see cref="_caixaService"/> call 'Caixa' and property 'Taxa' and return value.
        /// </summary>
        /// <returns>A string with Rate value in porcentual format</returns>
        internal string GetTaxa()
        {
            return _caixaService.GetTaxa();
        }
        /// <summary>
        /// Delegate close cash register.
        /// </summary>
        /// <returns>
        /// A string with a message that have date closed cash register.
        /// </returns>
        internal string FecharDia()
        {
            return _caixaService.FecharDia();
        }
        /// <summary>
        /// Delegate to call the correct method.
        /// </summary>
        /// <returns>A List <see cref="Disputa"/> with disputs not pay.</returns>
        internal IEnumerable<object> GetDisputasNãoPagas()
        {
            return _caixaService.GetDisputaNãoPagas();
        }
        /// <summary>
        /// Initial Disputs Component.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal void InitDisputs()
        {
            _caixaService.LoadDisputs();
        }
       /// <summary>
       /// Initial components required.
       /// </summary>
        internal void Init()
        {
            InitCaixa();
            InitDisputs();
        }
        /// <summary>
        /// To delegate to the 'service' the process of withdrawing the profit margin.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>
        /// True case success or false case no
        /// </returns>
        internal bool RetirarLucro(decimal valor)
        {
            return _caixaService.RetirarLucro(valor);
        }
        /// <summary>
        /// Processes payment for a selected dispute and returns a status message.
        /// </summary>
        /// <param name="disputaSelecionadaUi">The selected dispute to be paid.</param>
        /// <param name="value">The payment amount.</param>
        /// <returns>A message indicating the result of the payment operation.</returns>
        internal string PayDispute(object disputaSelecionadaUi, decimal value)
        {
            string message = String.Empty;
            message = _caixaService.PayDispute(disputaSelecionadaUi, value);
            return message;
        }
    }
}
