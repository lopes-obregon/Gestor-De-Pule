using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Repository;
namespace Gestor_De_Pule.src.Controllers
{
    class DisputaController
    {
        public static List<Animal> Animals { get; set; } = new List<Animal>();
        public static Disputa? Disputa { get; private set; } = null;
        public static List<Disputa> Disputas { get; private set; } = new List<Disputa>();
        public static List<Animal> AnimaisRemovidos { get; private set; } = new List<Animal>();
        public List<Disputa>? DisputasLocal { get; }
        public Disputa DisputaLocal { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DisputaCadastrosController class.
        /// </summary>
        public DisputaController()
        {
            DisputasLocal = Disputa.GetDisputasLocal();
        }
        internal static void AddAnimalRemovido(object animalSelecionadoUi)
        {
            if (animalSelecionadoUi is Animal)
            {
                var animalSelecionado = animalSelecionadoUi as Animal;
                if (animalSelecionado is not null)
                    AnimaisRemovidos.Add(animalSelecionado);
            }

        }

        internal static string AtualizarDados(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items)
        {
            bool sucess = false;
            List<Animal>? animaisSelecionadosUi = items.Cast<Animal>().ToList();
            List<Animal>? animaisSelecionados = null;
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
                }
                sucess = DisputaRepository.UpdateDisputa(Disputa, animaisSelecionadosUi, AnimaisRemovidos);
                if (sucess) return "Disputa Atualizado com sucesso!";
                else return "Erro ao Atualizar a Disputa!";

            }
            return "Erro Ao Atualizar A Disputa!";

        }

        internal static string Cadastrar(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items)
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
                        sucess = resultado.save();
                        resultado.AssociarAnimal(animal);
                        resultado.Update(animal);

                    }
                    // sucess = ResultadoRepository.Save(resultado);
                    if (!sucess)
                        return "Erro ao salvar o Resultado!";

                    disputa = Disputa.isCreate(nomeDisputa);

                    if (disputa == null)
                    {
                        //criar a disputa.
                        //disputa = new Disputa(nomeDisputa, date ?? DateTime.Now, resultado);
                        disputa = new Disputa(nomeDisputa, date ?? DateTime.Now);
                        //sucess = DisputaRepository.Save(disputa);
                        sucess = disputa.save();
                        if (!sucess)
                            return "Erro ao salvar a Disputa!";
                    }

                    if (disputa == null)
                        return "Disputa ainda está nula após tentativa de criação!";

                    sucess = ResultadoRepository.Update(resultado, disputa);
                    if (!sucess)
                        return "Erro ao atualizar o Resultado!";

                    sucess = AnimalRepository.Update(animal, resultado);
                    if (!sucess)
                        return "Erro ao atualizar o Animal!";
                    Caixa? caixa = Caixa.GetCaixa() as Caixa;
                    if (caixa is not null)
                    {
                        sucess = caixa.SaveWithDisputa(disputa);
                    }
                    if (!sucess)
                        return "Erro Interno Por Favor contate ao suporte!";
                }
                return "Disputa salva com sucesso!";

            }

        }
        /// <summary>
        /// Attempts to load a Disputa object from the provided UI item and returns a status message indicating success
        /// or failure.
        /// </summary>
        /// <param name="itemSelecionadoUi">The UI item representing the selected Disputa.</param>
        /// <returns>A string message indicating whether the Disputa was loaded successfully or if an error occurred.</returns>
        internal static string LoadDisputa(object itemSelecionadoUi)
        {
            bool sucess = false;
            Disputa? disputaSelecionado = itemSelecionadoUi as Disputa;
            if (disputaSelecionado == null) sucess = false;
            else
            {
                Disputa = DisputaRepository.ReadDisputa(disputaSelecionado);
                sucess = true;
                if (Disputa == null) sucess = false;

            }
            if (sucess == false) return "Erro Ao carregar a disputa!";
            else return "Disputa Carregado com Sucesso!";
        }
        /// <summary>
        /// Loads the list of disputes from the repository into the Disputas collection.
        /// </summary>
        internal static void LoadListDisputa()
        {
            Disputas = DisputaRepository.ReadDisputas();
        }
        /// <summary>
        /// Loads animal data and populates the Animals list from the AnimalController.
        /// </summary>
        internal static void LoadLists()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
        }
        /// <summary>
        /// Attempts to remove the selected Disputa from the repository.
        /// </summary>
        /// <param name="disputaSelecionadoUi">The UI object representing the selected Disputa.</param>
        /// <returns>True if the Disputa was successfully removed; otherwise, false.</returns>
        internal static bool RemoveDisuptaSelecionado(object disputaSelecionadoUi)
        {
            Disputa? disputaSelecionado = disputaSelecionadoUi as Disputa;
            bool sucess = false;
            if (disputaSelecionado is not null)
            {
                sucess = DisputaRepository.Remove(disputaSelecionado);
            }
            if (sucess) return true;
            else return false;
        }
        /// <summary>
        /// Updates the time for an animal in the current dispute if the provided animal matches and the time differs.
        /// </summary>
        /// <param name="animalUi">The animal object to match and update.</param>
        /// <param name="tempoUi">The new time value to set for the matched animal.</param>
        internal static void SalvarDisputa(object animalUi, TimeSpan tempoUi)
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
                                    Disputa.UpdateTempo(animalUi, tempoUi, res);
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
        internal static object? ToAnimal(object animalSelecionadoUi)
        {
            Animal? animal = animalSelecionadoUi as Animal;
            if (animal == null)
                return null;
            else
                return animal;
        }
        /// <summary>
        /// Loads the local dispute data based on the selected UI object and ensures that bettors with invalid IDs are
        /// reloaded.
        /// </summary>
        /// <param name="disputaSelecionadaUi">The selected dispute object from the UI.</param>
        internal void LoadDisputaLocal(object disputaSelecionadaUi)
        {
            Disputa? disputaSelecionado = disputaSelecionadaUi as Disputa;
            if (disputaSelecionado != null)
                if (DisputasLocal is not null && DisputasLocal.Count > 0)
                    DisputaLocal = DisputasLocal.FirstOrDefault(dis => dis.Id == disputaSelecionado.Id);
            //verificar o apostador se tiver id 0 quer dizer que houve erro para carregar no pule
            if (DisputasLocal is not null)
            {
                if (DisputaLocal.Pules is not null && DisputaLocal.Pules.Count > 0)
                {
                    foreach (var pule in DisputaLocal.Pules)
                    {
                        if (pule is not null)
                        {
                            if (pule.Apostador is not null && pule.Apostador.Id == 0)
                            {
                                //se for zero houve erro no carregamento do apostador, precisamos carregar novamente
                                //pule.Apostador = Disputa.ReloadPule(pule);
                                pule.ReloadApostador();
                            }
                        }
                    }
                }
                if (DisputaLocal.ResultadoList.Count > 0)
                {
                    foreach (var resultado in DisputaLocal.ResultadoList)
                    {
                        if (resultado is not null)
                        {
                            if (resultado.Animal is null)
                            {
                                resultado.ReloadAnimal();
                            }
                        }
                    }
                }

            }
        }

        internal bool IsDisputaValida(object disputaSelecionadoUi)
        {
            Disputa? disputaSelecionada = disputaSelecionadoUi as Disputa;
            if(disputaSelecionada != null)
            {
                if(disputaSelecionada.Id != DisputaLocal.Id)
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
    }
}
