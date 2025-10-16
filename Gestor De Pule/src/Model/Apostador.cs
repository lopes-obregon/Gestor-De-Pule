using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Model
{
    internal class Apostador
    {
        //id do apostador
        int _id = 0;
        //nome 
        string _nome = String.Empty;
        //contato
        string _contato = String.Empty;
        //------------------------set e get --------------------------/
        //set e get do id
        public int Id { get { return _id; }  set { _id = value; } }
        public string Nome { get { return _nome; } set { _nome = value; } }
        public string Contato { get { return _contato; } set { _contato = value; } }
        public Apostador() { }

    }
}
