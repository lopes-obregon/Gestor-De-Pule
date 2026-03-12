using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Service
{
    internal class ViewService
    {
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Disputa"/>.
        /// Utilizado para acessar, manipular e gerenciar disputas no contexto da aplicação.
        /// </summary>
        private DisputaService _disputaService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Resultado"/>.
        /// Utilizado para acessar, manipular e gerenciar disputas no contexto da aplicação.
        /// </summary>
        private ResultadoService _resultadoService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Animal"/>.
        /// Utilizado para acessar, manipular e gerenciar disputas no contexto da aplicação.
        /// </summary>
        private AnimalService _animalService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Caixa"/>.
        /// Utilizado para acessar, manipular e gerenciar disputas no contexto da aplicação.
        /// </summary>
        private CaixaService _caixaService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Pule"/>.
        /// Utilizado para acessar, manipular e gerenciar disputas no contexto da aplicação.
        /// </summary>
        private PuleService _puleService;
        public ViewService(DataBase dataBase)
        {
            
            _disputaService = new(dataBase);
            _resultadoService = new(dataBase);
            _animalService = new AnimalService(dataBase);
            _caixaService = new CaixaService(dataBase);
            _puleService = new PuleService(dataBase);
        }
        /// <summary>
        /// Retorna uma entidade <see cref="Animal"/> pelo ID informado,
        /// delegando a busca ao serviço de animais.
        /// </summary>
        /// <param name="animalId">Identificador único do animal a ser buscado.</param>
        /// <returns>
        /// A entidade <see cref="Animal"/> encontrada ou null se não existir.
        /// </returns>
        internal object? GetAnimalById(int animalId)
        {
            return _animalService.GetAnimalById(animalId);
        }

        internal List<Animal>? GetAnimals(List<int> list)
        {
            return _animalService.GetAnimalsByIdList(list);
        }

        /// <summary>
        /// Delega o serviço para caixa service para obter uma intancia do tipo <see cref="Caixa"/>
        /// </summary>
        /// <returns> A entidade <see cref="Caixa"/> ou cria uma nova se não existir</returns>
        internal object GetCaixa()
        {
            return _caixaService.Caixa;
        }

        /// <summary>
        /// Delega para disputa  obter a disputa de memória
        /// </summary>
        /// <returns>A disputa breviamente carregada</returns>
        internal object? GetDisput() => _disputaService.Disputa;
        /// <summary>
        /// Delega para o serviço de disputa a busca das disputas cadastradas
        /// </summary>
        /// <returns>As entidades cadastradas do tipo <see cref="Disputa"/></returns>
        internal object? GetDisputs()
        {
            return _disputaService.GetDisputs();
        }

        internal string GetNomeAnimaisVencedores()
        {
            string mensagem = String.Empty;
            var disputa = _disputaService.Disputa;
            var rodadas = _disputaService.Disputa?.Rodadas;
            var animais = _animalService.Animals;
            mensagem = "Vencedor ";
            if (disputa != null)
            {
                if(rodadas != null && animais is not null)
                {
                    foreach (var rodada in rodadas)
                    {
                        if (rodada is not null && rodada.ResultadoDestaRodada is not null)
                        {
                            var resultado = rodada.ResultadoDestaRodada.FirstOrDefault(r => r.Posição == 1);
                            if (resultado is not null)
                            {
                                var animal = animais.FirstOrDefault(a => a.Id == resultado.AnimalId);
                                if (animal is not null)
                                {
                                    mensagem += $"Rodada {rodada.Nrodadas}: {animal.Nome}. ";

                                }
                            }

                        }
                    }
                }
            }
            return mensagem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal List<Resultado>? GetResultadosByidRodada(int id)
        {
            return _resultadoService.GetResultadosByidRodada(id);
        }
        /// <summary>
        /// Carrega a entidade <see cref="Caixa"/> através do serviço de caixa,
        /// inicializando ou atualizando os dados da caixa atual.
        /// </summary>
        internal void LoadCaixa()
        {
            _caixaService.GetCaixa();
        }
        /// <summary>
        /// Pré load do que vai precisar para calcular os premios
        /// </summary>
        internal void LoadEntToPrice()
        {
            var disputa = _disputaService.Disputa;
            if (disputa != null)
            {
                if (_caixaService.Caixa is null)
                    _caixaService.GetCaixa();
                if (_puleService.Pules is null)
                    _puleService.LoadPulesWithAnimals(disputa.Id);
                if (_animalService.Animals is null)
                    _animalService.LoadAnimaisWithPules();
            }
        }

        /// <summary>
        /// Delega ao resultado
        /// </summary>
        internal void LoadResultados()
        {
            _resultadoService.LoadResultados();
        }

        /// <summary>
        /// Busca as rodadas da disputa
        /// Delega a  busca da entidade ao serviço de disputa.
        /// </summary>
        internal void LoadRodadas()
        {
            _disputaService.LoadRodadas();
        }

        /// <summary>
        /// Define a disputa atual com base no ID informado,
        /// delegando a busca da entidade ao serviço de disputa.
        /// </summary>
        /// <param name="idDisputa">Identificador único da disputa a ser definida.</param>
        internal void SetDisputa(int idDisputa)
        {
            _disputaService.GetById(idDisputa);
        }
    }
}
