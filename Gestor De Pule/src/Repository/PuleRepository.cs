using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Repository
{
    class PuleRepository
    {
        internal static List<Pule>? BuscarPorIds(object puleSelecionadosUi)
        {
            using DataBase db = new DataBase();
            List<int> ids = new();
           
            try
            {
                if (puleSelecionadosUi != null)
                {
                    if (puleSelecionadosUi is IList list)
                    {
                        foreach (var item in list)
                        {
                            var tipo = item.GetType();
                            var propId = tipo.GetProperty("Id");
                            if (propId != null)
                            {
                                var valorId = propId.GetValue(item);
                                if (valorId != null)
                                    ids.Add(int.Parse(valorId.ToString()));
                            }
                        }
                        var pulesDb = db.Pules
                            .Include(p => p.Animais)
                            .Include(p=> p.Apostador)
                            .Where(p => ids.Contains(p.Id))
                            .ToList();
                        if (pulesDb != null)
                            return pulesDb;
                        else
                            return null;
                    }

                }
                else
                    return null;
            }catch { return null; }

            return null;
        }
    }
}
