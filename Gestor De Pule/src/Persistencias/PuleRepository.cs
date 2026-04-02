using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Persistencias
{
    class PuleRepository
    {
        private readonly DataBase _data;
  

        public PuleRepository()
        {
            _data = new DataBase();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                fileSizeLimitBytes: 10_000_000,
                rollOnFileSizeLimit: true)
                .CreateLogger();
                
        }

        public PuleRepository(object data)
        {
            _data =(DataBase) data;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                fileSizeLimitBytes: 10_000_000,
                rollOnFileSizeLimit: true)
                .CreateLogger();
        }

      

        /// <summary>
        /// Buscar pule por IDS
        /// </summary>
        /// <param name="puleSelecionadosUi"></param>
        /// <returns></returns>
        internal static List<Pule>? BuscarPorIds(object puleSelecionadosUi)
        {
            using DataBase db = new DataBase();
            List<int> ids = new();
           
            try
            {
                if (puleSelecionadosUi != null)
                {
                    if (puleSelecionadosUi is IList list)
                    {
                        foreach (var item in list)
                        {
                            var tipo = item.GetType();
                            var propId = tipo.GetProperty("Id");
                            if (propId != null)
                            {
                                var valorId = propId.GetValue(item);
                                if (valorId != null)
                                    ids.Add(int.Parse(valorId.ToString()));
                            }
                        }
                        var pulesDb = db.Pules
                            .Include(p => p.Animais)
                            .Include(p => p.Apostador)
                            .Include(p => p.Disputa)
                            .ThenInclude(dis => dis.ResultadoList)
                            .Where(p => ids.Contains(p.Id))
                            .ToList();
                        if (pulesDb != null)
                            return pulesDb;
                        else
                            return null;
                    }

                }
                else
                    return null;
            }catch { return null; }

            return null;
        }
       /// <summary>
       /// Associação do pule com os animais e disputas conrrespondentes
       /// </summary>
       /// <param name="apostadorSelecionado"></param>
       /// <param name="animais"></param>
       /// <param name="disputaSelecionado"></param>
       /// <param name="pule"></param>
       /// <returns></returns>
        internal bool Associete(Apostador? apostadorSelecionado, List<Animal> animais, Disputa? disputaSelecionado, Pule pule)
        {
            try
            {
                if (apostadorSelecionado is not null)
                {
                    //db.Apostadors.Attach(apostadorSelecionado);
                    apostadorSelecionado = _data.Apostadors.FirstOrDefault(a => a.Id == apostadorSelecionado.Id);
                    if (apostadorSelecionado != null)
                    {
                        
                        apostadorSelecionado.Pules.Add(pule);
                        pule.Apostador = apostadorSelecionado;
                        _data.Apostadors.Update(apostadorSelecionado);
                    }
                }
                //um ajuste de rastreio do ef core
                /*foreach(Animal animal in animais)
                {
                    if(animal is not null )
                        db.Animals.Attach(animal);
                }*/
                if (pule.Animais is not null)
                    pule.Animais.Clear();
                else
                    pule.Animais = new List<Animal>();
                if (animais is not null && animais.Count > 0)
                {
                    foreach (var ani in animais)
                    {
                        if (ani is not null)
                        {
                            var animal = _data.Animals.Find(ani.Id);
                            if (animal != null)
                            {
                                animal.Pules.Add(pule);
                                _data.Animals.Update(animal);
                                pule.Animais.Add(animal);
                            }
                        }
                    }

                }

                if (disputaSelecionado is not null)
                {
                    var disputaDb = _data.Disputas
                        .Include(d => d.Pules)
                        .Include(d => d.ResultadoList)
                        .FirstOrDefault(d => d.Id == disputaSelecionado.Id);
                    if (disputaDb is not null)
                    {
                        disputaDb.Pules.Add(pule);
                        _data.Disputas.Update(disputaDb);
                        pule.Disputa = disputaDb;
                    }
                }
                _data.Pules.Update(pule);
                _data.SaveChanges();
                return true;
            }
            catch(Exception ex ){ return false; Log.Error(ex, "Erro ao fazer associações ao pule;"); }
        }

        //Salva apenas o pule
        internal  bool SavePule(Pule pule)
        {
            
            try
            {
                if (pule is not null)
                {
                    
                    _data.Pules.Add(pule);
                    
                    _data.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex){ Log.Error(ex, "Erro ao cadastrar o Pule {Id}", pule.Id); return false; }
            return false;
        }
        /// <summary>
        /// Remove a Selected Pule
        /// </summary>
        /// <param name="puleSelecionado"></param>
        /// <returns></returns>
        internal  bool Remove(Pule puleSelecionado)
        {
            bool sucess = false;
            try
            {

                _data.Pules.Remove(puleSelecionado);
                _data.SaveChanges();
                sucess= true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao Remover O pule {Id}", puleSelecionado.Id);
                sucess=  false;
            }
            return sucess;
        }
        /// <summary>
        /// Atualizar o Pule com novos Animais;
        /// </summary>
        /// <param name="pule"></param>
        /// <param name="novosAnimais"></param>
        /// <param name="isEqual"></param>
        /// <returns></returns>
        internal  bool Update(Pule pule, List<Animal> novosAnimais, bool isEqual)
        {
            
            try
            {
                if (pule is not null)
                {
                    _data.Pules.Attach(pule);

                    //peque ajuste para mapear os dados novamente

                    if (pule.Apostador is not null)
                    {
                        _data.Apostadors.Attach(pule.Apostador);
                    }
                    if (pule.Animais is not null)
                    {
                        foreach (var aniamal in pule.Animais)
                        {
                            if (aniamal is not null)
                            {
                                _data.Animals.Attach(aniamal);
                            }
                        }
                    }
                    if (!isEqual)
                    {
                        //remove as associações do banco 
                        foreach (var animal in pule.Animais)
                        {
                            if (animal is not null)
                            {
                                _data.Animals.Attach(animal);
                                if (animal.Pules.Any(p => p.Id == pule.Id))
                                {
                                    animal.Pules.Remove(pule);
                                    _data.Animals.Update(animal);
                                }
                            }
                        }
                        //faz novas associações.
                        foreach (var animal in novosAnimais)
                        {
                            if (animal is not null)
                            {
                                _data.Animals.Attach(animal);
                                animal.Pules.Add(pule);
                                _data.Animals.Update(animal);
                            }
                        }
                        pule.Animais = novosAnimais;
                    }
                    //marca apenas o pule que alterei
                    //db.Entry(pule).State = EntityState.Modified;

                    _data.Pules.Update(pule);
                }
                _data.SaveChanges();
                return true;
            }
            catch(Exception ex) { return false; Log.Error(ex, "Error ao atualizar o Pule {Id}", pule.Id); }
        }
        /// <summary>
        /// Retrieves a list of Pule entities from the data source, including related Apostador and Animais entities.
        /// </summary>
        /// <returns>A list of Pule objects, or an empty list if an error occurs.</returns>
        internal  List<Pule> ReadPules()
        {
           
            try
            {
                return _data.Pules
                    .ToList();
            }
            catch(Exception ex) {
                Log.Error(ex, "Erro ao carregas Pules");
                return new List<Pule>(); }
        }

        internal Pule? IsTrack(Pule pule)
        {
            Pule? pule1 = null;
            try
            {
                bool isTrack = _data.ChangeTracker.Entries<Pule>().Any(e => e.Entity == pule);
                if (isTrack)
                    pule1 = pule;
                else
                    pule1 = _data.Pules.Include(p => p.Apostador)
                        .Include(p => p.Animais)
                        .Include(p => p.Disputa)
                        .FirstOrDefault(p => p.Id == pule.Id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar o pule {pule.Id}");
            }
            return pule1;
        }

        internal bool Save()
        {
            bool sucess = false;
            try
            {
                _data.SaveChanges();
                sucess = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao salvar o pule");
            }
            return sucess;
        }
        /// <summary>
        /// Add context pule
        /// </summary>
        /// <param name="pule"></param>
        internal void AddContext(Pule pule)
        {
            try
            {
                _data.Pules.Add(pule);

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao Adicionar o pule {pule.Id} ao contexto!");
            }
        }
        /// <summary>
        /// get pule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>pule or null</returns>
        internal Pule? GetPuleById(int id)
        {
            try
            {
                return _data.Pules.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar o pule {id}");
            }
            return null;
        }
        /// <summary>
        /// Procuro no cache do ef caso não ache procura no db com o id do pule fornescido
        /// </summary>
        /// <param name="idPule"></param>
        /// <returns>Animal list que pertence ao id do pule fornescido</returns>
        internal List<Animal>? GetAnimaisToPule(int idPule)
        {
            List<Animal> animals = null;
            try
            {
                var track = _data.ChangeTracker.Entries<Animal>().Select(e => e.Entity).Where(an => an.Pules.Any(p => p.Id == idPule)).ToList();
                if(track != null)
                {
                    animals = track;
                }
                else
                {
                    var db = _data.Animals.Where(an => an.Pules.Any(p => p.Id == idPule)).Include(a => a.Pules).ToList();
                    if (db is not null)
                    {
                        animals = db;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                var db = _data.Animals.Where(an=> an.Pules.Any(p=> p.Id == idPule)).Include(a=> a.Pules).ToList();
                if(db is not null)
                {
                    animals = db;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar os animais para o pule {idPule}");
            }
            return animals;
        }

        internal List<Pule> GetPulesToAnimal(int idAnimal)
        {
            List<Pule> pules = new();
            try
            {
                var track = _data.ChangeTracker.Entries<Pule>().Select(e => e.Entity).Where(p => p.Animais.Any(a => a.Id == idAnimal)).ToList();
                if (track is not null)
                    pules = track;
            }
            catch (ArgumentNullException)
            {
                var db = _data.Pules.Where(p => p.Animais.Any(a => a.Id == idAnimal)).ToList();
                if (db is not null)
                {
                    pules = db;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao tentar carregar os pules com id ");
            }
            return pules;
        }
        /// <summary>
        /// Verifica se tem pules rastreados caso não tenha consulta no banco
        /// </summary>
        /// <param name="idDisputa"></param>
        /// <returns>List de pules</returns>
    
        internal List<Pule>? GetPulesWithIdDisputs(int idDisputa)
        {
            List<Pule>? pules = null;
            try
            {
                var track = _data.ChangeTracker.Entries<Pule>().Select(e => e.Entity).Where(p => p.DisputaId == idDisputa).ToList();
                if ( track.Count > 0)
                    pules = track;
                else
                {
                    var db = _data.Pules.Where(_ => _.DisputaId == idDisputa).ToList();
                    if (db.Count > 0)
                        pules = db;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao tentar encontrar os pules relacionados com a disputa de id {idDisputa}");
                
            }
            return pules;
        }


        /// <summary>
        /// Retorna a lista de <see cref="Pule"/> associada a uma disputa pelo ID informado.
        /// Primeiro verifica se já existem entidades rastreadas pelo ChangeTracker com a coleção
        /// de animais carregada. Caso contrário, consulta o banco de dados incluindo os animais
        /// relacionados. Em caso de erro, registra no log e retorna null.
        /// </summary>
        /// <param name="idDisputa">Identificador único da disputa.</param>
        /// <returns>
        /// A lista de <see cref="Pule"/> encontrada com seus animais carregados,
        /// ou null se não houver registros ou ocorrer erro.
        /// </returns>
        internal List<Pule>? GetPulesWithIdDisputsAndAnimalInclude(int idDisputa)
        {
            List<Pule>? pules = null;
            try
            {
                var track = _data.ChangeTracker.Entries<Pule>().Select(e => e.Entity).Where(p => p.DisputaId == idDisputa).ToList();
                if (track.Count > 0 && track.Any(p => p.Animais != null))
                    pules = track;
                else
                {
                    var db = _data.Pules.Where(p => p.DisputaId == idDisputa).Include(p => p.Animais).ToList();
                    if (db.Count > 0)
                        pules = db;

                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Erro ao tentar carregar os pules relacionado a disputa id {idDisputa}");
            }
            return pules;
        }
        /// <summary>
        /// verify in track or Data base to seach 'Pules'
        /// </summary>
        /// <param name="disputsId"> unique <see cref="List"/> <see cref="int"/> <see cref="Disputa"/></param>
        /// <returns><see cref="List"/> <see cref="Pule"/></returns>
        internal List<Pule> LoadPulesAssociedDisputa(List<int> disputsId)
        {
            List<Pule> pules = new();
            try
            {
                var track = _data.ChangeTracker.Entries<Pule>().Select(e => e.Entity).Where(pu => disputsId.Any(id => pu.DisputaId == id)).ToList();
                if (track.Count > 0)
                    pules = track;
                else
                {
                    var db = _data.Pules.Where(pu => disputsId.Any(id => pu.DisputaId == id)).ToList();
                    if (db.Count > 0)
                        pules = db;
                }
            }
            catch(Exception e)
            {
                Log.Error(e, $"Erro ao tentar carregar as disputas da lista!");
            }
            return pules;
        }
        /// <summary>
        /// Retrieves a list of pules associated with the specified animal.
        /// </summary>
        /// <param name="animalId">The unique identifier of the animal.</param>
        /// <returns>A list of pules linked to the given animal. Returns an empty list if none are found.</returns>
        internal List<Pule> ReadPulesWithAnimal(int animalId)
        {
            List<Pule> pules = new();
            try
            {
                var track = _data.ChangeTracker.Entries<Pule>().Select(e => e.Entity).Where(p => p.Animais.Any(a => a.Id == animalId)).ToList();
                if (track.Count > 0) pules = track;
                else
                {
                   pules = LoadPulesWithAnimal(animalId);
                }
            }
            catch (ArgumentNullException)
            {
                pules = LoadPulesWithAnimal(animalId);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Erro ao carregar os pules do animal com id {animalId}");
            }
            return pules;
        }

        private List<Pule> LoadPulesWithAnimal(int animalId)
        {
            List<Pule> pules = new();
            var db = _data.Pules.Where(p => p.Animais.Any(a => a.Id == animalId)).Include(p=> p.Animais).ToList();
            if(db.Count > 0)
                { pules = db; }
            return pules;
        }
    }
}
