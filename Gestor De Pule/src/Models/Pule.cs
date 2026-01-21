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
        public int Id { get; set; }
        public Apostador? Apostador { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public DateTime Date { get; set; }
        public List<Animal>? Animais { get; set; }
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
            StatusPagamento = pagamento;
            Date = DateTime.Now;
            Animais = animais;
            Valor = valor;
            Número = númeroDoPule;
        }

        //sett gett métodos

       

       

        
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
