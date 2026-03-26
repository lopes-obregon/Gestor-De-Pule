using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;

namespace Gestor_De_Pule.src.Service
{
    internal class DisputaService
    {
        /// <summary>
        /// Disputa única
        /// </summary>
        public Disputa? Disputa;
        /// <summary>
        /// List Disputs
        /// </summary>
        public List<Disputa>? Disputs;
        /// <summary>
        /// Repository or db
        /// </summary>
        private DisputaRepository _disputaRepository;
        /// <summary>
        /// Service animal
        /// </summary>
        private readonly AnimalService _animalService;
        /// <summary>
        /// Service Caixa
        /// </summary>
        private readonly CaixaService _caixaService;
        /// <summary>
        /// Service Rodada
        /// </summary>
        private readonly RodadaService _rdadaService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data"></param>
        public DisputaService(DataBase data)
        {
            _disputaRepository = new DisputaRepository(data);
            _animalService = new AnimalService(data);
            _caixaService = new CaixaService(data);
            _rdadaService = new RodadaService(data);
        }
        public DisputaService(object data)
        {
            _disputaRepository = new DisputaRepository(data);
        }
        internal string AtualizarDados(Disputa? disputa, string nomeDisputa, DateTime? date, List<int> idsAnimais, int quantidadeRodadas)
        {

            int nRodada = disputa?.Rodadas?.Count ?? 0;
            bool sucss = false;
            string mensagem = String.Empty;
            int i = 0;
            if (disputa != null)
            {
                if (!String.IsNullOrEmpty(nomeDisputa))
                    if (!String.Equals(nomeDisputa, disputa.Nome))//se são diferentes
                        disputa.Nome = nomeDisputa;
                if (date != null)
                    if (!disputa.DataEHora.Equals(date))
                        disputa.DataEHora = (DateTime)date; // se data diferente troca
                //ver se mudou o animal
                if (disputa.MudouAnimalRodada(idsAnimais))
                    RemoveAnimais(disputa, idsAnimais);
                if (disputa.GetNMaiorRodada() < quantidadeRodadas)//considerando que a disputa a inda ta com as rodadas antigas;
                {
                    //devo criar as rodadas
                    while (nRodada < quantidadeRodadas)
                    {
                        Rodada novaRodada = new Rodada();
                        _disputaRepository?.AddContext(novaRodada);
                        novaRodada.DisputaId = disputa.Id;
                        novaRodada.Nrodadas = (byte)(1 + nRodada);
                        foreach (int animalId in idsAnimais)
                        {
                            Resultado novoResultado = new Resultado();
                            _disputaRepository?.AddContext(novoResultado);

                            novoResultado.DisputaId = disputa.Id;
                            novoResultado.AnimalId = animalId;
                            if (novaRodada.ResultadoDestaRodada is null)
                                novaRodada.ResultadoDestaRodada = new();
                            if (!novaRodada.ResultadoDestaRodada.Contains(novoResultado))
                                novaRodada.ResultadoDestaRodada.Add(novoResultado);
                        }
                        if (disputa.Rodadas is null)
                            disputa.Rodadas = new();
                        if (!disputa.Rodadas.Contains(novaRodada))
                            disputa.Rodadas.Add(novaRodada);
                        nRodada++;


                    }
                }
                else if (disputa.GetNMaiorRodada() > quantidadeRodadas)

                {
                    //se diminui a quantidade de rodadas
                    while (disputa.Rodadas?.Count > quantidadeRodadas)
                    {
                        var rodada = disputa.Rodadas.Last();
                        if (rodada is not null)
                        {
                            rodada.Disputa = null;
                            if (rodada.ResultadoDestaRodada is not null)
                            {
                                foreach (var resultado in rodada.ResultadoDestaRodada)
                                {
                                    if (resultado is not null)
                                    {
                                        resultado.Disputa = null;
                                    }
                                }
                            }
                            disputa.Rodadas.Remove(rodada);
                        }
                    }
                }
                else
                {
                    //quero o que não contem na lista da disputa

                    while (i < disputa.Rodadas?.Count)
                    {
                        var rodada = disputa.Rodadas[i];
                        if (rodada is not null)
                        {
                            //quero o que não contem na lista da disputa
                            //retornar os animais cujo o id não aparece em resultados
                            if (rodada.ResultadoDestaRodada is not null)
                            {
                                var animais = idsAnimais.Where(id => !rodada.ResultadoDestaRodada.Any(res => res.AnimalId == id)).ToList();
                                //criar novo resultado
                                foreach (var animal in animais)
                                {
                                    var resultado = new Resultado();
                                    if (resultado is not null)
                                    {
                                        _disputaRepository.AddContext(resultado);
                                        resultado.AnimalId = animal;
                                        resultado.Disputa = disputa;
                                        rodada.ResultadoDestaRodada?.Add(resultado);


                                    }

                                }
                            }
                        }
                        i++;
                    }
                }
                sucss = _disputaRepository?.Save() ?? false;
            }
            if (sucss)
                mensagem = "Disputa Atualizada com sucesso!";
            else
                mensagem = "Erro Ao atualizar a disputa!";
            return mensagem;
        }
        /// <summary>
        /// Lê do banco a disputa
        /// </summary>
        /// <param name="idDisputa"></param>
        /// <returns>Disputa ou null se não encontrar a disputa</returns>
        internal Disputa? GetById(int idDisputa)
        {
            //primeiro verifica na memória
            if (Disputa is not null)
            {
                if (Disputa.Id != idDisputa)
                {
                    Disputa = Disputs?.FirstOrDefault(d => d.Id == idDisputa) ?? _disputaRepository.GetById(idDisputa);
                }
            }
            else
            {
                Disputa = Disputs?.FirstOrDefault(d => d.Id == idDisputa) ?? _disputaRepository.GetById(idDisputa);
            }

            return Disputa;
        }
        internal bool GetById(object itemSelecionadoUi)
        {
            int idDisputa = 0;
            Disputa? disputaSelecionado = null;
            bool sucess = false;
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
                if (Disputs?.Count > 0)
                {
                    Disputa = Disputs.FirstOrDefault(d => d.Id == idDisputa);
                    sucess = true;
                }
                else
                {
                    //Disputa = DisputaRepository?.ReadDisputa(idDisputa); 
                    Disputa = _disputaRepository.GetById(idDisputa);
                    sucess = true;
                }
                if (Disputa == null) sucess = false;
            }
            else
            {
                //Disputa = DisputaRepository.ReadDisputa(disputaSelecionado);
                Disputa = _disputaRepository.ReadDisputa(disputaSelecionado);
                sucess = true;
                if (Disputa == null) sucess = false;



            }
            return sucess;

        }
        /// <summary>
        /// Veja qual animal foi removido e ajusta a relação
        /// </summary>
        /// <param name="disputa"></param>
        /// <param name="idsAnimais"></param>
        private void RemoveAnimais(Disputa disputa, List<int> idsAnimais)
        {
            if (disputa.Rodadas != null)
            {
                foreach (var rodada in disputa.Rodadas)
                {
                    if (rodada is not null)
                    {
                        var resultados = rodada.ResultadoDestaRodada.Where(res => !idsAnimais.Contains(res.AnimalId)).ToList();
                        if (resultados is not null)
                        {
                            foreach (var resultado in resultados)
                            {
                                if (resultado is not null)
                                {
                                    resultado.Disputa = null;
                                    rodada.ResultadoDestaRodada.Remove(resultado);
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Carrega as rodadas da disputa em memória
        /// </summary>
        internal void LoadRodadas()
        {
            if (Disputa is not null)
            {
                if (Disputa.Rodadas is null)
                {
                    Disputa.Rodadas = _disputaRepository?.LoadRodadas(Disputa);
                }
            }
        }
        /// <summary>
        /// Carrega em memória as disputas
        /// </summary>
        /// <returns>Lista de disputas</returns>
        internal List<Disputa>? GetDisputs()
        {
            if (Disputs is not null)
                return Disputs.ToList();
            else
            {
                Disputs = _disputaRepository.GetDisputas()?.ToList();
                return Disputs;
            }
        }
        /// <summary>
        /// Remove disputa conforme Procurado pelo id
        /// </summary>
        /// <param name="disputaSelecionado"></param>
        /// <returns></returns>
        internal bool RemoveDisputaSelecionado(Disputa? disputaSelecionado)
        {
            bool sucess = false;
            if (disputaSelecionado is not null)
            {
                if (disputaSelecionado.Id != Disputa?.Id)
                    //rastreio
                    Disputa = _disputaRepository?.Track(disputaSelecionado);

                if (Disputa is not null)
                {
                    if (Disputa.Rodadas is null || Disputa.Rodadas.Count == 0)
                        Disputa.Rodadas = _disputaRepository?.GetRodadas(Disputa.Id);
                    Disputa.RemoveRodadas();
                    Disputa.RemovePules();

                    sucess = _disputaRepository.Remove(Disputa);
                }


            }
            return sucess;
        }
        /// <summary>
        /// Cria a nova disputa e adiciona na memória
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <param name="date"></param>
        /// <param name="items"></param>
        /// <param name="quantidadeRodadas"></param>
        internal void Create(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items, int quantidadeRodadas)
        {
           int i = 0;
            //var animalService = new AnimalService(_disputaRepository.GetDataBase);
            _animalService.LoadAnimalsSelecionados(items);
            bool sucess = false;
            var animaisSelecionados = _animalService.Animals;
            var caixa = _caixaService.GetCaixa();
            //var caixa = _caixaController.GetCaixaRepository().GetCaixa();
            if (Disputa is null)
            {
                Disputa = _disputaRepository.isCreate(nomeDisputa);
                if (Disputa is null)
                {
                    NovaDisputa(nomeDisputa, date ?? DateTime.Now, null);
                    _disputaRepository.AddContext(Disputa);
                }
            }


            //Para cada nova rodada devemos criar novas disputas.
            while (i < quantidadeRodadas)
            {
                //RodadaController.NovaRodada(quantidadeRodadas);
                _rdadaService.NewRodada(quantidadeRodadas);
                var rodada = _rdadaService.Rodada;
                if (rodada is not null)
                {
                    if (rodada.Nrodadas == 0)
                        rodada.Nrodadas = (byte)quantidadeRodadas;
                    if (rodada.Disputa == null)
                        rodada.Disputa = Disputa;

                    if(animaisSelecionados is not null)
                    {

                        foreach (var animal in animaisSelecionados)
                        {
                            if (animal is null)
                                continue;

                            //var resultado = new Resultado(animal);
                            //var animal = _animalController.Repository.Consultar(animalUi);
                            //var animal = DisputaRepository.DataBase.Animals.Find(animalUi.Id);
                            //var animal = _animalService.IsTracked(animalUi);
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
                    }
                    if (Disputa.Rodadas is null)
                        Disputa.Rodadas = new();
                    //verificar se já foi atribuida por algum motivo nas rodadas
                    if (!Disputa.Rodadas.Any(rod => Object.ReferenceEquals(rod, rodada)))
                        Disputa.Rodadas.Add(rodada); //caso for diferentes ele adiciona caso contrario não faz nada

                }
                i++;
            }
        }
        /// <summary>
        /// Gera Nova instancia para Disputa
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <param name="dateTime"></param>
        /// <param name="resultado"></param>
        private void NovaDisputa(string nomeDisputa, DateTime dateTime, Resultado? resultado)
        {
            if (resultado == null)
                Disputa = new Disputa(nomeDisputa, dateTime);
            else
                Disputa = new Disputa(nomeDisputa, dateTime, resultado);
        }

        internal bool Save()
        {
            return _disputaRepository.Save();
        }
        /// <summary>
        /// Instancia nova disputa com nome data e caixa definidos
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <param name="date"></param>
        /// <param name="caixa"></param>
        /// <returns> a nova instancia criada e já adicionada ao contexto</returns>
        internal Disputa NovaDisputa(string nomeDisputa, DateTime? date, Caixa caixa)
        {
            Disputa = new Disputa(nomeDisputa, date, caixa);
            _disputaRepository.AddContext(Disputa);
            return Disputa;
        }
        /// <summary>
        /// Loads <see cref="Disputs"/>  in memory
        /// </summary>
        /// <param name="id">unique <see cref="Caixa"/> identifier</param>
        internal void LoadDisputsByCaixaId(int id)
        {
            if(Disputs is null ||  Disputs.Count == 0)
            {
                Disputs = _disputaRepository.GetDisputasByCauxaId(id);
            }
        }
        /// <summary>
        /// search a dispute  in disputs list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> A dispute searched</returns>
        internal Disputa? SelectDispute(int id)
        {
            Disputa? dispute = null;
            if(Disputs?.Count > 0)
            {
                dispute = Disputs.FirstOrDefault(dis=> dis.Id == id);
            }
            return dispute;
        }
    }
}
