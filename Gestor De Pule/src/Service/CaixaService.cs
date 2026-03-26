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
            (decimal, decimal) resultado;
            if(Caixa is not null)
            {
                resultado = Caixa.GetPremioApagar();
                total = resultado.Item1.ToString("C");
            }
            return total;
        }
        /// <summary>
        /// if <see cref="Caixa"/> not null call Lucro funciton.
        /// </summary>
        /// <returns> A string  with value of the profit </returns>
        internal string Lucro()
        {
            string lucro = String.Empty;
            if(Caixa is not null)
            {
                lucro = Caixa.Lucro().ToString("C");
            }
            return lucro;
        }
        /// <summary>
        /// Set <see cref="Caixa.Taxa"/> in the <see cref="Caixa"/>
        /// </summary>
        /// <param name="taxa"></param>
        internal void SetTaxa(decimal taxa)
        {
            if (Caixa is not null && Caixa.Taxa != taxa)
            {
                Caixa.Taxa = taxa;
            }

        }
        /// <summary>
        /// Save context
        /// </summary>
        internal void Save()
        {

            _repository.Save();
        }
        /// <summary>
        /// close  register cash
        /// </summary>
        /// <returns>A string with message with date close register cash</returns>
        internal string FecharDia()
        {
            string fechar = String.Empty;
            bool fechou = false;
            if (Caixa is not null)
            {
                Caixa.FecharDia();
                fechou = _repository.Save();
                if (fechou)
                {
                    fechar = "Caixa Fechado com sucesso na Data: " + DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                   fechar = "Erro ao fechar o caixa!";
                }
            }
            else
            {
                fechar = "Desculpe mas Algo deu errado!";
            }
                return fechar;
        }
        /// <summary>
        /// Seach disputs not pay.
        /// </summary>
        /// <returns>A <see cref="List"/> with Disputs not pay</returns>
        internal List<Disputa> GetDisputaNãoPagas()
        {
            List<Disputa> disputas = new List<Disputa>();
            if(Caixa is not null)
                disputas = Caixa.DisputsNãoPagos();
            return disputas;
        }
        /// <summary>
        /// Get taxa from the cash register
        /// </summary>
        /// <returns>A string with value or '-'</returns>
        internal string GetTaxa()
        {
            if (Caixa is not null)
                return Caixa.Taxa.ToString("P");
            else
                return "-";
        }
        /// <summary>
        /// try to remove a profit value by object 'caixa'.
        /// if 'caixa' is defined delegate operation to him.
        /// Caso contrário, retorna falso.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>
        /// if the value remove with success return true.
        /// else return false.
        /// </returns>
        internal bool RetirarLucro(decimal valor)
        {
            if(Caixa is not null)
                return Caixa.RetirarLucro(valor);
            else return false;
        }
        /// <summary>
        /// Processes payment for the selected dispute and returns a status message.
        /// </summary>
        /// <param name="disputaSelecionadaUi">The selected dispute object from the UI.</param>
        /// <param name="value">The payment amount to be processed.</param>
        /// <returns>A message indicating the result of the payment operation.</returns>
        internal string PayDispute(object disputaSelecionadaUi, decimal value)
        {
            var dispute = _disputaService.Disputa;
            Disputa? selectedDispute = disputaSelecionadaUi as Disputa;
           string message = String.Empty;
            if(dispute is not null)
            {
                if (selectedDispute is not null)
                {
                    if (selectedDispute.Id != dispute.Id)
                    {
                        dispute = _disputaService.SelectDispute(selectedDispute.Id);
                    }
                    if (Caixa is not null && dispute is not null)
                    {
                        message = Caixa.PagaDisputa(dispute.Id, value);

                    }
                }
                    
            }
            return message;
        }
    }
}
