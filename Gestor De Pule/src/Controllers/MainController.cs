using Gestor_De_Pule.src.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Controllers
{
    class MainController
    {
        static public List<Apostador> Apostadors { get; private set; } = new List<Apostador>();
        static public Apostador? Apostador {  get; private set; }
        //pules do apostador
        static public List<Pule>? Pules { get; private set; }
        internal static void LoadApostador(object? apostadorSelecionadoUi)
        {
            Apostador? apostadorSelecionado = apostadorSelecionadoUi as Apostador;
            if (apostadorSelecionado != null)
                Apostador = Apostadors.Find(ap => ap.Id == apostadorSelecionado.Id);
            
        }

        internal static void LoadLists()
        {
            ApostadorController.LoadApostadores();
            Apostadors = ApostadorController.Apostadors;
        }

        internal static void LoadPuLesDoApostador()
        {
            PuleController.LoadPules();
            Pules = PuleController.Pules.Where(p => p.Apostador != null && p.Apostador.Id == Apostador.Id).ToList();
        }
    }
}
