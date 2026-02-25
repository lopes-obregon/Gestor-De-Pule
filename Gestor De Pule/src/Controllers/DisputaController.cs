using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Service;
using Microsoft.EntityFrameworkCore;
namespace Gestor_De_Pule.src.Controllers
{
    class DisputaController
    {
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public Disputa? Disputa { get; private set; } = null;
        public List<Disputa> Disputas { get; private set; } = new List<Disputa>();
        public List<Animal> AnimaisRemovidos { get; private set; } = new List<Animal>();
        public Disputa DisputaLocal { get; private set; }
        public DisputaRepository? DisputaRepository { get; private set; }
        //private CaixaRepository _caixaRepository;
        //private RodadaRepository _rodadaRepository;
        private Repository _repository;
        //controllers externos
        
        private AnimalController _animalController;
        public RodadaController RodadaController { get; private set; }
        private CaixaController _caixaController;
        public ResultadoController ResultadoController { get; }
        private PuleController _puleController;
        /// <summary>
        /// Initializes a new instance of the DisputaCadastrosController class.
        /// </summary>
        public DisputaController(DataBase data)
        {
            DisputaRepository = new DisputaRepository(data);
            // _resultadoRepository = new ResultadoRepository(data);
            _animalController = new AnimalController(data);
            _caixaController = new CaixaController(data);
            RodadaController = new RodadaController(data);
            ResultadoController = new ResultadoController(data);

        }
        public DisputaController()
        {
            _repository = new Repository();

            //Disputas = Disputa.GetDisputasLocal();
            DisputaRepository = new DisputaRepository(_repository.GetDataBase());
            Disputas = DisputaRepository.GetDisputas();

            RodadaController = new RodadaController(_repository.GetDataBase());
            _animalController = new AnimalController(_repository.GetDataBase());
            ResultadoController = new ResultadoController(_repository.GetDataBase());
            _caixaController = new CaixaController(_repository.GetDataBase());
            _puleController = new PuleController(_repository.GetDataBase());

        }
        public DisputaController(bool isNew)
        {
            _repository = new Repository();
            
            DisputaRepository = new DisputaRepository(_repository.GetDataBase());
        }
        internal void AddAnimalRemovido(object animalSelecionadoUi)
        {
            if (animalSelecionadoUi is Animal)
            {
                var animalSelecionado = animalSelecionadoUi as Animal;
                if (animalSelecionado is not null)
                    AnimaisRemovidos.Add(animalSelecionado);
            }

        }

        internal string AtualizarDados(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items, int quantidadeRodadas)
        {
            bool sucess = false;
            int i = 0;
            List<Animal>? animaisSelecionadosUi = items.Cast<Animal>().ToList();
            List<Animal>? animaisSelecionados = null;
            RodadaController.Track();
            //constando que a rodada ja foi rastreada
            var rodada = RodadaController.Rodada;
            if (Animals is null)
            {
                if (_animalController is not null)
                {
                    _animalController.LoadAnimais();
                    Animals = _animalController.Animals;
                }
                else
                { return "Erro Ao Carregar animais!"; }
            }


            if (Disputa is not null)
            {
                Disputa.Nome = nomeDisputa;
                if (date != null)
                    Disputa.DataEHora = date ?? DateTime.Now;
                else
                    Disputa.DataEHora = DateTime.Now;
                if (animaisSelecionadosUi.Count > 0)
                {
                    var idsAnimalSelecionadoUi = animaisSelecionadosUi.Select(a => a.Id).ToList();
                    animaisSelecionados = Animals.Where(a => idsAnimalSelecionadoUi.Contains(a.Id)).ToList();
                    //percorrer os a lista de resultado para ver se tem algum animal novo caso tenha deve ser adicionado.
                    var animaisSemDisputa = animaisSelecionados.Where(an => !an.Resultados.Any(res => res.Disputa?.Id == Disputa.Id)).ToList();
                    //pego esses animais sem disputa e crio seus resultado para esse disputa
                    if (Disputa.Rodadas is null)
                        Disputa.Rodadas = DisputaRepository.LoadRodadas(Disputa).ToList();
                    while(i < quantidadeRodadas)
                    {
                        var rodadaDisputa = Disputa.Rodadas[i];
                        if(rodadaDisputa is not null)
                        {
                            if(animaisSemDisputa is not null)
                            {
                                rodadaDisputa.ResultadoDestaRodada.RemoveAll(res=> !animaisSemDisputa.Any(a=>a.Id ==  res.Animal.Id));
                            }
                        }
                        foreach (var animalSemDisputa in animaisSemDisputa)
                        {
                            if (animalSemDisputa != null)
                            {
                                var animal = _animalController.IsTracked(animalSemDisputa);
                                if (animal != null)
                                {
                                    //criamos o resultado para essa disputa
                                    ResultadoController.NovoResultado();
                                    var resultado = ResultadoController.Resultado;
                                    if (resultado is not null)
                                    {
                                        animal.Associete(resultado);
                                        if (resultado.Disputa == null)
                                            resultado.Disputa = Disputa;
                                        // Disputa.ResultadoList?.Add(resultado);
                                        rodadaDisputa.ResultadoDestaRodada.Add(resultado);
                                    }

                                }
                            }

                        }
                        i++;
                    }

                }
             

                //sucess = DisputaRepository.UpdateDisputa(Disputa, animaisSelecionadosUi, AnimaisRemovidos);
                sucess = DisputaRepository.Save();
                if (sucess) return "Disputa Atualizado com sucesso!";
                else return "Erro ao Atualizar a Disputa!";

            }
            return "Erro Ao Atualizar A Disputa!";

        }

        /* internal  string Cadastrar(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items)
         {
             if (String.IsNullOrEmpty(nomeDisputa))
             {
                 return "Precisa De Um Nome Para Disputa!";
             }
             else if (date == null)
             {
                 return "Algo Deu errado Na Data!";
             }
             else if (items == null || items.Count == 0)
             {
                 if (items == null) return "Algo deu errado para cadastrar os animais!";
                 return "Deve ter 1 ou mais animais para a disputa!";

             }
             else
             {
                 List<Animal> animaisSelecionadosUi = items.Cast<Animal>().ToList();
                 List<Animal> animaisSelecionados = new List<Animal>();
                 bool sucess = false;
                 if (animaisSelecionadosUi is not null)
                 {
                     animaisSelecionados = Animals.Where(an => animaisSelecionadosUi.Any(anUi => anUi.Id == an.Id)).ToList();
                 }

                 Disputa? disputa = null;

                 foreach (var animal in animaisSelecionados)
                 {
                     if (animal is null)
                         continue;

                     //var resultado = new Resultado(animal);
                     var resultado = new Resultado();

                     if (resultado is not null)
                     {
                         sucess = _resultadoRepository.Save(resultado);
                         resultado.AssociarAnimal(animal);
                         //resultado.Update(animal);
                         _resultadoRepository.Update(resultado, animal);

                     }
                     // sucess = ResultadoRepository.Save(resultado);
                     if (!sucess)
                         return "Erro ao salvar o Resultado!";

                     Disputa = DisputaRepository.isCreate(nomeDisputa);
                    // disputa = Disputa.isCreate(nomeDisputa);

                     if (Disputa == null)
                     {
                         //criar a disputa.
                         //disputa = new Disputa(nomeDisputa, date ?? DateTime.Now, resultado);
                         Disputa = new Disputa(nomeDisputa, date ?? DateTime.Now);
                         //sucess = DisputaRepository.Save(disputa);
                         sucess = Disputa.save();
                         sucess = Disputa.save();
                         if (!sucess)
                             return "Erro ao salvar a Disputa!";
                     }

                     if (Disputa == null)
                         return "Disputa ainda está nula após tentativa de criação!";

                     sucess = _resultadoRepository.Update(resultado, Disputa);
                     if (!sucess)
                         return "Erro ao atualizar o Resultado!";

                     sucess = _animalRepository.Update(animal, resultado);
                     if (!sucess)
                         return "Erro ao atualizar o Animal!";
                     var caixa = _caixaRepository.GetCaixa();
                     if (caixa is not null)
                     {
                         sucess = _caixaRepository.SaveWithDisputa(Disputa, caixa);
                     }
                     if (!sucess)
                         return "Erro Interno Por Favor contate ao suporte!";
                 }
                 return "Disputa salva com sucesso!";

             }

         }*/
        /// <summary>
        /// Attempts to load a Disputa object from the provided UI item and returns a status message indicating success
        /// or failure.
        /// </summary>
        /// <param name="itemSelecionadoUi">The UI item representing the selected Disputa.</param>
        /// <returns>A string message indicating whether the Disputa was loaded successfully or if an error occurred.</returns>
        internal string LoadDisputa(object itemSelecionadoUi)
        {
            bool sucess = false;
            int idDisputa = 0;
            Disputa? disputaSelecionado = null;
            try
            {
                idDisputa = (int)itemSelecionadoUi;

            }
            catch (InvalidCastException ex)
            {
               
                disputaSelecionado = itemSelecionadoUi as Disputa;
            }
            if (disputaSelecionado == null)
            {
                //pode ser um id do tipo int
                //carregar disputa por id
                if (Disputas.Count > 0){
                    Disputa = Disputas.FirstOrDefault(d => d.Id == idDisputa); 
                    sucess = true;
                }
                else
                    {
                    Disputa = DisputaRepository?.ReadDisputa(idDisputa); 
                    sucess = true; 
                }
                if (Disputa == null) sucess = false;
            }
            else
            {
                //Disputa = DisputaRepository.ReadDisputa(disputaSelecionado);
                Disputa = DisputaRepository.ReadDisputa(disputaSelecionado);
                sucess = true;
                if (Disputa == null) sucess = false;

               /* if (sucess)
                    RodadaController.LoadRodada(Disputa.Id);*/
                //_resultadoController.LoadResultado(Disputa.Id);

            }
            if (sucess == false) return "Erro Ao carregar a disputa!";
            else return "Disputa Carregado com Sucesso!";
        }
        /// <summary>
        /// Loads the list of disputes from the repository into the Disputas collection.
        /// </summary>
        internal void LoadListDisputa()
        {
            if (DisputaRepository is not null)
                Disputas = DisputaRepository.ReadDisputas();
            //Disputas = DisputaRepository.ReadDisputas();
        }
        /// <summary>
        /// Loads animal data and populates the Animals list from the AnimalController.
        /// </summary>
        internal void LoadLists()
        {
            //AnimalController.LoadAnimais();
            _animalController.LoadAnimais();
            Animals = _animalController.Animals.ToList();
            //Animals = AnimalController.Animal;
        }
        /// <summary>
        /// Attempts to remove the selected Disputa from the repository.
        /// </summary>
        /// <param name="disputaSelecionadoUi">The UI object representing the selected Disputa.</param>
        /// <returns>True if the Disputa was successfully removed; otherwise, false.</returns>
        internal bool RemoveDisuptaSelecionado(object disputaSelecionadoUi)
        {
            Disputa? disputaSelecionado = disputaSelecionadoUi as Disputa;
            bool sucess = false;
            if (disputaSelecionado is not null)
            {
                if(disputaSelecionado.Id != Disputa.Id)
                    //rastreio
                    Disputa = DisputaRepository?.Track(disputaSelecionado);

                if (Disputa is not null)
                {
                    if (Disputa.Rodadas is null || Disputa.Rodadas.Count == 0)
                        Disputa.Rodadas = DisputaRepository.GetRodadas(Disputa.Id);
                    Disputa.RemoveRodadas();
                    Disputa.RemovePules();

                    sucess = DisputaRepository.Remove(Disputa);
                }



                //------------------------------------------//



                
            }
            if (sucess) return true;
            else return false;
        }
        /// <summary>
        /// Updates the time for an animal in the current dispute if the provided animal matches and the time differs.
        /// </summary>
        /// <param name="animalUi">The animal object to match and update.</param>
        /// <param name="tempoUi">The new time value to set for the matched animal.</param>
        internal void SalvarDisputa(object animalUi, TimeSpan tempoUi)
        {

            if (Disputa is not null)
            {
                foreach (var res in Disputa.ResultadoList)
                {
                    if (res is not null)
                    {
                        var animal = res.Animal;
                        if (animal is not null)
                        {
                            if (animal.isAnimalMesmoNome(animalUi))
                            {
                                if (res.Tempo != tempoUi)
                                {
                                    //Disputa.UpdateTempo(animalUi, tempoUi, res);
                                    DisputaRepository.UpdateTempo(animalUi, tempoUi, res, Disputa);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Attempts to cast the provided object to an Animal instance.
        /// </summary>
        /// <param name="animalSelecionadoUi">The object to be cast to Animal.</param>
        /// <returns>The Animal instance if the cast is successful; otherwise, null.</returns>
        internal object? ToAnimal(object animalSelecionadoUi)
        {
            Animal? animal = animalSelecionadoUi as Animal;
            if (animal == null)
                return null;
            else
                return animal;
        }
    

        internal bool IsDisputaValida(object disputaSelecionadoUi)
        {
            Disputa? disputaSelecionada = disputaSelecionadoUi as Disputa;
            if (disputaSelecionada != null)
            {
                if (disputaSelecionada.Id != DisputaLocal.Id)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else { return false; }
        }

        internal List<Disputa>? ListarDisputas()
        {
            if (Disputas is not null)
                return Disputas.ToList();
            else
            {
                Disputas = DisputaRepository.GetDisputas()?.ToList();
                return Disputas?.ToList();
            }
        }

        internal Disputa? BuscarDisputa(object disputaSelecionadaUi)
        {
            
            Disputa? disputaSelecionado = disputaSelecionadaUi as Disputa;
            if (disputaSelecionado is not null && disputaSelecionado.Id != Disputa?.Id)
            {
                Disputa = DisputaRepository.ReadDisputa(disputaSelecionado);
                return Disputa;
            }
            else return null;
        }

        internal Disputa? Reload(Disputa disputaMemória)
        {
            return DisputaRepository.Reload(disputaMemória);
        }

        internal void InitAnimalController()
        {
            if (_animalController == null)
                _animalController = new AnimalController();
        }

        internal string Cadastrar(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items, int quantidadeRodadas)
        {
            int i = 0;
            if (String.IsNullOrEmpty(nomeDisputa))
            {
                return "Precisa De Um Nome Para Disputa!";
            }
            else if (date == null)
            {
                return "Algo Deu errado Na Data!";
            }
            else if (items == null || items.Count == 0)
            {
                if (items == null) return "Algo deu errado para cadastrar os animais!";
                return "Deve ter 1 ou mais animais para a disputa!";

            }
            else
            {
                List<Animal> animaisSelecionadosUi = items.Cast<Animal>().ToList();
                List<Animal> animaisSelecionados = new List<Animal>();
                bool sucess = false;
                if (animaisSelecionadosUi is not null)
                {
                    animaisSelecionados = Animals.Where(an => animaisSelecionadosUi.Any(anUi => anUi.Id == an.Id)).ToList();
                }


                
                    

                
                var caixa = _caixaController.GetCaixaRepository().GetCaixa();
                if (Disputa is null)
                {
                    Disputa = DisputaRepository.isCreate(nomeDisputa);
                    if (Disputa is null)
                    {
                        NovaDisputa(nomeDisputa, date ?? DateTime.Now, null);
                        DisputaRepository.AddContext(Disputa);
                    }
                }
               
                
                    //Para cada nova rodada devemos criar novas disputas.
                    while (i < quantidadeRodadas)
                    {
                        RodadaController.NovaRodada(quantidadeRodadas);
                        var rodada = RodadaController.Rodada;
                        if (rodada is not null)
                        {
                            if (rodada.Nrodadas == 0)
                                rodada.Nrodadas = (byte)quantidadeRodadas;
                            if (rodada.Disputa == null)
                                rodada.Disputa = Disputa;

                            foreach (var animalUi in animaisSelecionados)
                            {
                                if (animalUi is null)
                                    continue;

                                //var resultado = new Resultado(animal);
                                //var animal = _animalController.Repository.Consultar(animalUi);
                                //var animal = DisputaRepository.DataBase.Animals.Find(animalUi.Id);
                                var animal = _animalController.IsTracked(animalUi);
                                if (animal is not null)
                                {
                                    var resultado = new Resultado();
                                    //adiciona ao contexto
                                    //_resultadoController.ResultadoRepository.AddContext(resultado);
                                    //DisputaRepository.AddContext(resultado);

                                    animal.Associete(resultado);
                                    //resultado.Animal = animal;
                                    if (rodada.ResultadoDestaRodada is null)
                                        rodada.ResultadoDestaRodada = new();
                                    rodada.ResultadoDestaRodada.Add(resultado);
                                    //Disputa.Associete(resultado);
                                    resultado.Disputa = Disputa;
                                    //DisputaRepository.CheckDuplicate();
                                    if (resultado.Disputa is null)
                                        resultado.Disputa = Disputa;
                                    if (Disputa.Caixa is null)
                                        Disputa.Caixa = caixa;

                                    if (caixa is not null)
                                        caixa.Associete(Disputa);


                                    //sucess = _rodadaController.RodadaRepository.Save(rodada);
                                    //rodada.Associete(Disputa.ResultadoList);
                                    //_rodadaController.RodadaRepository.Update(rodada, Disputa);


                                }
                            }
                            if (Disputa.Rodadas is null)
                                Disputa.Rodadas = new();
                            //verificar se já foi atribuida por algum motivo nas rodadas
                            if(!Disputa.Rodadas.Any(rod => Object.ReferenceEquals(rod, rodada)))
                                Disputa.Rodadas.Add(rodada); //caso for diferentes ele adiciona caso contrario não faz nada

                        }
                        i++;
                    }
                        




                    

                
               
                sucess = DisputaRepository.Save();
                if (!sucess)
                    return "Erro ao salvar A Disputa";
                return "Disputa salva com sucesso!";

            }
        }

        private void NovaDisputa(string nomeDisputa, DateTime dateTime, Resultado? resultado)
        {
            if (resultado == null)
                Disputa = new Disputa(nomeDisputa, dateTime);
            else
                Disputa = new Disputa(nomeDisputa, dateTime, resultado);
        }
        /// <summary>
        /// Releases resources associated with the repository's database.
        /// </summary>
        internal void Clear()
        {
            _repository.GetDataBase().Dispose();
            DisputaRepository = null;

        }

        internal void ReloadDisputas()
        {
            if (_repository is null || _repository.GetDataBase() is null)
            {
                //gera novamente o contexto
                _repository = new Repository();
                if (DisputaRepository == null)
                    DisputaRepository = new DisputaRepository(_repository.GetDataBase()); // recarrega o contexto
                if (DisputaRepository != null)
                    Disputas = DisputaRepository.GetDisputas();



            }
        }

        internal void RemoveAnimalDisputa(object selectedItem)
        {
            //animal carregado;
            _animalController.LoadAnimal(selectedItem);
            var animal = _animalController.Animal;
            if (animal != null)
            {
                if (animal.Resultados is null)
                {
                    _animalController.LoadListsResultado();
                    animal.Resultados = _animalController.Animal.Resultados;
                }
                if (animal.Resultados.Count > 0)
                {
                    var resultado = animal.Resultados.FirstOrDefault(_ => _.Disputa?.Id == Disputa?.Id);

                    if (resultado is not null)
                    {
                        if (Disputa is not null && Disputa.ResultadoList is not null && Disputa.ResultadoList.Count > 0)
                        {


                            resultado.Disputa = null;
                            Disputa.ResultadoList.Remove(resultado);

                            //animal.Resultados.Remove(resultado);
                        }

                    }
                }
            }
        }

        internal void LoadDisputs()
        {
            Disputas = DisputaRepository.GetDisputas();
        }

        internal Disputa? FindDisputa(Disputa disputaUi)
        {
            Disputa? disputa = null;
            if (Disputas is null || Disputas.Count == 0)
                Disputas = DisputaRepository.ReadDisputas();
            disputa = Disputas?.Find(d=> d.Id == disputaUi.Id);
            return disputa;
        }

        internal bool IsEquals(object item)
        {
            Disputa? disputaUi = item as Disputa;
            bool equals = false;
            if(disputaUi is not null && Disputa is not null)
            {
                if(Disputa.Id == disputaUi.Id)
                    equals =  true;
                
            }
            return equals;
        }
        /// <summary>
        /// Load Rodada if null
        /// </summary>
        internal void LoadRodada()
        {
            if(Disputa is not null)
            {
                if(Disputa.Rodadas is null)
                {
                    Disputa.Rodadas = DisputaRepository?.LoadRodadas(Disputa);
                }
            }
        }

        internal void LoadCaixa()
        {
            if (_caixaController.Caixa is null)
                _caixaController.LoadCaixa();
            Disputa.Caixa =  _caixaController.Caixa;
        }

        internal void loadPules()
        {
            //Disputa.Pules = DisputaRepository.LoadPules(Disputa);
            var pules = Disputa.Pules;
            
            if(pules is  null || pules.Count() == 0)
            {
                _puleController.LoadPules();
                var pulesCarregador = _puleController.Pules.Where(pu => pu.Disputa?.Id == Disputa.Id).ToList();
                if (pulesCarregador is not null)
                    pules = pulesCarregador;
                
            }
            Disputa.Pules = pules;
        }

        internal string SalvarDisputa()
        {
            bool sucss = false;
            sucss = DisputaRepository.Save();
            if (sucss)
                return $"Disputa {Disputa?.Nome} Salva com Sucesso!";
            else
                return $"Erro ao salvar a disputa {Disputa?.Nome} Salva com Sucesso!";
        }
        /// <summary>
        /// Returns the repository instance used by this controller.
        /// </summary>
        /// <returns> The Repository instance.</returns>
        internal Repository GetContext()
        {
            return _repository;
        }
        /// <summary>
        /// Updata disputa and save context
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <param name="date"></param>
        /// <param name="idsAnimais"></param>
        /// <param name="quantidadeRodadas"></param>
        /// <returns></returns>
        internal string AtualizarDados(string nomeDisputa, DateTime? date, List<int> idsAnimais, int quantidadeRodadas)
        {
            var disputa = Disputa;
            int nRodada = Disputa?.Rodadas?.Count ?? 0;
            bool sucss = false;
            string mensagem = String.Empty;
            if(disputa != null)
            {
                if(!String.IsNullOrEmpty(nomeDisputa))
                    if(!String.Equals(nomeDisputa, disputa.Nome))//se são diferentes
                        disputa.Nome = nomeDisputa;
                if (date != null)
                    if (!disputa.DataEHora.Equals(date))
                        disputa.DataEHora = (DateTime)date; // se data diferente troca
                if(disputa.GetNMaiorRodada() < quantidadeRodadas)//considerando que a disputa a inda ta com as rodadas antigas;
                {
                    //devo criar as rodadas
                    while(nRodada < quantidadeRodadas)
                    {
                        Rodada novaRodada = new Rodada();
                        DisputaRepository?.AddContext(novaRodada);
                        novaRodada.IdDisputa = disputa.Id;
                        novaRodada.Nrodadas = (byte) (1 + nRodada);
                        foreach(int animalId  in idsAnimais)
                        {
                            Resultado novoResultado = new Resultado();
                            DisputaRepository?.AddContext(novoResultado);

                            novoResultado.DisputaId = disputa.Id;
                            novoResultado.AnimalId = animalId;
                            if (novaRodada.ResultadoDestaRodada is null)
                                novaRodada.ResultadoDestaRodada = new();
                            if(!novaRodada.ResultadoDestaRodada.Contains(novoResultado))
                                novaRodada.ResultadoDestaRodada.Add(novoResultado);
                        }
                        if(disputa.Rodadas is null)
                            disputa.Rodadas = new();
                        if(!disputa.Rodadas.Contains(novaRodada))
                            disputa.Rodadas.Add(novaRodada);
                        nRodada++;

                        
                    }
                }
                else if(disputa.GetNMaiorRodada() > quantidadeRodadas)
               
                {
                    //se diminui a quantidade de rodadas
                    while(disputa.Rodadas?.Count > quantidadeRodadas)
                    {
                        var rodada = disputa.Rodadas.Last();
                        if(rodada is not null)
                        {
                            rodada.Disputa = null;
                            if(rodada.ResultadoDestaRodada is not null)
                            {
                                foreach (var resultado in rodada.ResultadoDestaRodada)
                                {
                                    if(resultado is not null)
                                    {
                                        resultado.Disputa = null;
                                    }
                                }
                            }
                            disputa.Rodadas.Remove(rodada);
                        }
                    }
                }
                    sucss = DisputaRepository?.Save() ?? false;
            }
            if (sucss)
                mensagem = "Disputa Atualizada com sucesso!";
            else
                mensagem = "Erro Ao atualizar a disputa!";
            return mensagem;
        }
    }
}
    

