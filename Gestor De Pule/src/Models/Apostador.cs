using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Model
{
    internal class Apostador
    {
        //id do apostador
        int _id = 0;
        //nome 
        string _nome = String.Empty;
        //contato
        string _contato = String.Empty;
        public Apostador() { }

        public Apostador(string nome, string contato)
        {
            _nome = nome;
            _contato = contato;
        }

        //------------------------set e get --------------------------/
        //set e get do id
        public int Id { get { return _id; }  set { _id = value; } }
        public string Nome { get { return _nome; } set { _nome = value; } }
        public string Contato { get { return _contato; } set { _contato = value; } }

        internal static bool Save(Apostador apostador)
        {
            using DataBase db = new DataBase();
            try
            {
                db.Apostadors.Add(apostador);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        internal  static List<Apostador> ReadApostadores()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Apostadors.ToList();
            }
            catch { return new List<Apostador>(); }
        }

        internal static Apostador? Load(Apostador? apostador)
        {
            using DataBase db = new DataBase();
            try
            {
                if (apostador is not null)
                    return db.Apostadors.Find(apostador.Id);
            }catch { return null; }
            return null;
        }

        internal static  bool Update(Apostador apostador)
        {
            using DataBase db = new DataBase();
            try
            {
                if (apostador is not null)
                    db.Apostadors.Update(apostador);
                db.SaveChanges();
                return true;
            }catch { return false; }
        }

        internal static bool Remove(Apostador apostador)
        {
            using DataBase db = new DataBase();
            try
            {
                if (apostador is not null)
                    db.Apostadors.Remove(apostador);
                db.SaveChanges();
                return true;
            }catch { return false; }
        }
    }
}
