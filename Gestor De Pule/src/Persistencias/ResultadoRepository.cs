using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    class ResultadoRepository
    {
        private DataBase _db = new DataBase();
        public ResultadoRepository() {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(
               "logs/log.txt",
               rollingInterval: RollingInterval.Day, //cria um arquivo por dia
               retainedFileCountLimit: 7,//mantem 7 arquivos
               fileSizeLimitBytes: 10_000_000,//limite de 10 mb
               rollOnFileSizeLimit: true //cria novo arquivo quando chegar o limite
               )
               .CreateLogger();

        }
        public static bool Save(Resultado resultado)
        {
            using DataBase db = new DataBase();
            try
            {
                if(resultado is not null)
                {
                    var animalDb = db.Animals
                        .Include(a=>a.Resultados)
                        .FirstOrDefault(a=> a.Id == resultado.Animal.Id);
                    var resultadoDb = db.Resultados.Include(res=>res.Animal).FirstOrDefault(res=>res.Id == resultado.Id);

                    if (animalDb is not null && resultadoDb is not null)
                    {
                       if(animalDb.Id == resultado.Animal.Id)
                        {
                            //resultado.Animal = animalDb;
                            resultadoDb.Animal = animalDb;
                            db.Resultados.Add(resultadoDb);
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

        internal  bool Update(Resultado resultado)
        {
            bool sucess = false;
            
            try
            {
                if (resultado is not null)
                {
                    _db.Resultados.Update(resultado);
                    _db.SaveChanges();
                    sucess =  true;
                }
            }catch(Exception ex) { sucess =  false; Log.Error(ex, "Erro no resultado Id", resultado.Id); }
            return sucess;
        }

        internal  bool Update(Resultado? resultado, Disputa? disputa)
        {
            bool sucess = false;
            try
            {
                if(resultado is not null)
                {
                    var resultadoDb = _db.Resultados
                        .Include(res => res.Disputa)
                        .Include(res=>res.Animal)
                        .FirstOrDefault(res => res.Id == resultado.Id);
                    if(resultadoDb is not null)
                    {
                        if(disputa is not null)
                        {
                            var disputaDb = _db.Disputas
                                .Include(dis => dis.ResultadoList)
                                .FirstOrDefault(dis => dis.Id == disputa.Id);
                            if(disputaDb is not null)
                            {
                                resultadoDb.Disputa = disputaDb;
                                disputaDb.ResultadoList.Add(resultadoDb);
                                _db.Resultados.Update(resultadoDb);
                                _db.Disputas.Update(disputaDb);
                                disputa = disputaDb;
                                resultado = resultadoDb;
                                _db.SaveChanges();
                            }
                        }
                    }
                }
                sucess =  true;
            }catch (Exception ex){ sucess =  false; Log.Error(ex, "Error no Resultado {Id} e Disputa {Id}", resultado?.Id, disputa?.Id); }
            return sucess;
        }
    }
}
