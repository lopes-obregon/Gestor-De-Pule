using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
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

        /// <summary>
        /// Attaches an Apostador entity to the current data context.
        /// </summary>
        /// <param name="apostador">The Apostador entity to attach.</param>
        internal void AttachApostador(Apostador apostador)
        {
            _context.Apostadors.Attach(apostador);
        }
        /// <summary>
        /// Attaches a Disputa entity to the current database context.
        /// </summary>
        /// <param name="disputa">The Disputa entity to attach.</param>
        internal void AttachDisputa(Disputa disputa)
        {
            _context.Disputas.Attach(disputa);
        }
    }
}
