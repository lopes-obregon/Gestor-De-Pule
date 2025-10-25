using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RelatórioApostadorController
    {
        public static  List<Apostador>? Apostadors { get; private set; }
        public static Apostador? Apostador { get; private set; }
        public static List<Pule>? Pules { get; private set; }
        public RelatórioApostadorController() {
            
        }
        /// <summary>
        /// Loads the list of apostadores into the application.
        /// </summary>
        /// <remarks>This method initializes the apostadores list by retrieving data from the
        /// ApostadorController. It must be called before accessing the Apostadors collection to ensure it is
        /// populated.</remarks>
        internal static void LoadLists()
        {
            ApostadorController.LoadApostadores();
            Apostadors = ApostadorController.Apostadors.ToList();
            PuleController.LoadPules();
            Pules = PuleController.Pules.ToList();

        }
        /// <summary>
        /// Loads the selected apostador from the UI into the current context.
        /// </summary>
        /// <remarks>This method attempts to find and set the current apostador based on the provided UI
        /// selection. If the selection is valid and matches an existing apostador in the list, it updates the current
        /// apostador.</remarks>
        /// <param name="apostadorSelecionadoUi">The selected apostador object from the UI, which can be null.</param>
        internal static void LoadApostador(object? apostadorSelecionadoUi)
        {
            Apostador? apostadorSelecionado = apostadorSelecionadoUi as Apostador;
            if(apostadorSelecionado is not null)
                if(Apostadors is not null && Apostadors.Count > 0)
                    Apostador = Apostadors.Find(ap => ap.Id == apostadorSelecionado.Id);
        }
        /// <summary>
        /// Loads the betting slips associated with the current bettor.
        /// </summary>
        /// <remarks>This method filters the betting slips from the <see cref="PuleController.Pules"/>
        /// collection to include only those that belong to the current bettor, identified by <see
        /// cref="Apostador.Id"/>.</remarks>
        internal static void LoadPuLesDoApostador()
        {
            if(PuleController.Pules is not null && PuleController.Pules.Count > 0)
                if(Apostador is not null)
                    Pules = PuleController.Pules.Where(p=>p.Apostador!=null && p.Apostador.Id == Apostador.Id).ToList();

        }
    }
}
