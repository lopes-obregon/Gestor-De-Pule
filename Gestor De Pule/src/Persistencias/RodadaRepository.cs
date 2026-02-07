using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
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
