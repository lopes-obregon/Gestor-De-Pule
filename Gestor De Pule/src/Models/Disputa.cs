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

       
    }
}
