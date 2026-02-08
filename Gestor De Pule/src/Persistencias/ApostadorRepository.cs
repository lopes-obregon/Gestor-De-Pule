using Gestor_De_Pule.src.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class ApostadorRepository
    {
        private readonly DataBase _dataBase;
        public ApostadorRepository()
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

        public ApostadorRepository(DataBase dataBase)
        {
            _dataBase = dataBase;
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

        internal Apostador? Load(Apostador? apostador)
        {
            Apostador? apostadorDb = null;
           if(apostador is not null)
            {
                try
                {
                    apostadorDb = _dataBase.Apostadors.Find(apostador.Id);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao tentar carregar o apostador do {Id}", apostador.Id);
                }
            }
           return apostadorDb;
        }

        internal List<Apostador> ReadApostadores()
        {
            List<Apostador> apostadors = new();
            try
            {
                var apostadoresDb = _dataBase.Apostadors.Include(a => a.Pules).ToList();
                if(apostadoresDb is not null)
                {
                    apostadors = apostadoresDb;
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"Erro ao carregar os apostadores");
            }
            return apostadors;
        }

        internal bool Remove(Apostador apostador)
        {
            try
            {
                _dataBase.Apostadors.Remove(apostador);
                _dataBase.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Log.Error(ex, "Erro ao remover o apostador {Id} - {Nome}", apostador.Id, apostador.Nome);
                return false;
            }
        }

        internal bool Save(Apostador apostador)
        {
            bool sucess = false;
            try
            {
                _dataBase.Apostadors.Add(apostador);
                _dataBase.SaveChanges();
                sucess = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao salvar o Apostador: {Nome}", apostador.Nome);
            }
            return sucess;
        }

        internal bool Update(Apostador apostador)
        {
            try
            {
                _dataBase.Apostadors.Update(apostador);
                _dataBase.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                Log.Error(ex, "Erro ao tentar carregar o apostador {Nome} ", apostador.Nome);
                return false;
            }
        }
    }
}
