using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Models;
using System.Diagnostics.CodeAnalysis;
using Gestor_De_Pule.src.Service;
namespace Gestor_De_Pule.src.Controllers
{
    internal class CaixaController

    {
        /// <summary>
        /// repository de cauxa
        /// </summary>
        private CaixaRepository _caixaRepository;
        /// <summary>
        /// método que chama o caixa repository
        /// </summary>
        /// <returns>intancia de caixaRepository</returns>
        public CaixaRepository GetCaixaRepository() {  return _caixaRepository; }
        /// <summary>
        /// Propriedade que retorna o chache de caixa
        /// </summary>
        /// 
        public Caixa Caixa => _caixaService.Caixa;

        private CaixaService _caixaService;
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
            _caixaService = new CaixaService(data);
        }
       
        /// <summary>
        /// faz a chamada para Carrega no cache o caixa existente
        /// </summary>
        public void LoadCaixa()
        {

            _caixaService.GetCaixa();
                    //Caixa = _caixaRepository.GetCaixa();
        }
        /// <summary>
        /// faz a chamada para carregar em cache o caixa com as disputas relacionadas.
        /// </summary>
        /// <param name="id"></param>
        internal void LoadCaixaWithDisput(int id)
        {
            if (Caixa is null)
                _caixaService.GetCaixaWithDisput(id);
                    //_caixaRepository.GetCaixaWithDisput(id);
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



        /// <summary>
        /// Retorna uma entidade <see cref="Caixa"/> pelo ID informado,
        /// delegando a busca ao serviço de caixa.
        /// </summary>
        /// <param name="caixaId">Identificador único da caixa a ser buscada.</param>
        /// <returns>A entidade <see cref="Caixa"/> encontrada ou null se não existir.</returns>
        internal Caixa? GetCaixaById(int caixaId) => _caixaService.GetCaixaById(caixaId);
    }
}
