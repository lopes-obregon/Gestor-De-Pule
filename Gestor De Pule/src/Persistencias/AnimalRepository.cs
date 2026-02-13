
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class AnimalRepository
    {
        private readonly DataBase _db;

        public AnimalRepository() { 
            
            _db = new DataBase();
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
        public AnimalRepository(DataBase dataBase)
        {
            _db = dataBase;
        }

        public AnimalRepository(object data)
        {
            _db = (DataBase)data;
        }
        /// <summary>
        /// Disposes of the underlying database resource.
        /// </summary>
        internal void Clear()
        {
            _db.Dispose();
        }

        internal Animal? Consultar(Animal? animalSelecionado)
        {
            Animal? animal = null;
            if(_db is not null)
            {
                
                try
                {
                    if (animalSelecionado is not null)
                    {
                        var animalEmMemória = _db.Animals.Local.FirstOrDefault(a=> a.Id == animalSelecionado.Id);
                        if (animalEmMemória is null)
                            animal = _db.Animals.Find(animalSelecionado.Id);
                        else
                            animal = animalEmMemória;
                    }

                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao consultar o Animal: {Id} -  {Nome}", animalSelecionado.Id, animalSelecionado.Nome);
                }
            }

            return animal;
        }

        internal bool Delete(Animal animal)
        {
            bool sucess = false;
            try
            {
                if (_db is not null)
                {
                    _db.Animals.Remove(animal);
                    _db.SaveChanges();
                    sucess = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao Excluir o animal: {Id} - {Nome}", animal.Id, animal.Nome);
                
            }
            return sucess;
        }

        internal List<Pule> GetPules(Animal animal)
        {
            List<Pule> pules = new();
            try
            {
                var animalDb = _db.Animals.Include(a => a.Pules).FirstOrDefault(a => a.Id == a.Id);
                if (animalDb is not null)
                    pules = animalDb.Pules;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar os pules do animal {animal.Id} - {animal.Nome}");
            }
            return pules;
        }

        /// <summary>
        /// Returns the tracked Animal entity if it is being tracked; otherwise, retrieves the Animal from the database
        /// including related Resultados and Disputa entities.
        /// </summary>
        /// <param name="animalUi">The Animal entity to check for tracking.</param>
        /// <returns>The tracked Animal entity, the entity from the database if not tracked, or null if not found or on error.</returns>
        internal Animal? IsTracked(Animal animalUi)
        {
            try
            {
                bool isTracked = _db.ChangeTracker.Entries<Animal>()
                    .Any(e => e.Entity == animalUi);
                if (isTracked)
                    return animalUi;
                else
                    return _db.Animals.Include(an=>an.Resultados).ThenInclude(res=> res.Disputa)
                        .Include(an=>an.Pules).FirstOrDefault(an=> an.Id == animalUi.Id);
            }
            catch (Exception ex) { Log.Error(ex, $"Erro ao tentar encontrar {animalUi.Id} -  {animalUi.Nome}"); }
            return null;
        }

        internal List<Animal> LoadAnimais(ListBox.ObjectCollection animaisSelecionados)
        {
            var animaisUi = animaisSelecionados.Cast<Animal>().ToList();
            var animaisTrack = new List<Animal>();
            if(animaisUi is not null && animaisUi.Count > 0)
            {
                try
                {
                    foreach(var animal in animaisUi)
                    {
                        var entry = _db.ChangeTracker.Entries<Animal>().FirstOrDefault(e => e.Entity.Id == animal.Id);
                        if(entry != null)
                        {
                            //já rastreado
                            animaisTrack.Add(entry.Entity);
                        }
                        else
                        {
                            //não temos rastreo devemos procurar no banco
                            var animalDb = _db.Animals.Find(animal.Id);
                            if(animalDb != null)
                            {
                                animaisTrack.Add(animalDb);
                            }
                        }
                    }
                    return animaisTrack;
                }catch (Exception ex)
                {
                    Log.Error(ex, $"Erro ao localizar animais");
                }
            }
                return animaisTrack;
        }

        internal List<Resultado> LoadResultados(Animal animal)
        {
            List<Resultado> resultados = new();
            try
            {
                var animalDb = _db.Animals.Include(_ => _.Resultados).FirstOrDefault(_ => _.Id == animal.Id);
                if (animalDb != null)
                    resultados = animalDb.Resultados;
            }catch (Exception ex)
            {
                Log.Error(ex, $"erro ao carregar os resultados do animal: {animal.Id} - {animal.Nome}");
            }
            return resultados;
        }

        internal List<Animal> ReadAnimals()
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                if (_db is not null)
                {
                    animals = _db.Animals
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao obter Animais");
                throw;
            }
            return animals;
        }

        internal bool Save(Animal animal)
        {
            bool success = false;
            if(_db is not null)
            {
                try
                {
                    _db.Animals.Add(animal);
                    _db.SaveChanges();
                    success = true;
                }catch(Exception ex)
                {
                    Log.Error(ex, "Erro ao Salvar o Animal {Nome}", animal.Nome);

                }
            }
                return success;
        }
        /// <summary>
        /// Updates the specified animal in the database.
        /// </summary>
        /// <param name="animal">The animal entity to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        internal bool Update(Animal? animal)
        {
            bool sucess = false;
            if(animal is not null)
            {
                try

                {
                    var entry = _db.Entry(animal);
                    if(entry.State == EntityState.Modified)
                    {
                        _db.SaveChanges();
                        sucess = true;

                    }
                    else if (_db.ChangeTracker.Entries<Resultado>().Any(entry=> entry.State == EntityState.Modified))
                    {
                        _db.SaveChanges();
                        sucess = true;
                    }
                    
                    

                }catch(Exception ex)
                {
                    Log.Error(ex, "Erro ao Atualizar o Animal {Nome}", animal.Nome);

                }
            }
            return sucess;
        }
        internal  bool Update(Animal? animal, Resultado? resultado)
        {
            
            try
            {
                if (animal is not null)
                {
                    var animalDb = _db.Animals
                        .Include(a => a.Resultados)
                        .ThenInclude(res => res.Disputa)
                        .FirstOrDefault(a => a.Id == animal.Id);
                    if (animalDb is not null)
                    {
                        if (resultado is not null)
                        {
                            var resultadoDb = _db.Resultados
                                .Include(res => res.Animal)
                                .Include(res => res.Disputa)
                                .FirstOrDefault(res => res.Id == resultado.Id);
                            if (resultadoDb is not null)
                            {
                                animalDb.Resultados.Add(resultadoDb);
                                _db.Animals.Update(animalDb);
                                _db.SaveChanges();
                            }
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }
    }
}
