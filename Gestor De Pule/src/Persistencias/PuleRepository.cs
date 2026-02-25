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
            
            try
            {
                if (puleSelecionado is not null)
                {
                    _data.Pules.Attach(puleSelecionado);
                    foreach (var animal in puleSelecionado.Animais)
                    {
                        if (animal is not null)
                        {
                            _data.Animals.Attach(animal);
                            if (animal.Pules.Any(p => p.Id == puleSelecionado.Id))
                            {

                                animal.Pules.Remove(puleSelecionado);
                                _data.Animals.Update(animal);
                            }
                        }
                    }
                    puleSelecionado.Animais.Clear();
                    _data.Pules.Remove(puleSelecionado);
                    _data.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex){ return false;  Log.Error(ex, "Erro ao Remover O pule {Id}", puleSelecionado.Id); }
            return false;
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
                    .Include(p => p.Apostador)
                    .Include(p => p.Animais)
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
    }
}
