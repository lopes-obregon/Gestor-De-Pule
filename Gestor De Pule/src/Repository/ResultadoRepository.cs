using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src
{
    class ResultadoRepository
    {
        public static bool Save(Models.Resultado resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultado is not null)
                {
                    var animalDb = db.Animals
                        .Include(a=>a.Resultados)
                        .FirstOrDefault(a=> a.Id == resultado.Animal.Id);
                    if (animalDb is not null)
                    {
                       if(animalDb.Id == resultado.Animal.Id)
                        {
                            resultado.Animal = animalDb;
                            db.Resultados.Add(resultado);
                            db.SaveChanges();
                            return true;
                        }
                        //resultado.Animal = db.Animals.Find(resultado.Animal.Id);
                    }
                }
            }
            catch {  return false; }
            return false;
        }

        internal static List<Resultado> ReadResultados(List<Resultado> resultadoList)
        {
            using DataBase db = new DataBase();
            List<Resultado> resultadosMapeados = new List<Resultado>();
            try
            {
                if (resultadoList is not null && resultadoList.Count > 0)
                {
                    foreach (Resultado resultado in resultadoList)
                    {
                        if (resultado is not null)
                        {
                            var resultadoDb = db.Resultados.Include(res=> res.Animal).FirstOrDefault(res=> res.Id == resultado.Id);
                            if (resultadoDb is not null)
                                resultadosMapeados.Add(resultadoDb);

                        }
                    }
                    if (resultadosMapeados.Count > 0)
                        return resultadosMapeados.ToList();
                    else return new List<Resultado>();
                }
            }
            catch { return  resultadoList; }
            return new List<Resultado>();
        }

        internal static bool Update(Resultado resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if (resultado is not null)
                {
                    db.Resultados.Update(resultado);
                    db.SaveChanges();
                    return true;
                }
            }catch { return false; }
            return false;
        }

        internal static bool Update(Resultado? resultado, Disputa? disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultado is not null)
                {
                    var resultadoDb = db.Resultados
                        .Include(res => res.Disputa)
                        .Include(res=>res.Animal)
                        .FirstOrDefault(res => res.Id == resultado.Id);
                    if(resultadoDb is not null)
                    {
                        if(disputa is not null)
                        {
                            var disputaDb = db.Disputas
                                .Include(dis => dis.ResultadoList)
                                .FirstOrDefault(dis => dis.Id == disputa.Id);
                            if(disputaDb is not null)
                            {
                                resultadoDb.Disputa = disputaDb;
                                disputaDb.ResultadoList.Add(resultadoDb);
                                db.Resultados.Update(resultadoDb);
                                db.Disputas.Update(disputaDb);
                                disputa = disputaDb;
                                resultado = resultadoDb;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }catch { return false; }
        }
    }
}
