using Gestor_De_Pule.src.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Gestor_De_Pule.src.Models.Caixa;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class CaixaRepository
    {
        private readonly DataBase _db = new DataBase();
        public CaixaRepository() {
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
            bool sucess  = false;
            try
            {
                var disputaDb = _db.Disputas
                    .FirstOrDefault(d => d.Id == disputa.Id);
                var caixaDb = _db.Caixas.Include(caixa => caixa.Disputs).FirstOrDefault(caixa => caixa.Id == this.Id);
                if (disputaDb != null)
                {
                    disputaDb.Caixa = caixa;
                    if (caixaDb is not null)
                    {
                        if (caixaDb.Disputs is null)
                        {
                            caixaDb.Disputs = new List<Disputa>();

                        }
                        else
                        {
                            caixaDb.Disputs.Add(disputaDb);
                            _db.Caixas.Update(caixaDb);
                            _db.Disputas.Update(disputaDb);


                        }

                    }
                }
                _db.SaveChanges();
                sucess =  true;
            }
            catch (Exception ex){ sucess =  false; Log.Error(ex, "Algo de erado para associar o caixa com disputa!"); }
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

    }
}
