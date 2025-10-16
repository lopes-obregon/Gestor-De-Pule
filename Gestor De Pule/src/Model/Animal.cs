using System.Reflection.Metadata.Ecma335;

namespace Gestor_De_Pule.src.Model
{
    internal class Animal
    {
        private int _id = 0;
        private string _name = "";
        private string _Proprietário = "";
        private string _Treinador = "";
        private string _Jockey = "";
        private string _cidade = "";
        //publics métodos
        //retorna e seta a o id
        public string Id { get { return Id; } set { Id = value; } }
        //retorna e seta o nome
        public string Nome { get { return _name; } set { _name = value; } }
        //set e get proprietário
        public string Proprietário { get { return _Proprietário; } set { _Proprietário= value; } }
        //set e get treinador
        public string Treinador { get { return _Treinador; } set { _Treinador = value; } }
        //set e get jock
        public string Jockey { get { return _Jockey; } set { _Jockey = value; } }
        //set e get cidade
        public string Cidade { get { return _cidade; } set { _cidade = value; } }
        
        //construct
        public Animal() { }
    
    }
}
