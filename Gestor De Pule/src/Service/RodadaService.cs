using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Service
{
    internal class RodadaService
    {
        private RodadaRepository _repository;
        public RodadaService(object data)
        {
            _repository = new RodadaRepository(data);
        }

       

        /// <summary>
        /// Pesquisa pelo id a rodada
        /// </summary>
        /// <param name="rodadaId"></param>
        /// <returns>Return null se não encontrar a rodada ou a rodada pesquisada se encontrar</returns>
        internal Rodada? GetById(int? rodadaId)
        {
            Rodada? rodada;
            if (rodadaId != null) {
                rodada = _repository.GetById(rodadaId);



            }
            else
            {
                rodada = null;
            }
            return rodada;
        }

       
    }
}
