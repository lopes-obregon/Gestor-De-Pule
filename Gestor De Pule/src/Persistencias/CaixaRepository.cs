using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Gestor_De_Pule.src.Models.Caixa;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class CaixaRepository
    {
        private readonly DataBase _db;
        public CaixaRepository() {
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

        public CaixaRepository(object data)
        {
            _db = (DataBase)data;
        }

        /// <summary>
        /// Retrieves the currently open Caixa from the database, including its associated Disputs, or returns a new
        /// Caixa if none are open.
        /// </summary>
        /// <returns>An instance of Caixa representing the open caixa, or a new Caixa if none are found.</returns>
        internal  Caixa GetCaixa()
        {
           Caixa caixa = new Caixa();
            try
            {
                if (_db.Caixas.Count() > 0)
                {
                    //existe um caixa registrado no sistema
                    var caixaDb = _db.Caixas.Include(caix => caix.Disputs).FirstOrDefault(caix => caix.Open == IsOpen.Open);
                    if (caixaDb != null)
                        caixa =  caixaDb;
                    else
                    {
                        _db.Caixas.Add(caixa);
                    }
                }
                
            }
            catch (Exception ex) { Log.Error(ex, "Erro ao procurar os caixas");  }
            return caixa;
        }
        /// <summary>
        /// Associates a Disputa with the current Caixa and saves changes to the database.
        /// </summary>
        /// <param name="disputa">The Disputa entity to associate and save.</param>
        /// <returns>True if the operation succeeds; otherwise, false.</returns>
        internal bool SaveWithDisputa(Disputa disputa, Caixa caixa)
        {
            bool sucess = false;

            try
            {
                // garante que disputa é a instância rastreada
                var disputaRastreada = _db.Disputas.Local
                    .FirstOrDefault(d => d.Id == disputa.Id)
                    ?? _db.Disputas.Find(disputa.Id);

                disputa = disputaRastreada;

                // garante que caixa também é a instância rastreada
                var caixaRastreada = _db.Caixas.Local
                    .FirstOrDefault(c => c.Id == caixa.Id)
                    ?? _db.Caixas.Find(caixa.Id);

                caixa = caixaRastreada;

                if (caixa.Disputs is null)
                {
                    caixa.Disputs = new List<Disputa>();
                }

                // só adiciona se não existir
                if (!caixa.Disputs.Any(dis => dis.Id == disputa.Id))
                {
                    caixa.Disputs.Add(disputa);
                }

                // não precisa chamar Update manualmente se já estão rastreados
                // o EF já sabe que houve mudança na coleção
                _db.SaveChanges();
                sucess = true;
            }
            catch (Exception ex)
            {
                sucess = false;
                Log.Error(ex, "Erro ao associar o caixa com disputa!");
            }

            return sucess;
        }
        /// <summary>
        /// Carrega o caixa que está aberto;
        /// </summary>
        /// <returns></returns>
        internal  Caixa? LoadInit()
        {
            
            Caixa caixa = null;
            try
            {
                var caixaDb = _db.Caixas.Include(cai => cai.Disputs).ThenInclude(dis => dis.Pules).FirstOrDefault(cai => cai.Open == IsOpen.Open);
                if (caixaDb != null)
                {
                    caixa = caixaDb;
                }
            }
            catch (Exception ex) { return caixa; Log.Error(ex, "Erro ao iniciar o caixa"); }
            return caixa;
        }

        internal void Save(Caixa caixa)
        {
            
            try
            {
                //melhor forma para rastrear o caixa
                var CaixaDb = _db.Caixas.FirstOrDefault(cai => cai.Id == caixa.Id);
                if (CaixaDb != null)
                {
                    //atualiza para o valor atual
                    CaixaDb.Taxa = caixa.Taxa;
                    _db.Caixas.Update(CaixaDb);
                }
                else
                {
                    var caixaDb = _db.Caixas.OrderByDescending(c => c.Id).FirstOrDefault();
                    if (caixaDb is not null)
                    {
                        //pega o saldo do anterior e acresenta no novo caixa;
                        caixa.TotalEmCaixa = caixaDb.TotalEmCaixa;
                    }
                    if (caixa.DateOpen is null)
                    {
                        caixa.DateOpen = DateTime.Now;
                    }
                    if (caixa.Open is null)
                    {
                        caixa.Open = IsOpen.Open;
                    }
                    //novo caixa sendo criado.
                    _db.Caixas.Add(caixa);

                }
                _db.SaveChanges();
            }
            catch (Exception ex){ return; Log.Error(ex, "Erro ao salvar o caixa {Id}", caixa.Id); }
        }
        /// <summary>
        /// Retrieves the list of disputes associated with the specified cash register.
        /// </summary>
        /// <param name="caixa">The cash register whose disputes are to be loaded.</param>
        /// <returns>A list of disputes for the given cash register, or null if not found.</returns>
        internal List<Disputa>? LoadDisputs(Caixa caixa)
        {
            List<Disputa>? disputas = null;
            try
            {
                var caixaDb = _db.Caixas.Include(cai => cai.Disputs).FirstOrDefault(_ => _.Id == caixa.Id);
                if (caixaDb is not null)
                    disputas = caixaDb.Disputs.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Erro ao adicionar disputas ao caixa: {caixa.Id}");
            }
            return disputas;
        }

        internal void Save()
        {
            try
            {
                _db.SaveChanges();
            }catch(Exception ex){ Log.Error(ex, "Erro ao atualizar ou salvar os dados do caixa!"); }
        }

        internal Caixa? GetCaixaWithDisput(int id)
        {
            Caixa? caixa = null;
            try
            {
                var track = _db.ChangeTracker.Entries<Caixa>().Select(e => e.Entity).FirstOrDefault(caixa => caixa.Open == IsOpen.Open);
                if (track is not null)
                    caixa = track;
                else
                {
                    var db = _db.Caixas.Where(_ => _.Open == IsOpen.Open).Select(c => new Caixa()
                    {
                        Id = c.Id,
                        TotalEmCaixa = c.TotalEmCaixa,
                        Taxa = c.Taxa,
                        Disputs = c.Disputs.Where(d => d.Id == id).ToList()
                    }).FirstOrDefault();
                    if (db is not null)
                        caixa = db;
                }
            }catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao Carregar o caixa para a disputa {id}");
            }
            return caixa;
        }
    }
}
