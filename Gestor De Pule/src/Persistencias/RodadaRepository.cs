using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
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
    }
}
