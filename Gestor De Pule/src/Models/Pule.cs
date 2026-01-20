using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Gestor_De_Pule.src.Model
{
        public  enum StatusPagamento
        {
            Pendente,
            Pago,
            Cancelado
        }
     internal class Pule
    {
        //menbros
        private int _id;
        private Apostador? _apostador = new();
        private StatusPagamento _statusPagamento;
        private DateTime _date = DateTime.Now;
        private List<Animal>? _animais = new();
        public Disputa? Disputa { get; set; }
        public int Número { get; set; }
        public float Valor {  get; set; }
        //Propriedade para chamar no listVew
        public string NomeAnimais => (string)AnimaisToString();
        public string ValorFormatado => Valor.ToString("C2");
        //construct
        public Pule() { }

        public Pule(Apostador? apostador, StatusPagamento pagamento, List<Animal>? animais, float valor, int númeroDoPule)
        {
            Apostador = apostador;
            _statusPagamento = pagamento;
            _date = DateTime.Now;
            _animais = animais;
            Valor = valor;
            Número = númeroDoPule;
        }

        //sett gett métodos

        public int Id { get { return _id; } set { _id = value; } }
        public Apostador? Apostador { get { return _apostador; } set { _apostador = value; } }
        public StatusPagamento StatusPagamento { get { return _statusPagamento; } set { _statusPagamento = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public List<Animal>? Animais { get { return _animais; } set { _animais = value; } }

        internal static List<Pule> ReadPules()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Pules
                    .Include(p => p.Apostador)
                    .Include(p => p.Animais)
                    .ToList();
            }
            catch { return new List<Pule>(); }
        }

        internal static bool Save(Pule pule)
        {
            //são sei pq esse método ta dando erro.
            using DataBase db = new DataBase();
            try
            {
                if(pule is not null)
                {
                    
                    
                    db.Pules.Add(pule);
                    db.SaveChanges();
                   // Apostador? apostador = pule.Apostador;
                
                    if (pule.Apostador != null){
                        
                        pule.Apostador.Pules.Add(pule);
                        db.Apostadors.Update(pule.Apostador);
                    }
                    foreach(Animal animal in pule.Animais)
                    {
                        if(animal is not null)
                        {
                           // db.Animals.Attach(animal);
                            animal.Pules.Add(pule);
                            db.Animals.Update(animal);
                        }
                    }

                   // db.Pules.Add(pule);
                   db.Pules.Update(pule);
                }
                db.SaveChanges();
                return true;
            }
            catch {  return false; }

        }

        

      
        
        internal String AnimaisToString()
        {
            string nomeAnimal = String.Empty;
            for(int i =0; i < Animais.Count; i++)
            {
                if(Animais[i] is not null)
                {
                    nomeAnimal += Animais[i].Nome;
                    if (i+1 < Animais.Count)
                    {
                        nomeAnimal += ", ";
                    }
                }
            }
            return nomeAnimal;
        }
        

       
        public override string ToString()
        {
            return Número.ToString();
        }

        internal static Pule? ToPule(object puleSelecionadoUi)
        {
            Pule? pule = puleSelecionadoUi as Pule;
            if (pule == null) return null;
            else return pule;
        }

        internal static List<Pule>? ToPules(object puleSelecionadosUi)
        {
            List<Pule>? pules = puleSelecionadosUi as List<Pule>;
            if (pules == null) return null; return pules;
        }

        internal static Apostador? ReloadPuleApostador(int idApostador)
        {
            using DataBase db = new DataBase();
            Apostador? apostador = null;
            try
            {
                if(idApostador != 0)
                {
                    var apostadorPuleDb = db.Apostadors.FirstOrDefault(ap => ap.Id ==idApostador);
                    if(apostadorPuleDb != null)
                    {
                        apostador = apostadorPuleDb;
                        
                    }
                }
            }
            catch { return apostador; }
            return apostador;
        }

        internal static Pule? ReloadPule(Pule pule)
        {
            using DataBase db = new DataBase();
            Pule? puler = null;
            try
            {
                if(pule != null)
                {
                    var puleDb = db.Pules.FirstOrDefault(pu => pu.Id ==pule.Id);
                    if(puleDb != null)
                    {
                        puler = puleDb;
                        
                    }
                }
            }
            catch { return puler; }
            return puler;
        }
        /// <summary>
        /// Reloads the Apostador property from the database for the current instance.
        /// </summary>
        internal void ReloadApostador()
        {
            using DataBase db = new DataBase();
            try
            {
                if(this != null)
                {
                    var puleDb = db.Pules
                        .Include(p => p.Apostador)
                        .FirstOrDefault(p => p.Id == this.Id);
                    if (puleDb != null)
                        this.Apostador = puleDb.Apostador;
                }
            }
            catch { this.Apostador = null;}
        }
    }
}
