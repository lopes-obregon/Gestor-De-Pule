using Gestor_De_Pule.src.Model;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RelatórioApostadorController
    {
        public static  List<Apostador>? Apostadors { get; private set; }
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
        }
    }
}
