using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RelatórioAnimalController
    {
        /// <summary>
        /// Gets the list of animals currently managed by the system.
        /// </summary>
        public static List<Animal>? Animals {  get; private set; }
        public static Animal ? Animal { get; private set; }
        private static List<Pule>? Pules { get; set; }
        /// <summary>
        /// Selects an animal from the list of available animals based on the provided UI object.
        /// </summary>
        /// <remarks>This method attempts to find and set the <see cref="Animal"/> from the <see
        /// cref="Animals"/> list that matches the ID of the provided UI object. If the UI object is not an <see
        /// cref="Animal"/> or if the <see cref="Animals"/> list is null or empty, no action is taken.</remarks>
        /// <param name="animalSelecionadoUi">The UI object representing the selected animal. Can be null.</param>
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
        /// <summary>
        /// Loads and initializes the lists of animals and pules.
        /// </summary>
        /// <remarks>This method populates the <see cref="Animals"/> and <see cref="Pules"/> lists by
        /// invoking the respective controllers to load data. It ensures that the lists are up-to-date with the current
        /// data from the controllers.</remarks>
        internal static void LoadLists()
        {
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals.ToList();
            PuleController.LoadPules();
            if(PuleController.Pules != null)
                Pules = PuleController.Pules.ToList();
        }
        /// <summary>
        /// Searches for a <see cref="Pule"/> in the collection that matches the specified <paramref name="pule"/>.
        /// </summary>
        /// <remarks>The method searches within a collection of <see cref="Pule"/> objects. If the
        /// collection is empty or <see langword="null"/>, the method returns <see langword="null"/>.</remarks>
        /// <param name="pule">The <see cref="Pule"/> object to search for, identified by its <c>Id</c> property.</param>
        /// <returns>The matching <see cref="Pule"/> if found; otherwise, <see langword="null"/>.</returns>
        internal static Pule? SearchPule(Pule pule)
        {
            if(Pules is not null &&  Pules.Count > 0)
                return Pules.Find(p => p.Id == pule.Id);
            return null;
        }
    }
}
