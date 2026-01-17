
using Gestor_De_Pule.src.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class AnimalRepository
    {
        private readonly DataBase _db;
        
        public AnimalRepository() { 
            
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

        internal Animal? Consultar(Animal? animalSelecionado)
        {
            Animal? animal = null;
            if(_db is not null)
            {
                
                try
                {
                    if (animalSelecionado is not null)
                        animal = _db.Animals.Find(animalSelecionado.Id);

                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Erro ao consultar o Animal: {Id} -  {Nome}", animalSelecionado.Id, animalSelecionado.Nome);
                }
            }
            return animal;
        }

        internal bool Delete(Animal animal)
        {
            bool sucess = false;
            try
            {
                if (_db is not null)
                {
                    _db.Animals.Remove(animal);
                    _db.SaveChanges();
                    sucess = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao Excluir o animal: {Id} - {Nome}", animal.Id, animal.Nome);
                
            }
            return sucess;
        }

        internal List<Animal> ReadAnimals()
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                if (_db is not null)
                {
                    animals = _db.Animals.Include(a => a.Pules)
                        .Include(a => a.Resultados)
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao obter Animais");
                throw;
            }
            return animals;
        }

        internal bool Save(Animal animal)
        {
            bool success = false;
            if(_db is not null)
            {
                try
                {
                    _db.Animals.Add(animal);
                    _db.SaveChanges();
                    success = true;
                }catch(Exception ex)
                {
                    Log.Error(ex, "Erro ao Salvar o Animal {Nome}", animal.Nome);

                }
            }
                return success;
        }

        internal bool Update(Animal? animal)
        {
            bool sucess = false;
            if(animal is not null)
            {
                try
                {
                    _db.Animals.Update(animal);
                    _db.SaveChanges();
                    sucess = true;

                }catch(Exception ex)
                {
                    Log.Error(ex, "Erro ao Atualizar o Animal {Nome}", animal.Nome);

                }
            }
            return sucess;
        }
    }
}
