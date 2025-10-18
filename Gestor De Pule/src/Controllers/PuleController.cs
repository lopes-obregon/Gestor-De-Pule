using Gestor_De_Pule.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Controllers
{
    internal class PuleController
    {
        internal static List<Pule.Status>? GetStatusPagamento()
        {
            return Enum.GetValues(typeof(Pule.Status)).Cast<Pule.Status>().ToList();
        }
    }
}
