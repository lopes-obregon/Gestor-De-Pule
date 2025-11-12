using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Repository;
namespace Gestor_De_Pule.src.Controllers
{
    class DisputaCadastrosController
    {
        public static List<Animal> Animals { get; set; } = new List<Animal>();
        public static Disputa? Disputa { get; private set; } = null;
        public static List<Disputa> Disputas { get;private set; } = new List<Disputa>();
        public static List<Animal> AnimaisRemovidos { get; private set; } = new List<Animal>();

        internal static void AddAnimalRemovido(object animalSelecionadoUi)
        {
            if (animalSelecionadoUi is Animal)
                {var animalSelecionado = animalSelecionadoUi as Animal;
                if(animalSelecionado is not null)
                    AnimaisRemovidos.Add(animalSelecionado);
            }

        }

        internal static string AtualizarDados(string nomeDisputa, DateTime? date, ListBox.ObjectCollection items)
        {
            bool sucess = false;
            List<Animal>? animaisSelecionadosUi = items.Cast<Animal>().ToList();
            List<Animal>? animaisSelecionados = null;
            if(Disputa is not null)
            {
                Disputa.Nome = nomeDisputa;
                if (date != null)
                    Disputa.DataEHora = date ?? DateTime.Now;
                else
                    Disputa.DataEHora = DateTime.Now;
                if(animaisSelecionadosUi.Count > 0)
                {
                    var idsAnimalSelecionadoUi = animaisSelecionadosUi.Select(a => a.Id).ToList();
                    animaisSelecionados = Animals.Where(a=>idsAnimalSelecionadoUi.Contains(a.Id)).ToList();
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
                if(animaisSelecionadosUi is not null)
                {
                    animaisSelecionados = Animals.Where(an=> animaisSelecionadosUi.Any(anUi=>anUi.Id == an.Id)).ToList();
                }

                Disputa? disputa = null;

                foreach (var animal in animaisSelecionados)
                {
                    if (animal is null)
                        continue;

                    var resultado = new Resultado(animal);
                     sucess = ResultadoRepository.Save(resultado);
                    if (!sucess)
                        return "Erro ao salvar o Resultado!";

                     disputa = Disputa.isCreate(nomeDisputa);

                    if (disputa == null)
                    {
                        disputa = new Disputa(nomeDisputa, date ?? DateTime.Now, resultado);
                        sucess = DisputaRepository.Save(disputa);
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
                }
                return "Disputa salva com sucesso!";
                
            }

        }

        internal static string LoadDisputa(object itemSelecionadoUi)
        {
           bool sucess = false;
            Disputa? disputaSelecionado = itemSelecionadoUi as Disputa;
            if (disputaSelecionado == null) sucess = false;
            else
            {
               Disputa =  DisputaRepository.ReadDisputa(disputaSelecionado);
                sucess = true;
                if (Disputa == null) sucess = false;
                
            }
            if (sucess == false) return "Erro Ao carregar a disputa!";
            else return "Disputa Carregado com Sucesso!";
        }

        internal static void LoadListDisputa()
        {
            Disputas = DisputaRepository.ReadDisputas();
        }

        internal static void LoadLists()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
        }

        internal static bool RemoveDisuptaSelecionado(object disputaSelecionadoUi)
        {
            Disputa? disputaSelecionado = disputaSelecionadoUi as Disputa;
            bool sucess = false;
            if(disputaSelecionado is not null)
            {
                 sucess = DisputaRepository.Remove(disputaSelecionado);
            }
            if (sucess) return true;
            else return false;
        }

        internal static object? ToAnimal(object animalSelecionadoUi)
        {
            Animal? animal = animalSelecionadoUi as Animal;
            if (animal == null)
                return null;
            else  
                return animal;
        }
    }
}
