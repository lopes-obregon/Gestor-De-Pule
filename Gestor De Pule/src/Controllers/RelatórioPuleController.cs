using Gestor_De_Pule.src.Model;
using System.Collections;

namespace Gestor_De_Pule.src.Controllers
{
    internal class RelatórioPuleController
    {
        static public List<Pule>? Pules { get; private set; }
        static public Pule? Pule { get; private set; }

        internal static List<int> GetAttrNumPule()
        {
            if (Pules is not null && Pules.Count > 0)
            {
                return Pules.Select(p => p.Número)
                    .Distinct()
                    .ToList();
            }
            return new List<int>();
        }

        internal static void LoadLists()
        {
            PuleController.LoadPules();
            if (PuleController.Pules != null)
            {
                Pules = PuleController.Pules.ToList();
            }
            else {Pules = new List<Pule>();}
        }
        /// <summary>
        /// Selects a specific <see cref="Pule"/> object from a collection based on the provided UI selection.
        /// </summary>
        /// <remarks>This method attempts to find and select a <see cref="Pule"/> from the <c>Pules</c>
        /// collection that matches the ID of the provided UI selection. If the collection is null or empty, no action
        /// is taken.</remarks>
        /// <param name="puleSelecionadoUi">The UI object representing the selected <see cref="Pule"/>. Must be castable to <see cref="Pule"/>.</param>
        internal static List<Pule> PuleSelecionado(object puleSelecionadoUi)
        {
            int? puleSelecionado = puleSelecionadoUi as int?;
            if (puleSelecionado != null)
            {
                if (Pules is not null && Pules.Count > 0)
                {
                    return Pules.Select(p=>p)
                        .Where(p=> p.Número == puleSelecionado).ToList();
                }
            }
            return new List<Pule>();

        }
    }
}
