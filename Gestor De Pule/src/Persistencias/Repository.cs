using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class Repository
    {
        private DataBase _context;
        public Repository(DataBase data)
        {
            _context = data;
        }
        public Repository()
        {
            _context = new DataBase();
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
        //consulta no banco ou rastreia
        public DataBase GetDataBase() { return _context; }
    }
}
