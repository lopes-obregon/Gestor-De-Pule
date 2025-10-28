using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Repository
{
    class DisputaRepository
    {
        internal static Disputa? ReadDisputa(Disputa disputaSelecionado)
        {
            using DataBase db = new DataBase();
            try
            {
                if (disputaSelecionado == null) return null;
                return db.Disputas.Include(d => d.ResultadoList).First();
                
            }
            catch { return null; }
        }

        internal static List<Disputa> ReadDisputas()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas.Include(d => d.ResultadoList).ToList();
            }
            catch { return new List<Disputa>(); }
        }

        internal static bool Save(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                    List<Resultado> resultados = disputa.ResultadoList.ToList();
                    foreach(Resultado resultado in resultados)
                    {
                        if (resultado is not null)
                        {
                           db.Attach(resultado);
                            db.Entry(resultado).State = EntityState.Modified;
                           db.Attach(resultado.Animal);
                            db.Entry(resultado.Animal).State = EntityState.Modified;
                        }
                    }
                    db.Disputas.Add(disputa);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;
        }

        internal static bool  UpdateDisputa(Disputa disputa, List<Animal> animaisSelecionadosUi)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                    if(animaisSelecionadosUi is not null && animaisSelecionadosUi.Count > 0)
                    {
                        var ids = animaisSelecionadosUi.Select(a=> a.Id).ToList();
                        var animaisDb = db.Animals.Where(a=>ids.Contains(a.Id)).ToList();
                        foreach(var animal in animaisDb)
                        {
                            if(animal is not null)
                            {
                                //senãrio onde não existe uma disputa
                                bool temEssaDisputa = animal.Resultados.Exists(r => r.Disputa.Id == disputa.Id);
                                if (!temEssaDisputa)
                                {
                                    animal.Resultados.Add
                                }
                            }
                        }
                    }
                }
            }
            catch { return false; }
        }
    }
}
