using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    class DisputaRepository
    {
        private DataBase _dataBase = new();
        public DisputaRepository()
        {
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
        internal static Disputa? Exist(string nomeDisputa)
        {
            using var db = new DataBase();

            return db.Disputas
                
                .FirstOrDefault(dis => dis.Nome == nomeDisputa);
        }

        internal  Disputa? ReadDisputa(Disputa disputaSelecionado)
        {
            
            try
            {
                if (disputaSelecionado == null) return null;
                return _dataBase.Disputas
                    .Include(d => d.ResultadoList)
                    .ThenInclude(res=> res.Animal)
                    .FirstOrDefault(dis=> dis.Id == disputaSelecionado.Id);
                
            }
            catch { return null; }
        }

        internal  List<Disputa> ReadDisputas()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Disputas
                    .Include(d => d.ResultadoList)
                    .Where(dis=> !string.IsNullOrEmpty(dis.Nome))
                    .ToList();
            }
            catch { return new List<Disputa>(); }
        }
        /// <summary>
        /// Remove uma disputa que foi selecionado na Ui
        /// </summary>
        /// <param name="disputaSelecionado"></param>
        /// <returns></returns>
        internal  bool Remove(Disputa disputaSelecionado)
        {
            
            try
            {
                var disputaDb = _dataBase.Disputas
                    .Include(d => d.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .FirstOrDefault(d=> d.Id ==  disputaSelecionado.Id);
                if (disputaDb != null)
                {
                    foreach (var resultado in disputaDb.ResultadoList.ToList()) //tolist cópia segura
                    {
                        if (resultado is not null)
                        {
                            //tira a associação dos dados para essa disputa selecionado;
                            if (resultado.Disputa.Id == disputaSelecionado.Id)
                            {
                                resultado.Disputa = new();//remove associação   
                                _dataBase.Resultados.Update(resultado);
                                _dataBase.SaveChanges();
                            }
                        }
                    }
                    _dataBase.Disputas.Remove(disputaDb);
                    _dataBase.SaveChanges();
                }
                return true;
            }
            catch (Exception ex){ return false;  Log.Error(ex, "Error ao Remor a disputa!"); }
        }
        /// <summary>
        /// Saves the specified <see cref="Disputa"/> instance to the database.
        /// </summary>
        /// <param name="disputa">The <see cref="Disputa"/> object to be saved. Cannot be null.</param>
        /// <returns><see langword="true"/> if the <see cref="Disputa"/> was successfully saved; otherwise, <see
        /// langword="false"/>.</returns>
        internal static bool Save(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                if(disputa is not null)
                {
                   
                    foreach(Resultado resultado in disputa.ResultadoList)
                    {
                        Animal animal = resultado.Animal;
                        //verifica se o animal existe no banco de dados
                        var local = db.Animals.Local.FirstOrDefault(a=> a.Id == animal.Id);
                        if (local != null)
                            resultado.Animal = local; //usa o rastreado;
                        else
                            db.Animals.Attach(animal); //anexa como existente
                    }
                    db.Disputas.Add(disputa);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
            return false;
        }

        internal  bool  UpdateDisputa(Disputa disputa, List<Animal> animaisSelecionadosUi)
        {
           
            try
            {
                if(disputa is not null)

                {
                    //db.Disputas.Attach(disputa);
                    List<Resultado> resultadosUi = disputa.ResultadoList.ToList();
                    if(animaisSelecionadosUi is not null && animaisSelecionadosUi.Count > 0)
                    {
                        var ids = animaisSelecionadosUi.Select(a=> a.Id).ToList();
                        var animaisDb = _dataBase.Animals.Where(a=>ids.Contains(a.Id)).ToList();
                        foreach(var animal in animaisDb)
                        {
                            if(animal is not null)
                            {
                                //Acho que está errado pensar melhor 
                                //senãrio onde não existe uma disputa
                                bool temEssaDisputa = animal.Resultados.Any(ar => resultadosUi.Any(rui => rui.Id == ar.Id));
                                if (!temEssaDisputa)
                                {
                                    Resultado resultado = resultadosUi.Find(res => res.Disputa.Id == disputa.Id) ?? new Resultado();
                                    _dataBase.Resultados.Attach(resultado);
                                    _dataBase.Entry(resultado).State = EntityState.Modified;
                                    if (resultado.Disputa is null)
                                        resultado.Disputa = disputa;
                                    resultado.Animal = animal;
                                    
                                    animal.Resultados.Add(resultado);
                                    _dataBase.Update(animal);
                                    _dataBase.Update(resultado);

                                }
                                else
                                {
                                    //caso onde a disputa exista.
                                    //procuro onde essa disputa caso com o resultado ou seja
                                    //onde o resultado -> disputa
                                    Resultado resultadoToDisputa = resultadosUi.Find(res=> res.Disputa.Id==disputa.Id) ?? new Resultado();
                                    //caso onde o animal -> resultado;
                                    Resultado? resultadoAnimalToResultado = animal.Resultados.Find(ar => ar.Id == resultadoToDisputa.Id);
                                    //verificando se é o mesmo resultado
                                    if(resultadoAnimalToResultado == null && resultadoToDisputa is null)
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }
                    _dataBase.Update(disputa);
                    _dataBase.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex) { return false; Log.Error(ex, "ERROR AO ATUALIZAR DISPITA!"); }
            return false;
        }
        /// <summary>
        /// Update disputa;
        /// </summary>
        /// <param name="disputa"></param>
        /// <param name="animaisSelecionadosUi"></param>
        /// <param name="animaisRemovidos"></param>
        /// <returns></returns>
        internal  bool UpdateDisputa(Disputa disputa, List<Animal> animaisSelecionadosUi, List<Animal> animaisRemovidos)
        {
            
            try
            {
                if(disputa is not null)
                {
                    var disputaDb = _dataBase.Disputas
                        .Include(d => d.ResultadoList)
                        .FirstOrDefault(d => d.Id == disputa.Id);
                    if (disputa.Nome != disputaDb.Nome)
                        disputaDb.Nome = disputa.Nome;
                    if(!disputa.DataEHora.Equals(disputaDb.DataEHora))
                        disputaDb.DataEHora = disputa.DataEHora;
                    //trataremos dos animais removidos
                    if(animaisRemovidos.Count > 0)
                    {
                        // se for maior que zero então temos animais removidos
                        foreach(var animal in animaisRemovidos)
                        {
                            if(animal is not null)
                            {
                                _dataBase.Animals.Attach(animal);
                                var resultadoDoAnimal = animal.Resultados.Find(res => res.Disputa.Id == disputaDb.Id);
                                if(resultadoDoAnimal is not null)
                                {
                                    _dataBase.Resultados.Attach(resultadoDoAnimal);
                                    //encontramos a disputa devemos remover ela da relação
                                    // no caso o resultado
                                    resultadoDoAnimal.Disputa = new Disputa();
                                    _dataBase.Resultados.Update(resultadoDoAnimal);
                                    disputa.ResultadoList.Remove(resultadoDoAnimal);

                                }
                            }
                        }
                    }
                    if(animaisSelecionadosUi.Count > 0)
                    {
                        foreach (var animal in animaisSelecionadosUi)
                        {
                            var animalDb = _dataBase.Animals
                                .Include(a=> a.Resultados)
                                .ThenInclude(r=>r.Disputa)
                                .FirstOrDefault(a=> a.Id == animal.Id);
                            // Verifica se o animal já tem um resultado vinculado a essa disputa
                            bool jaParticipa = animalDb.Resultados.Any(res => res.Disputa.Id == disputaDb.Id);

                            if (!jaParticipa)
                            {
                                // Verifica se o contexto já está rastreando esse animal
                                /*var local = db.Animals.Local.FirstOrDefault(a => a.Id == animal.Id);
                                if (local == null)
                                {
                                    db.Animals.Attach(animal); // começa a rastrear
                                }*/

                                // Cria o novo resultado e vincula à disputa e ao animal
                                var resultado = new Resultado(animalDb)
                                {
                                    Disputa = disputaDb
                                };
                                // Adiciona o resultado ao contexto
                                _dataBase.Resultados.Add(resultado);

                                // Atualiza as relações
                                animalDb.Resultados.Add(resultado);
                                disputaDb.ResultadoList.Add(resultado);

                                _dataBase.Animals.Update(animalDb);
                            }
                        }

                        // Atualiza a disputa (se necessário)
                        _dataBase.Disputas.Update(disputaDb);
                        _dataBase.SaveChanges();
                    }
                }
                return true;
            }catch {  return false; }
        }

        /// <summary>
        /// Verifica se essa disputa já foi criada!
        /// </summary>
        /// <param name="nomeDisputa"></param>
        /// <returns></returns>
        internal  Disputa? isCreate(string nomeDisputa)
        {
            Disputa? disputaDb;
            
            disputaDb = _dataBase.Disputas.FirstOrDefault(dis => dis.Nome == nomeDisputa);
            if (disputaDb == null)
                return null;
            else
                return disputaDb;

        }
        /// <summary>
        /// Retrieves a list of Disputa entities from the database, including related ResultadoList, Animal, Pules, and
        /// Apostador data, filtering out entries with empty or null names.
        /// </summary>
        /// <returns>A list of Disputa objects if found; otherwise, null.</returns>
        internal List<Disputa>? GetDisputas()
        {
           
            try
            {
                var disputasDb = _dataBase.Disputas
                    .Include(d => d.ResultadoList)
                    .ThenInclude(r => r.Animal)
                    .Include(d => d.Pules)
                    .ThenInclude(p => p.Apostador)
                    .Where(d => !String.IsNullOrEmpty(d.Nome))
                    .ToList();
                if (disputasDb is null || disputasDb.Count == 0)
                {
                    return null;
                }
                else
                {
                    return disputasDb;
                }
            }
            catch (Exception ex) { return null; Log.Error(ex, "Error ao obter Disputas"); }
        
    }

       
    }
}
