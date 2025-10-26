using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Repository
{
    class DisputaRepository
    {
        internal static bool Save(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                    List<Resultado> resultados = disputa.ResultadoList.ToList();
                    foreach(Resultado resultado in resultados)
                    {
                        if (resultado is not null)
                        {
                           db.Attach(resultado);
                            db.Entry(resultado).State = EntityState.Modified;
                           db.Attach(resultado.Animal);
                            db.Entry(resultado.Animal).State = EntityState.Modified;
                        }
                    }
                    db.Disputas.Add(disputa);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;
        }
    }
}
