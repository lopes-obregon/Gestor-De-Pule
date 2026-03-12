using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Controllers
{
    internal class ViewController
    {
        private ViewService _viewService;
        
        public ViewController()
        {
            _viewService = new(new DataBase());
        }
        public ViewController(DataBase dataBase)
        {
            _viewService = new ViewService(dataBase);
        }
        /// <summary>
        /// Delega a busca para o serviço correspondente.
        /// Para realizar a busca do <see cref="Animal"/> Referente.
        /// </summary>
        /// <param name="animalId"> identificador unico do animal que pretende buscar</param>
        /// <returns>A entidade <see cref="Animal"/> ou null se não encontrar</returns>
        internal object? GetAnimalById(int animalId)
        {
            return _viewService.GetAnimalById(animalId);
        }

        internal List<Animal> GetAnimals(List<int> list)
        {
            return _viewService.GetAnimals(list);
        }

        /// <summary>
        /// Chama o serviço da view.
        /// </summary>
        /// <returns>A entidade <see cref="Caixa"/> que estiver em memória</returns>
        internal object GetCaixa()
        {
            return _viewService.GetCaixa();
        }

        /// <summary>
        /// Chama o view serviço para obter a disputa
        /// </summary>
        /// <returns>Disputa em memória breviamente carregada</returns>
        internal object? GetDisputa()
        {
            return _viewService.GetDisput();
        }

        internal string GetNomeAnimaisVencedores()
        {


            return _viewService.GetNomeAnimaisVencedores();
        }

        /// <summary>
        /// Retorna a lista de <see cref="Resultado"/> associada ao ID da rodada informado,
        /// delegando a busca ao serviço de visualização.
        /// </summary>
        /// <param name="id">Identificador único da rodada.</param>
        /// <returns>
        /// A lista de <see cref="Resultado"/> encontrada ou null se não houver registros.
        /// </returns>
        internal List<Resultado>? GetResultados(int id)
        {
            return _viewService.GetResultadosByidRodada(id);
        }
        /// <summary>
        /// Chamada para o serviço solicitar as disputas cadastradas
        /// </summary>
        /// <returns>Lista de <see cref="Disputa"/></returns>
        internal object? ListarDisputas()
        {
            return _viewService.GetDisputs();
        }

        /// <summary>
        /// Carrega a entidade <see cref="Caixa"/> através do serviço de visualização,
        /// inicializando ou atualizando os dados da caixa atual.
        /// </summary>
        internal void LoadCaixa()
        {
            _viewService.LoadCaixa();
        }
        /// <summary>
        /// Chama o serviço para carregar o que vai ser utilizado para calcular os prémios
        /// </summary>
        internal void LoadEntToPrice()
        {
            _viewService.LoadEntToPrice();
        }

        /// <summary>
        /// Carrega os resultados no serviço de visualização,
        /// inicializando ou atualizando o cache de <see cref="Resultado"/>.
        /// </summary>
        internal void LoadResultados()
        {
            _viewService.LoadResultados();
        }

        /// <summary>
        /// Delega o carregamento das rodadas para o service view
        /// </summary>
        internal void LoadRodadas() => _viewService.LoadRodadas();

        internal Rodada NovaRodada()
        {
            throw new NotImplementedException();
        }

        internal Resultado NovoResultado()
        {
            throw new NotImplementedException();
        }

        internal string SalvarDisputa()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Faz a chamada para o serviço.
        /// </summary>
        /// <param name="idDisputa"> id da disputa que pretende carregar</param>
        internal void SetDisputa(int idDisputa)
        {
            _viewService.SetDisputa(idDisputa);
        }
    }
}
