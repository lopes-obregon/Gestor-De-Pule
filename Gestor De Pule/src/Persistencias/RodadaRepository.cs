using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Microsoft.AspNetCore.Mvc.Razor.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class RodadaRepository
    {
        private readonly DataBase _dataBase;
        public RodadaRepository()
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
        public RodadaRepository(object data)
        {
            _dataBase = (DataBase)data;
        }

        internal void AddContext(Rodada? rodada)
        {
            try

            {

                //adiciona ao contexto
                if (rodada is not null)
                    _dataBase.Rodas.Add(rodada);
            }
            catch (Exception ex) { Log.Error(ex, $"Erro ao adicionar ao contexto a rodada {rodada?.Id}"); }
        }
        /// <summary>
        /// Dispose database
        /// </summary>
        internal void Clear()
        {
            _dataBase.Dispose();
        }

        internal Rodada? GetById(int? rodadaId)
        {
            Rodada? rodada = null;
            if (rodadaId != null)
            {
                try
                {
                    var track = _dataBase.ChangeTracker.Entries<Rodada>().Select(e => e.Entity).FirstOrDefault(r => r.Id == rodadaId);
                    if (track != null)
                    {
                        rodada = track;
                    }
                    else
                    {
                        var db = _dataBase.Rodas.FirstOrDefault(r => r.Id == rodadaId);
                        if(db != null)
                        {
                            rodada = db;
                        }
                    }
                }catch(Exception ex)
                {
                    Log.Error(ex, $"Erro ao tentar carregar a rodada");
                }
            }
            return rodada;
        }
        /// <summary>
        /// Obtem a rodada com o id da disputa fornescida
        /// </summary>
        /// <param name="idDisputa"></param>
        /// <returns>As RODADAS QUE TEM O ID DA DISPUTA ou null caso contrario</returns>
        internal List<Rodada>? GetByIdRodadas(int idDisputa)
        {
            List<Rodada> rodadas = null;
            try
            {
                var track = _dataBase.ChangeTracker.Entries<Rodada>().Select(e => e.Entity).Where(r => r.DisputaId == idDisputa).ToList();
                if (track is not null && track.Count > 0)
                    rodadas = track;
                else
                {
                    var db = _dataBase.Rodas.Where(r => r.DisputaId == idDisputa).ToList();
                    if(db != null)
                    {
                        rodadas = db;
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao tentar carregar a disputa com id {idDisputa}");
            }
            return rodadas;
        }

        internal Rodada? isTrack(Rodada? rodada)
        {
            Rodada? rodada1 = null;
            try
            {
                if(rodada is not null)
                {
                    var isTrack = _dataBase.ChangeTracker.Entries<Rodada>().Any(e => e.Entity == rodada);
                    if (isTrack)
                        rodada1 =  rodada;
                    else
                        rodada1 =  _dataBase.Rodas.FirstOrDefault(rod => rod.Id == rodada.Id);
                }
            }catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar a rodada {rodada?.Id}");
            }
            return rodada1;
        }

        internal Rodada? Load(int idDisputa)
        {
            Rodada? rodada = null;
            try
            {
                var rodadaTrack = _dataBase.Rodas
                    .Include(rod => rod.Disputa)
                    .Include(rod => rod.ResultadoDestaRodada)
                    .FirstOrDefault(rod => rod.Disputa.Id == idDisputa);
                if (rodadaTrack is  not null)
                    rodada = rodadaTrack;
                
            }catch(Exception ex) { Log.Error(ex, $"Erro ao buscar a rodada com o id de disputa {idDisputa}"); }
            return rodada;
        }

        internal Disputa? LoadDisputs(Rodada rodada)
        {
        Disputa? disputa = null;
            try
            {
                var rodadaDb = _dataBase.Rodas.Include(rod => rod.Disputa).FirstOrDefault(rod => rod.Id == rodada.Id);
                if (rodadaDb is not null && rodadaDb.Disputa is not null)
                {
                    disputa = rodadaDb.Disputa;
                }
            }catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar a disputa da rodada {rodada.Id}");
            }
            return disputa;
        }

        internal bool Save(Rodada? rodada)
        {
            bool sucess = false;
            try
            {
                if (rodada is not null)
                {
                    _dataBase.Rodas.Add(rodada);
                    _dataBase.SaveChanges();
                    sucess = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao cadastrar as Rodadas!");
            }
            return sucess;
        }

        internal void Update(Rodada rodada, Disputa disputa)
        {
            try
            {
                //garantir o rastreio do ef
                var disputaRastreada = _dataBase.Disputas.Local.FirstOrDefault(dis => dis.Id == disputa.Id);
                    if(disputaRastreada == null)
                        disputaRastreada = _dataBase.Disputas.Include(dis=> dis.ResultadoList).First(dis=> dis.Id == disputa.Id);
                if(! Object.ReferenceEquals(disputa, disputaRastreada) ){
                    //disputa = disputaRastreada;
                    _dataBase.Entry(disputaRastreada).State = EntityState.Detached;
                    _dataBase.Disputas.Attach(disputa);
                }
                var rodadaRastreada = _dataBase.Rodas.Local.FirstOrDefault(rod => rod.Id == rodada.Id) ?? _dataBase.Rodas.First(dis => dis.Id == rodada.Id);
                if (! Object.ReferenceEquals(rodadaRastreada, rodada))
                    rodada = rodadaRastreada;
                if(rodada.ResultadoDestaRodada is null)
                {
                    rodada.ResultadoDestaRodada = new List<Resultado>();
                    if(disputa.ResultadoList is not null)
                        rodada.ResultadoDestaRodada.AddRange(disputa.ResultadoList);
                }
                else
                {
                    if(disputa.ResultadoList != null)
                        rodada.ResultadoDestaRodada.AddRange(disputa.ResultadoList);
                }
                _dataBase.SaveChanges();
            }catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao atualizar a rodada {rodada.Id}");
            }
        }
    }
}
