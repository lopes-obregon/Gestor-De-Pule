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
        public Caixa Caixa { get; private set; }

       public CaixaService(object data)
        {
            _repository = new CaixaRepository(data);
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

       
    }
}
