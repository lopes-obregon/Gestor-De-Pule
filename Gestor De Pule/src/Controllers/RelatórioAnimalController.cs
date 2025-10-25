using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RelatórioAnimalController
    {
        public static List<Animal>? Animals {  get; private set; }
        public static Animal ? Animal { get; private set; }
        private static List<Pule>? Pules { get; set; }
        internal static void AnimalSelecionado(object? animalSelecionadoUi)
        {
            Animal? animalSelecionado = animalSelecionadoUi as Animal;
            if (animalSelecionado != null)
            {
                if (Animals is not null && Animals.Count > 0)
                {
                    Animal = Animals.Find(an => an.Id == animalSelecionado.Id);
                }
            }
        }

        internal static void LoadLists()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
            PuleController.LoadPules();
            if(PuleController.Pules != null)
                Pules = PuleController.Pules.ToList();
        }

        internal static Pule? SearchPule(Pule pule)
        {
            if(Pules is not null &&  Pules.Count > 0)
                return Pules.Find(p => p.Id == pule.Id);
            return null;
        }
    }
}
