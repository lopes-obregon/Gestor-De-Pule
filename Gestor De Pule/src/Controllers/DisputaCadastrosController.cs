using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Repository;
namespace Gestor_De_Pule.src.Controllers
{
    class DisputaCadastrosController
    {
        public static List<Animal> Animals { get; set; } = new List<Animal>();
        
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
                foreach(var animal in animaisSelecionados)
                {
                    if(animal is not null)
                    {
                        Resultado resultado = new Resultado(animal);
                        sucess = ResultadoRepository.Save(resultado);
                        //sucess = Resultado.Save(resultado);
                        if (sucess == false) return "Erro ao salvar o Resultado!";
                        Disputa disputa = new Disputa(nomeDisputa, date ?? new DateTime(),  resultado);
                        //Disputa.Save(disputa);
                        sucess =DisputaRepository.Save(disputa);
                        if (sucess == false) return "Erro ao Salvar a Disputa!";

                        resultado.Disputa = disputa;
                        sucess = ResultadoRepository.Update(resultado);
                        if (sucess == false) return "Erro ao Atualizar o resultados!";
                        animal.Resultados.Add(resultado);
                        Animal.Update(animal);
                        sucess = AnimalRepository.Update(animal);
                        if (sucess == false) return "Erro ao atualizar o Animal!";

                    }
                }
                return "Disputa salva com sucesso!";
                
            }

        }

        internal static void LoadLists()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
        }
    }
}
