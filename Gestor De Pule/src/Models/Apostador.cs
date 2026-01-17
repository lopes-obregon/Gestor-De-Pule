using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Model
{
    internal   class Apostador
    {
       
        public List<Pule> Pules { get; set; } = new List<Pule>();
        public Apostador() {
           
        }

        public Apostador(string nome, string contato)
        {
            Nome = nome;
            Contato = contato;
        }

        //------------------------set e get --------------------------/
        //set e get do id
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Contato { get; set; }

       

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
        public override string ToString()
        {
            return Nome;
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
