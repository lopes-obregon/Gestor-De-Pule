using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Model
{
    internal   class Apostador
    {
        //id do apostador
        int _id = 0;
        //nome 
        string _nome = String.Empty;
        //contato
        string _contato = String.Empty;
        public List<Pule> Pules { get; set; } = new List<Pule>();
        public Apostador() {
           
        }

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
                return db.Apostadors
                    .Include(a => a.Pules)
                    .ToList();
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
        public override string ToString()
        {
            return _nome;
        }
        public override bool Equals(object? obj)
        {
            Apostador? apostadorSelecionado = obj as Apostador;
            if (apostadorSelecionado is not null)
            {
                return this.Id == apostadorSelecionado.Id;
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        internal static Apostador? GetApostador(Apostador apostadorUi)
        {
            using DataBase db = new DataBase();
            try
            {
                if(apostadorUi is not null)
                {
                    var apostadorDb = db.Apostadors.Include(ap => ap.Pules).ThenInclude(pule => pule.Animais).FirstOrDefault(ap => ap.Id == apostadorUi.Id);
                    if (apostadorDb is not null)
                        return apostadorDb;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }catch { return null; }
        }
    }
}
