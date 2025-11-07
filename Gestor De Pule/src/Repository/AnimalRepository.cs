using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Repository
{
    class AnimalRepository
    {
        /// <summary>
        /// Updata simple
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates the specified animal with the given result in the database.
        /// </summary>
        /// <remarks>This method attempts to update the specified animal by associating it with the
        /// provided result. If the animal or result does not exist in the database, the operation will not be
        /// performed. The method returns <see langword="false"/> if an exception occurs during the update
        /// process.</remarks>
        /// <param name="animal">The animal to be updated. Cannot be null.</param>
        /// <param name="resultado">The result to associate with the animal. Can be null.</param>
        /// <returns><see langword="true"/> if the update operation succeeds; otherwise, <see langword="false"/>.</returns>
        internal static bool Update(Animal? animal, Resultado? resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if(animal is not null)
                {
                    var animalDb = db.Animals
                        .Include(a => a.Resultados)
                        .ThenInclude(res => res.Disputa)
                        .FirstOrDefault(a => a.Id == animal.Id);
                    if(animalDb is not null)
                    {
                        if(resultado is not null)
                        {
                            var resultadoDb = db.Resultados
                                .Include(res => res.Animal)
                                .Include(res => res.Disputa)
                                .FirstOrDefault(res => res.Id == resultado.Id);
                            if(resultadoDb is not null)
                            {
                                animalDb.Resultados.Add(resultadoDb);
                                db.Animals.Update(animalDb);
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }
    }
}
