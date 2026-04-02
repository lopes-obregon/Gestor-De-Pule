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
        /// Utilizado para acessar, manipular e gerenciar Animais no contexto da aplicação.
        /// </summary>
        private AnimalService _animalService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Caixa"/>.
        /// Utilizado para acessar, manipular e gerenciar Caixa no contexto da aplicação.
        /// </summary>
        private CaixaService _caixaService;
        /// <summary>
        /// Serviço responsável por operações relacionadas à entidade <see cref="Pule"/>.
        /// Utilizado para acessar, manipular e gerenciar Pule no contexto da aplicação.
        /// </summary>
        private PuleService _puleService;
        /// <summary>
        /// Serviço Responsável por operações relacionadas à entidade <see cref="Rodada"/>.
        /// Utilizado para acessar, manipular e gerenciar Rodada no contexto da aplicação.
        /// </summary>
        private RodadaService _rodadaService;
        /// <summary>
        /// Repository geral para salvar alterações de contextos
        /// </summary>
        private Repository _repository;
        public ViewService(DataBase dataBase)
        {
            
            _disputaService = new(dataBase);
            _resultadoService = new(dataBase);
            _animalService = new AnimalService(dataBase);
            _caixaService = new CaixaService(dataBase);
            _puleService = new PuleService(dataBase);
            _rodadaService = new RodadaService(dataBase);
            _repository = new Repository(dataBase);
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

        internal string GetTotalGanhadoresPulesPorRodada()
        {
            string mensagem = String.Empty;
            var disputa = _disputaService.Disputa;
            if(disputa is not null)
            {
                mensagem = disputa.GetTotalGanhadoresPorPulesEmRodadas();
            }
            return mensagem;
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
                    _puleService.LoadPulesWithAnimalsInDisputs(disputa.Id);
                if (_animalService.Animals is null)
                    _animalService.LoadAnimaisWithPules();
                if(_rodadaService.Rodadas is null)
                    _rodadaService.LoadRodadasWithResultadosByIdDisputa(disputa.Id);
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

        internal bool NovaRodada()
        {
            var rodada = _rodadaService.NewRodada();
            var disputa = _disputaService.Disputa;
            bool sucess = false;
            byte nRodada;
            if (disputa != null)
            {
                var animais = _animalService.GetAnimalsWithDisputaId(disputa.Id);
                if (animais != null)
                {
                    foreach (var animal in animais)
                    {
                        if (animal is not null)
                        {
                            var resulado = _resultadoService.NovoResultado();
                            animal.Resultados.Add(resulado);
                            if (rodada.ResultadoDestaRodada is null)
                                rodada.ResultadoDestaRodada = new();
                            rodada.ResultadoDestaRodada.Add(resulado);
                            resulado.Disputa = disputa;
                            _resultadoService.Resultados?.Add(resulado);

                        }
                    }
                    nRodada = (byte)disputa.GetNMaiorRodada();
                    rodada.Nrodadas = ++nRodada;
                    var ultimoPules = disputa.GetLastPulesRodadas();
                    if(ultimoPules != null)
                    {
                        foreach(var pule in ultimoPules)
                        {
                            //criar novo pule com mesmo dados
                            if(pule is not null)
                            {
                                var novoPule = _puleService.NovoPule(pule);
                                if(novoPule != null)
                                {
                                    if (rodada.PulesDestaRodada is null)
                                        rodada.PulesDestaRodada = new();
                                    rodada.PulesDestaRodada.Add(novoPule);//adiciona o novo pule para a lista de rodadas.
                                }

                            }
                        }
                    }
                    //rodada.PulesDestaRodada = ultimoPules;
                    disputa.Rodadas?.Add(rodada);
                    _rodadaService.Rodadas?.Add(rodada);
                }
                sucess = _repository.Save();
                
            }
            return sucess;
        }

        /// <summary>
        /// Se tem uma disputa valida chama o procedimento que calcula o valor para ser pago por pule
        /// </summary>
        /// <returns>O valor que deve ser pago por pule</returns>
        internal string PagamentoPorPule()
        {
            var disputa = _disputaService.Disputa;
            var caixa = _caixaService.Caixa;
            string mensagem = String.Empty;
            int quantidadeDePulesVencedores;
            decimal totalArrecadado = 0.0m;
            decimal valorTaxa = 0.0m;
            decimal prêmioLiquido = 0;
            if (disputa is not null)
            {
                //mensagem =disputa.PagamentoPorPule();
                try
                {
                    totalArrecadado = _puleService.Pules.Where(p => p.DisputaId == disputa.Id).Sum(p => p.Valor);
                    quantidadeDePulesVencedores = disputa.QuantidadeDePulesVencedoresTotal();

                }
                catch (ArgumentNullException)
                {
                    _puleService.LoadPulesWithDisputaById(disputa.Id);
                    totalArrecadado = _puleService.Pules.Where(p => p.DisputaId == disputa.Id).Sum(p => p.Valor);
                    quantidadeDePulesVencedores = disputa.QuantidadeDePulesVencedoresTotal();
                }
                if(caixa is not null)
                {
                    valorTaxa = totalArrecadado * caixa.Taxa;
                    prêmioLiquido = totalArrecadado - valorTaxa;
                   mensagem =  (prêmioLiquido / quantidadeDePulesVencedores).ToString("C");
                }
            }
            return mensagem;
        }
        /// <summary>
        /// Delegate repository save
        /// </summary>
        /// <returns>true if saved the context or false if the context not saved.</returns>
        internal bool Save()
        {
            return _repository.Save();
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
        /// <summary>
        /// Updates the time values in the current round's results based on the data from the specified DataGridView.
        /// </summary>
        /// <param name="grid">The DataGridView containing animal names and time values.</param>
        /// <param name="index">The one-based index of the round to update.</param>
        internal void SetTimeInResultado(DataGridView grid, int index)
        {
            Disputa? disputa = _disputaService.Disputa;
            Rodada rodada;
            string nomeAnimal;
            TimeSpan tempo;
            int rowIndex = 0;
            if (disputa != null && disputa.Rodadas?.Count > 0)
            {
                rodada = disputa.Rodadas[index - 1];//primeira roada
                if (rodada.ResultadoDestaRodada?.Count > 0)
                {
                    foreach (Resultado resultado in rodada.ResultadoDestaRodada)
                    {
                        nomeAnimal = grid.Rows[rowIndex].Cells[0]?.Value?.ToString() ?? String.Empty;
                        var tempoGrid = grid.Rows[rowIndex].Cells[2]?.Value?.ToString()?.Replace(',', '.');
                        if(tempoGrid is not null)
                        {
                            tempo = TimeSpan.Parse(tempoGrid.ToString());
                        }
                        else
                            tempo = TimeSpan.Zero;
                        //tempo = TimeSpan.ParseExact(grid.Rows[rowIndex].Cells[2]?.Value?.ToString() ?? "00:00:00,00", @"hh\:mm\:ss\,ff", null);
                        if (!String.IsNullOrEmpty(nomeAnimal))
                        {
                            if (resultado.Animal?.Nome == nomeAnimal)
                            {
                                if (tempo != resultado.Tempo)
                                    resultado.Tempo = tempo;
                            }
                        }
                        rowIndex++;
                    }

                }
            }
        }
    }
}
