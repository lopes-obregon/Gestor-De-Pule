using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Models
{
    class Resultado
    {
        public int Id { get; set; }
        public Disputa? Disputa { get; set; } = null;
        public Animal? Animal { get; set; } = null;
        public TimeSpan Tempo { get; set; } = new TimeSpan();
        public byte Posição { get; set; }

        public Resultado() { }

        public Resultado(Animal animal)
        {
            Animal = animal;
        }

        internal static Resultado? BuscarResultado(Resultado resultadoUi)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultadoUi is not null)
                {
                    var resultadoDb = db.Resultados
                        .Include(res => res.Disputa)
                        .Include(res => res.Animal)
                        .FirstOrDefault(res => res.Id == resultadoUi.Id);
                    if (resultadoDb is not null) return resultadoDb;
                    else return null;
                }
            }catch { return null; }
            return null;
        }

        internal bool save()
        {
            using DataBase db = new DataBase();
            try
            {
                var resultadoDb = db.Resultados.FirstOrDefault(res => res.Id == this.Id);
                if(resultadoDb is  null)
                {
                    // quer dizer que é um resultado novo.
                    // this.Id = 0; //correção da chave
                    resultadoDb = this;
                    db.Resultados.Add(resultadoDb);
                    db.SaveChanges();
                    return true;

                }
            }
            catch { return false; }
            return false;
        }

        internal void AssociarAnimal(Animal animal)
        {
            using DataBase db = new DataBase();
            try
            {
                var animalDb = db.Animals.Include(an => an.Resultados).FirstOrDefault(an => an.Id == animal.Id);
                if (animalDb is not null)
                {
                    animalDb.Resultados.Add(this);
                    this.Animal = animalDb;
                    db.Animals.Update(animalDb);
                }
                db.SaveChanges();
            }
            catch { return; }
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }

        internal void Update(Animal animal)
        {
            using DataBase db = new DataBase();
            try
            {
                if(this is not null)
                {
                    var resultadoDb = db.Resultados
                        .Include(res => res.Animal)
                        .FirstOrDefault(res => res.Id == this.Id);
                    if(resultadoDb is not null)
                    {
                        if(resultadoDb.Animal is null)
                        {
                            var animalDb = db.Animals.FirstOrDefault(an => an.Id == animal.Id);
                            if(animalDb is not null)
                            {
                                //já teve uma associação externa
                                if(this.Animal is not null)
                                {
                                    //associação do banco
                                    if(resultadoDb.Animal is null)
                                    {
                                        //quer dizer que não fiz a associação a inda
                                        resultadoDb.Animal = animalDb;
                                        animalDb.Resultados.Add(resultadoDb);
                                        this.Animal = resultadoDb.Animal;
                                        db.Resultados.Update(resultadoDb);
                                        db.Animals.Update(animalDb);
                                    }
                                }
                                   

                            }
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch { return; }
        }
    }
}
