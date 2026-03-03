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
            Caixa = _repository.GetCaixa();
            return Caixa;
        }
    }
}
