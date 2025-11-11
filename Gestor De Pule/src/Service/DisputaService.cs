using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Service
{
    internal class DisputaService
    {
        internal static List<Disputa>? ListarTodas()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas
                    .Include(dis => dis.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .ToList();
            }catch { return null; }
        }

        internal static Disputa? ReadDisputa(Disputa disputaSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas
                    .Include(dis => dis.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .First(dis => dis.Id == disputaSelecionado.Id);
            }catch { return null; }
        }
    }
}
