using Gestor_De_Pule.src.Persistencias;
using Gestor_De_Pule.src.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Models
{
    class Disputa
    {
        public int Id { get; set; }
        public DateTime DataEHora { get; set; } = new DateTime();
        public string Nome { get; set; } = String.Empty;
        public List<Resultado> ResultadoList { get; set; } = new List<Resultado>();
        public Disputa() { }
        public Disputa(string nome,  DateTime dataEHora,Resultado resultados)
        {
            Id = 0;
            DataEHora = dataEHora;
            Nome = nome;
            ResultadoList.Add(resultados);
        }

        internal  static Disputa? isCreate(string nomeDisputa)
        {
            Disputa? disputaDb;
            using DataBase db = new DataBase();
            disputaDb =  db.Disputas.FirstOrDefault(dis=> dis.Nome == nomeDisputa);
            if (disputaDb == null)
                return null;
            else
                return disputaDb;
            
        }
    }
}
