using Gestor_De_Pule.src.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Controllers
{
    class MainController
    {
        static public List<Apostador> Apostadors { get; private set; } = new List<Apostador>();
        static public Apostador? Apostador {  get; private set; }
        static public List<Animal>? Animals { get; private set; }
        static public Animal? Animal { get; private set; }
        //pules do apostador
        static public List<Pule>? Pules { get; private set; }
        /// <summary>
        /// Handles the selection of an animal from the user interface.
        /// </summary>
        /// <param name="animalSelecionadoUi">The selected animal object from the UI. Can be <see langword="null"/> if no animal is selected.</param>
        /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
        internal static void AnimalSelecionado(object? animalSelecionadoUi)
        {
            Animal? animalSelecionado = animalSelecionadoUi as Animal;
            if (animalSelecionado == null)
            {
                Animal = null;
            }
            else
            {
                if(Animals is not null)
                    Animal = Animals.Find(a => a.Id == animalSelecionado.Id);
            }
        }
        /// <summary>
        /// Loads the selected apostador from the UI and updates the current apostador reference.
        /// </summary>
        /// <remarks>This method attempts to cast the provided UI object to an <see cref="Apostador"/>
        /// type. If successful, it updates the current apostador reference by finding the corresponding apostador in
        /// the collection based on the ID.</remarks>
        /// <param name="apostadorSelecionadoUi">The selected apostador object from the UI, which can be null.</param>
        internal static void LoadApostador(object? apostadorSelecionadoUi)
        {
            Apostador? apostadorSelecionado = apostadorSelecionadoUi as Apostador;
            if (apostadorSelecionado != null)
                Apostador = Apostadors.Find(ap => ap.Id == apostadorSelecionado.Id);
            
        }
        /// <summary>
        /// Loads and initializes the lists of apostadores and animals.
        /// </summary>
        /// <remarks>This method calls the respective controllers to load apostadores and animals, and
        /// assigns the loaded data to the corresponding lists. It must be called before accessing the lists to ensure
        /// they are populated with the latest data.</remarks>
        internal static void LoadLists()
        {
            ApostadorController.LoadApostadores();
            Apostadors = ApostadorController.Apostadors;
            AnimalController.LoadAnimais();
            Animals = AnimalController.Animals;
            PuleController.LoadPules();
            Pules = PuleController.Pules;
        }
        /// <summary>
        /// Loads and filters the list of 'Pules' associated with the current 'Apostador'.
        /// </summary>
        /// <remarks>This method retrieves all 'Pules' and filters them to include only those associated
        /// with the current 'Apostador'. Ensure that the 'Apostador' object is properly initialized before calling this
        /// method.</remarks>
        internal static void LoadPuLesDoApostador()
        {
            PuleController.LoadPules();
            Pules = PuleController.Pules.Where(p => p.Apostador != null && p.Apostador.Id == Apostador.Id).ToList();
        }

        internal static Pule? SearchPule(Pule pule)
        {
            if(Pules is not null && Pules.Count > 0)
            {
                return Pules.Find(p => p.Id == pule.Id) ?? null;
            }
            else
            {
                return null;
            }

        }
    }
}
