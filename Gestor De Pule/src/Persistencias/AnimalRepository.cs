
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        /// <summary>
        /// Busca o animal conforme o id fornescido
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns>O animal track ou db</returns>
        internal Animal? GetAnimalById(int animalId)
        {
            Animal animal = null;
            try
            {
                var track = _db.ChangeTracker.Entries<Animal>().Select(e => e.Entity).FirstOrDefault(a => a.Id == animalId);
                if (track is not null)
                    animal = track;
                else
                {
                    var db = _db.Animals.FirstOrDefault(a => a.Id == animalId);
                    if (db is not null)
                        animal = db;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao tentar buscar o animal com o id {animalId}");
            }
            return animal;
        }
        /// <summary>
        /// Verifica se tem animais rastreados caso tenha procura pelo os ids fornecido na lista, caso não tenha procura no banco
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        /// <returns>Retorna a lista dos animais pesquisados</returns>
        internal List<Animal>? GetAnimalByIdList(List<int> animaisSelecionados)
        {
            List<Animal>? animals = null;
            try
            {
                //verificar se existe os animais no rastreio
                var track = _db.ChangeTracker.Entries<Animal>().Select(e => e.Entity).Where(a => animaisSelecionados.Contains(a.Id)).Distinct().ToList();
                if (track is not null && track.Count > 0)
                    animals = track;
                else
                {
                    var db = _db.Animals.Where(a => animaisSelecionados.Contains(a.Id)).ToList();
                    if(db is not null &&  db.Count > 0) animals = db;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao tentar Carregar os animais da lista fornecida!");
            }
            return animals;
        }

        /// <summary>
        /// verifica em track se tem rastreado os animais, caso não tenha pesquisa no banco.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List de animais</returns>
        internal List<Animal>? GetAnimalsByIdPule(int id)
        {
            List<Animal> animals = null;
            try
            {
                var track = _db.ChangeTracker.Entries<Animal>().Select(e => e.Entity).Where(a => a.Pules.Any(pu => pu.Id == id)).ToList();
                if (track is not null)
                    animals = track;
                else
                {
                    var db = _db.Animals.Where(a => a.Pules.Any(pu => pu.Id == id)).ToList();
                    if (db is not null && db.Count > 0)
                        animals = db;
                }
            }
            catch (ArgumentNullException)
            {
                var db = _db.Animals.Where(a => a.Pules.Any(pu => pu.Id == id)).ToList();
                if (db is not null && db.Count > 0)
                    animals = db;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao tentar carregar os animais relacionado ao pule de id {id}");
            }
            return animals;
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
        /// <summary>
        /// Procua os animais conforme a lista forncescida;
        /// </summary>
        /// <param name="animaisSelecionados"></param>
        /// <returns>List de animais encontrados</returns>
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
        /// <summary>
        /// Load animais with pules
        /// </summary>
        /// <returns>List animal with pules</returns>
        internal List<Animal> LoadAnimaisWithPules()
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                var animais = _db.Animals.Include(a => a.Pules).ToList();
                if (animais != null)
                    animals = animais;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao tentar carregar os animais!");
            }
            return animals;
        }
        /// <summary>
        /// Carrega os animais conforme suas relações com os pules fornescidos
        /// </summary>
        /// <param name="pulesIds"></param>
        /// <returns>List animal</returns>
        internal List<Animal>? LoadAnimaisWithPules(List<int> pulesIds)
        {
            List<Animal> animais = null;
            try
            {
                var track = _db.ChangeTracker.Entries<Animal>().Select(e => e.Entity).Where(a => a.Pules.Any(p => pulesIds.Contains(p.Id))).ToList();
                if (track.Count > 0)
                    animais = track;
                else
                {
                    var db  = _db.Animals.Where(a => a.Pules.Any(p => pulesIds.Contains(p.Id))).ToList();
                    if (db.Count > 0)
                        animais = db;
                }
            }
            catch (NullReferenceException)
            {
                var db = _db.Animals.Where(a => a.Pules.Any(p => pulesIds.Contains(p.Id))).ToList();
                if (db.Count > 0)
                    animais = db;
            }
            catch (Exception ex){
                Log.Error(ex, "Erro ao tentar buscar os animais!");

            }
            return animais;
        }

        internal Animal? LoadAnimalWithResultado(Animal? animal)
        {
            Animal? animal1 = null;
            try
            {
                if(animal is not null)
                {
                    var db = _db.Animals.Include(an => an.Resultados).FirstOrDefault(a => a.Id == animal.Id);
                    if (db != null)
                        animal1 = db;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar o animal {animal.Id} - {animal.Nome}");
            }
            return animal1;
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
                    var track = _db.ChangeTracker.Entries<Animal>().Select(e=> e.Entity).ToList();
                    if(track is not null && track.Count > 0)
                        animals = track.ToList();
                    else
                    {
                        animals = _db.Animals
                            .ToList();

                        
                    }
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
