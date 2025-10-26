using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Models
{
    class Resultado
    {
        public int Id { get; set; }
        public Disputa Disputa { get; set; } = new Disputa();
        public Animal Animal { get; set; } = new Animal();
        public TimeSpan Tempo { get; set; } = new TimeSpan();
        public byte Posição { get; set; }

        public Resultado() { }

        public Resultado(Animal animal)
        {
            Animal = animal;
        }

      
    }
}
