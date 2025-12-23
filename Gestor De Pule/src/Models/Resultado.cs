using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
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

        internal static Resultado? BuscarResultado(Resultado resultadoUi)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultadoUi is not null)
                {
                    var resultadoDb = db.Resultados
                        .Include(res => res.Disputa)
                        .Include(res => res.Animal)
                        .FirstOrDefault(res => res.Id == resultadoUi.Id);
                    if (resultadoDb is not null) return resultadoDb;
                    else return null;
                }
            }catch { return null; }
            return null;
        }
    }
}
