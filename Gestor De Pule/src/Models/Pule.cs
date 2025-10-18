using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Model
{
    public enum StatusPagamento
    {
        Pendente,
        Pago,
        Cancelado
    }
    internal class Pule
    {
        //menbros
        private int _id = 0;
        private Apostador _apostador = new();
        private StatusPagamento _statusPagamento;
        private DateTime _date = DateTime.Now;
        private List<Animal> _animais = new();
        //construct
        public Pule() { }
        //sett gett métodos
        public int Id { get { return _id; } set { _id = value; } }
        public Apostador Apostador { get { return _apostador; } set { _apostador = value; } }
        public StatusPagamento StatusPagamento { get { return _statusPagamento; } set { _statusPagamento = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public List<Animal> Animais { get { return _animais; } set { _animais = value; } }

    }
}
