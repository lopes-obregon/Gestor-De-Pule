using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src
{
    class ResultadoRepository
    {
        public static bool Save(Models.Resultado resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultado is not null)
                {
                    if (resultado.Animal is not null)
                    {
                        resultado.Animal = db.Animals.Find(resultado.Animal.Id);
                        db.Resultados.Add(resultado);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch {  return false; }
            return false;
        }

        internal static bool Update(Resultado resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if (resultado is not null)
                {
                    db.Resultados.Update(resultado);
                    db.SaveChanges();
                    return true;
                }
            }catch { return false; }
            return false;
        }
    }
}
