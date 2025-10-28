using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Repository
{
    class AnimalRepository
    {
        internal static bool Update(Animal animal)
        {
            using DataBase db = new DataBase();
            if(animal is not null)
            {
                try
                {
                    db.Attach(animal);
                    db.Animals.Update(animal);
                    db.SaveChanges();
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
    }
}
