using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Gestor_De_Pule.src.Model
{
     internal class Animal
    {
       
        public List<Resultado> Resultados { get; set; } = new List<Resultado>();
        //publics métodos
        //retorna e seta a o id
        public int Id { get; set ; } 
        //retorna e seta o nome
        public string Nome { get;  set; }
        //set e get proprietário
        public string Proprietário { get;  set; }
        //set e get treinador
        public string Treinador { get; set; }
        //set e get jock
        public string Jockey { get; set; }
        //set e get cidade
        public string Cidade { get; set; }
        public int Número { get; set; }
        public List<Pule> Pules { get; set; }

        //construct
        public Animal(int número)
        {
            Número = número;
        }

        public Animal(int número, string nome, string proprietário, string treinador, string jockey, string cidade)
        {
            Número = número;
            Nome = nome;
            Proprietário = proprietário;
            Treinador = treinador;
            Jockey = jockey;
            Cidade = cidade;
        }
        public Animal() { }

       
       

        

        internal static bool Update(Animal animal)
        {
            using DataBase db = new DataBase();
            try
            {
                if (animal is not null)
                    db.Animals.Update(animal);
                db.SaveChanges();
                return true;
            }catch  { return false; }
        }

       
        public override string ToString()
        {

            return Número + " - " + this.Nome;
        }


        internal bool isAnimalMesmoNome(object animalNome)
        {
           string? animalNomeStr = animalNome.ToString();
            if(animalNomeStr != null)
            {
                if (animalNomeStr == Nome) return true;
                else return false;
            }
            else { return false; }
        }

        internal bool isMesmoId(object animalUi)
        {
            Animal? animal = animalUi as Animal;
            if(animal is not null)
            {
                if (animal.Id == this.Id) return true;
                else return false;
            }else { return false; }
        }

        internal  static Animal? GetAnimal(Animal animalSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                var animalDb = db.Animals.Include(an => an.Resultados)
                    .Include(an => an.Pules)
                    .FirstOrDefault(an => an.Id == animalSelecionado.Id);
                if (animalDb != null)
                {
                    return animalDb;
                }
                else
                {
                    return null;
                }

            } catch { return null; }
        }
        /// <summary>
        /// Associates the specified Resultado with the current object if it is not already present.
        /// </summary>
        /// <param name="resultado">The Resultado instance to associate.</param>
        internal void Associete(Resultado resultado)
        {
            if (this.Resultados is null)
                this.Resultados = new List<Resultado> { resultado };
            else
               if(!this.Resultados.Any(res => res.Id == resultado.Id))
                //se não tiver adiciona
                    this.Resultados.Add(resultado);
        }
    }
}
