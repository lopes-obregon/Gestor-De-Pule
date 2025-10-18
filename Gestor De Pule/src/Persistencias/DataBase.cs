using Gestor_De_Pule.src.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestor_De_Pule.src.Persistencias
{
    internal class DataBase: DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Apostador> Apostadors { get; set; }
        public DbSet<Pule> Pules { get; set; }
       public string Dbpath { get; }
        public DataBase() {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Dbpath = System.IO.Path.Join(path, "pule.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={Dbpath}");
       
        
    }
}
