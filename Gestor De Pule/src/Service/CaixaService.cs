using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class CaixaService
    {
        /// <summary>
        /// Repository or db
        /// </summary>
        private  readonly CaixaRepository _repository;
        /// <summary>
        /// Cache in the Caixa
        /// </summary>
        /// 
        public Caixa Caixa { get; private set; }
        private readonly  DisputaService _disputaService;
        private readonly PuleService _puleService;
       public CaixaService(object data)
        {
            _repository = new CaixaRepository(data);
            _disputaService = new(data);
            _puleService = new(data);
        }


        /// <summary>
        /// Verica se existe algum caixa aberto
        /// </summary>
        /// <returns>Caixa encontrado ou null se não existir caixa</returns>

        internal Caixa? GetCaixa()
        {
            if (Caixa == null)
            {
                Caixa = _repository.GetCaixa();
            }
            return Caixa;
        }
        /// <summary>
        /// Obtem o caixa com as disputas relacionadas.
        /// </summary>
        /// <param name="id"></param>
        internal void GetCaixaWithDisput(int id)
        {
            if (Caixa is null)
                Caixa = _repository.GetCaixaWithDisput(id);
        }
        /// <summary>
        /// Retorna uma entidade <see cref="Caixa"/> pelo ID informado.
        /// Se já existir uma instância em memória (propriedade Caixa) com o mesmo ID,
        /// utiliza essa instância. Caso contrário, consulta o repositório para buscar
        /// a caixa correspondente e atualiza a propriedade Caixa.
        /// </summary>
        /// <param name="caixaId">Identificador único da caixa a ser buscada.</param>
        /// <returns>
        /// A entidade <see cref="Caixa"/> encontrada ou null se não existir.
        /// </returns>

        internal Caixa? GetCaixaById(int caixaId)
        {
            Caixa? caixa = null;
            if (Caixa is not null)
                if (Caixa.Id == caixaId)
                    caixa = Caixa;
                else
                    caixa = _repository.GetCaixaById(caixaId);
                
            else 
                caixa = _repository.GetCaixaById(caixaId);
            Caixa = caixa;
            return caixa;
        }
        /// <summary>
        /// check if variable Caixa is null
        /// </summary>
        /// <returns> true or false</returns>
        internal bool CaixaIsNull()
        {
            if (Caixa is null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// if <see cref="Caixa"/> not null delegate service to loads disputs.
        /// </summary>
        internal void LoadDisputs()
        {
            if(Caixa is not null && (Caixa.Disputs is null || Caixa.Disputs.Count == 0))
            {
                _disputaService.LoadDisputsByCaixaId(Caixa.Id);
            }
        }
        /// <summary>
        /// Retrieves the total cash value formatted as currency.
        /// </summary>
        /// <returns>Returns an empty string if the Box object is not defined.</returns>
        internal string GetTotalEmCaixa()
        {
            string mensagem = String.Empty;
           if(Caixa is not null)
            {
                mensagem =  Caixa.TotalEmCaixa.ToString("C");
            }
           return mensagem;
        }

        internal string GetEntradaDeApostas()
        {
            string total = String.Empty;
            if(Caixa is not null)
            {
                if(Caixa.Disputs is not null && Caixa.Disputs.Any(dis=> dis.Pules == null))
                {
                    var disputsId = Caixa.Disputs.Select(dis=> dis.Id).ToList();// projeta uma lista com os ids da disputa
                    if(disputsId.Count > 0)
                    {
                        _puleService.LoadPulesAssociedDisputa(disputsId);
                    }
                }
              total =  Caixa.GetEntradaDeApostas().ToString("C");
            }
            return total;
        }
        /// <summary>
        /// Get the prize amount to be paid, formatted as currency.
        /// </summary>
        /// <returns>
        /// A string representing the prize value in monetary format,
        /// or an empty string if there is no <see cref="Caixa"/>
        /// </returns>
        internal string PrêmioParaPagar()
        {
            string total = String.Empty;
            if(Caixa is not null)
            {
                total = Caixa.GetPremioApagar().ToString("C");
            }
            return total;
        }
    }
}
