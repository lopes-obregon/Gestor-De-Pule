using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Models
{
    internal class Resultado
    {
      /// <summary>
      /// Id resultado
      /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id -Disputa, get, set
        /// </summary>
        public int DisputaId { get; internal set; }
        /// <summary>
        /// Disputa navegate, get,set
        /// </summary>
        public Disputa? Disputa { get; set; } = null;
        /// <summary>
        /// Id -Animal, get, set
        /// </summary>
        public int AnimalId { get; set; }
        /// <summary>
        /// Animal Navegate, get,set
        /// </summary>
        public Animal? Animal { get; set; } = null;
        /// <summary>
        /// Id da rodada;
        /// </summary>
        public int RodadaId {  get; set; }
        /// <summary>
        /// Rodada nagegate
        /// </summary>
        public Rodada Rodada { get; }
        public TimeSpan Tempo { get; set; } = new TimeSpan();
        public byte Posição { get; set; }

        public Resultado() { }

        public Resultado(Animal animal)
        {
            Animal = animal;
        }

        public Resultado(Disputa disputa, Animal animal) : this(animal)
        {
            Disputa = disputa;
            //Animal = animal;
        }

        public Resultado(Disputa disputa, Animal animal, Rodada rodada) : this(disputa, animal)
        {
            Rodada = rodada;
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

    

        internal void ReloadAnimal()
        {
            using DataBase db = new DataBase();
            try
            {
                if (this is not null)
                {
                    if (this.Animal is null)
                    {
                        var resultadoDb = db.Resultados
                            .Include(res => res.Animal)
                            .FirstOrDefault(res => res.Id == this.Id);
                        if (resultadoDb is not null)
                            this.Animal = resultadoDb.Animal;
                    }
                }
            }
            catch { this.Animal = null; }
        }
    }
}
