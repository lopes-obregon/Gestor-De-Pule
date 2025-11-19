using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Gestor_De_Pule.src.Model
{
    internal class Animal
    {
        private int _id = 0;
        private int _número = 0;
        private string _name = "";
        private string _proprietário = "";
        private string _treinador = "";
        private string _jockey = "";
        private string _cidade = "";
        private List<Pule> _pules = new();
        public List<Resultado> Resultados { get; set; } = new List<Resultado>();
        //publics métodos
        //retorna e seta a o id
        public int Id { get { return _id; } set { _id = value; } }
        //retorna e seta o nome
        public string Nome { get { return _name; } set { _name = value; } }
        //set e get proprietário
        public string Proprietário { get { return _proprietário; } set { _proprietário= value; } }
        //set e get treinador
        public string Treinador { get { return _treinador; } set { _treinador = value; } }
        //set e get jock
        public string Jockey { get { return _jockey; } set { _jockey = value; } }
        //set e get cidade
        public string Cidade { get { return _cidade; } set { _cidade = value; } }
        public int Número { get { return _número; } set { _número = value; } }
        public List<Pule> Pules { get { return _pules; } set { _pules = value; } }

        //construct
        public Animal(int número)
        {
            Número = número;
        }

        public Animal(int número, string nome, string proprietário, string treinador, string jockey, string cidade)
        {
            _número = número;
            _name = nome;
            _proprietário = proprietário;
            _treinador = treinador;
            _jockey = jockey;
            _cidade = cidade;
        }
        public Animal() { }

        internal static bool Save(Animal animal)
        {
            using DataBase db = new DataBase();
            try
            {
                
                db.Animals.Add(animal);
                db.SaveChanges();
                return true;
            }
            catch {  return false; }
        }

        internal static List<Animal> ReadAnimals()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Animals
                    .Include(a => a.Pules)
                    .Include(a => a.Resultados)
                    .ToList();
            }catch { return new List<Animal>(); }
                
        }

        internal  static Animal? Consultar(Animal? animalSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                if (animalSelecionado is not null)
                    return db.Animals.Find(animalSelecionado.Id);
                else return null;
            }catch { return null; }
        }

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

        internal static bool Delete(Animal animal)
        {
            using DataBase db = new DataBase();
            try
            {
                db.Animals.Remove(animal);
                db.SaveChanges();
                return true;
            }
            catch {  return false; }
               
        }
        public override string ToString()
        {

            return Número + " - " + _name;
        }

        internal void UpDate()
        {
            using DataBase db = new DataBase();
            try
            {
                if(this is not null)
                {
                    db.Animals.Update(this);
                    db.SaveChanges();
                }
            }catch { }
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
                if (animal.Id == _id) return true;
                else return false;
            }else { return false; }
        }
    }
}
