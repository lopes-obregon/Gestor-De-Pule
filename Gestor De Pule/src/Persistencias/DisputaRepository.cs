using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    class DisputaRepository
    {
        private DataBase _dataBase;
       // public DataBase DataBase { get { return _dataBase; } }
        public DisputaRepository()
        {
            _dataBase = new DataBase();
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

        public DisputaRepository(DataBase data)
        {
            
            _dataBase = data;
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
                
                bool isTracked = _dataBase.ChangeTracker.Entries<Disputa>().Any(e => e.Entity == disputaSelecionado); //verifica se está rastreado
                if(isTracked) return disputaSelecionado;
                else
                    return _dataBase.Disputas
                        .Include(d => d.ResultadoList)
                        .ThenInclude(res=> res.Animal)
                        .FirstOrDefault(dis=> dis.Id == disputaSelecionado.Id);
                
            }
            catch { return null; }
        }
        /// <summary>
        /// Retrieves a list of Disputa entities from the database, including their associated ResultadoList.
        /// </summary>
        /// <returns>A list of Disputa objects, or null if an error occurs.</returns>
        internal  List<Disputa>? ReadDisputas()
        {
            
            try
            {
                return _dataBase.Disputas
                    .Include(d => d.ResultadoList)
                    //.Where(dis=> !string.IsNullOrEmpty(dis.Nome))
                    .ToList();
            }
            catch { return null; }
        }
        /// <summary>
        /// Remove uma disputa que foi selecionado na Ui
        /// </summary>
        /// <param name="disputaSelecionado"></param>
        /// <returns></returns>
        internal  bool Remove(Disputa disputaSelecionado)
        {
            bool sucess = false;
            
            try
            {
                bool isTrack = _dataBase.ChangeTracker.Entries<Disputa>().Any(e=> e.Entity == disputaSelecionado);
                if (isTrack)
                {
                    _dataBase.Disputas.Remove(disputaSelecionado);

                }
                else
                {
                    var disputaDb = _dataBase.Disputas.FirstOrDefault(_ => _.Id == disputaSelecionado.Id);
                    if (disputaDb != null)
                    {
                        _dataBase.Disputas.Remove(disputaDb);
                    }

                }
                
                    _dataBase.SaveChanges();
                    sucess = true;
            }
            catch (Exception ex){ Log.Error(ex, "Error ao Remor a disputa!"); }
            return sucess;
        }
        /// <summary>
        /// Saves the specified <see cref="Disputa"/> instance to the database.
        /// </summary>
        /// <param name="disputa">The <see cref="Disputa"/> object to be saved. Cannot be null.</param>
        /// <returns><see langword="true"/> if the <see cref="Disputa"/> was successfully saved; otherwise, <see
        /// langword="false"/>.</returns>
   

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
        internal Disputa? isCreate(string nomeDisputa)
        {
            // tenta pegar do Local (já rastreado)
            var disputaDb = _dataBase.Disputas.Local
                .FirstOrDefault(dis => dis.Nome == nomeDisputa);

            if (disputaDb != null)
                return disputaDb;

            // se não está no Local, busca no banco
            return _dataBase.Disputas
                .Include(dis => dis.ResultadoList)
                .FirstOrDefault(dis => dis.Nome == nomeDisputa);
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

        internal void UpdateTempo(object animalNome, TimeSpan tempoUi, Resultado resUi, Disputa disputa)
        {
            // string? animalNomeStr = animalNome.ToString();
            
            try
            {
                var disputaDb = _dataBase.Disputas
                    .Include(dis => dis.ResultadoList)
                    .ThenInclude(res => res.Animal)
                    .FirstOrDefault(dis => dis.Id == disputa.Id);
                if (disputaDb is not null)
                {
                    var resultadoDb = _dataBase.Resultados
                        .Include(res => res.Animal)
                        .Include(res => res.Disputa)
                        .FirstOrDefault(res => res.Id == resUi.Id && res.Disputa.Id == disputa.Id);
                    if (resultadoDb is not null)
                    {
                        if (resultadoDb.Animal.isAnimalMesmoNome(animalNome))
                        {
                            resultadoDb.Tempo = tempoUi;
                            _dataBase.Resultados.Update(resultadoDb);
                        }
                    }
                    _dataBase.SaveChanges();
                }
            }
            catch (Exception ex) { Log.Error(ex, "Erro ao atualizar a disputa com tempo"); }
        }

        internal Disputa? Reload(Disputa disputa)
        {
            
            try
            {
                if (disputa is not null)
                {
                    var disputaDb = _dataBase.Disputas.Include(dis => dis.Pules)
                        .ThenInclude(pu => pu.Animais).Include(dis => dis.Caixa).Include(dis => dis.ResultadoList)
                        .ThenInclude(res => res.Animal).Include(dis => dis.Pules).ThenInclude(pu => pu.Apostador).FirstOrDefault(dis => dis.Id == disputa.Id);
                    if (disputaDb != null)
                    {
                        return disputaDb;

                    }
                }
                return null;
            }
            catch(Exception ex) { return null;  Log.Error(ex, "Erro ao carregar disputa da memória"); }
        }

        internal bool UpdateDisputa(Disputa disputa)
        {
            bool sucess = false;
           if(this is not null)
            {
                try
                {
                    //ajuste de rastreamento
                    foreach(var resultado in disputa.ResultadoList)
                    {
                        if(resultado is not null)
                        {
                            if(resultado.Animal is not null)
                            {
                                var animalMemória = _dataBase.Animals.Local.FirstOrDefault(a => a.Id == resultado.Animal.Id)  ?? _dataBase.Animals.Find(resultado.Animal.Id);
                                //verifica se está em memória a instancia
                                if (animalMemória != null)
                                {
                                    if(!ReferenceEquals(animalMemória, resultado.Animal))
                                    {
                                        //subistituir pelo objeto já rastreado
                                        resultado.Animal = animalMemória;
                                    }
                                }

                            }
                        }
                    }

                    _dataBase.SaveChanges();
                    sucess = true;

                }catch(Exception ex) { Log.Error(ex, $"Erro ao atualizar a disputa {disputa.Id} - {disputa.Nome}"); }
            }
           return sucess;
        }
        /// <summary>
        /// Adds a Disputa entity to the database context if it is not null.
        /// </summary>
        /// <param name="disputa">The Disputa entity to add to the context.</param>
        internal void AddContext(Disputa? disputa)
        {
            try
            {
                if (disputa is not null)
                    _dataBase.Disputas.Add(disputa);
            }catch(Exception ex) { Log.Error(ex, $"Erro ao adicionar ao contexto a disputa: {disputa?.Id} - {disputa?.Nome}"); }
        }

        internal bool Save(Disputa? disputa)
        {
            bool sucess = false;
            try
            {
                var entry = _dataBase.Entry(disputa);
                Console.WriteLine(entry.State);
               // CheckForDuplicates(disputa);
                _dataBase.SaveChanges();
                sucess = true;
            }catch (Exception ex) { Log.Error(ex, $"Erro ao salvar A disputa"); }
            return sucess;

        }

        private void CheckForDuplicates(Disputa? disputa)
        {
            var duplicatas = _dataBase.ChangeTracker.Entries<Disputa>()
    .GroupBy(e => e.Entity.Id)
    .Where(g => g.Count() > 1)
    .ToList();

            foreach (var grupo in duplicatas)
            {
                Console.WriteLine($"Disputa duplicada Id={grupo.Key}, Count={grupo.Count()}");

                foreach (var entry in grupo)
                {
                    Console.WriteLine($" -> Ref={entry.Entity.GetHashCode()}, State={entry.State}");
                }
            }
        }

        internal void CheckDuplicate()
        {
            var duplicatas = _dataBase.ChangeTracker.Entries<Disputa>()
    .GroupBy(e => e.Entity.Id)
    .Where(g => g.Count() > 1);

            foreach (var grupo in duplicatas)
            {
                Console.WriteLine($"⚠️ Disputa duplicada Id={grupo.Key}, Count={grupo.Count()}");
                foreach (var entry in grupo)
                {
                    Console.WriteLine($" -> Ref={entry.Entity.GetHashCode()}, State={entry.State}");
                }
            }
        }

        internal object? GetByAnimalId(int id)
        {
            try
            {
                return _dataBase.Animals.Find(id);
            }
            catch (Exception ex) { Log.Error(ex, $"Erro ao consultar o animal {id}"); }
            return null;
        }

        internal void AddContext(Resultado resultado)
        {
            try
            {
                _dataBase.Resultados.Add(resultado);
            }catch(Exception ex) { Log.Error(ex, $"Erro ao adiconar ao contexto o resultado {resultado.Id}"); }
        }

        internal bool Save()
        {
            bool sucess = false;
            try
            {
                _dataBase.SaveChanges();
                sucess = true;
            }catch(Exception ex) { Log.Error(ex, $"Eo ao aplicar as alterações geral"); }
            return sucess;
        }
    /// <summary>
    /// Retrieves a tracked Disputa entity from the context or loads it with related data if not already tracked.
    /// </summary>
    /// <param name="disputa">The Disputa entity to track or retrieve.</param>
    /// <returns>The tracked Disputa entity, or null if not found.</returns>
        internal Disputa? Track(Disputa? disputa)
        {
            Disputa? disputa1 = null;
            try
            {
                bool isTrack = _dataBase.ChangeTracker.Entries<Disputa>().Any(e => e.Entity == disputa);
                if (isTrack)
                {
                    disputa1 = disputa;
                }
                else
                {
                    disputa1 = _dataBase.Disputas
                        .Include(_=> _.Caixa)
                        .Include(_=> _.Pules)
                        .Include(_=> _.ResultadoList)
                        .Include(_=> _.Rodadas)
                        .FirstOrDefault(_=> _.Id == disputa.Id);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar a disputa: {disputa?.Id} - {disputa?.Nome}");
            }
            return disputa1;
        }
    }
}
