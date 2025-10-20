using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Model
{
        public  enum StatusPagamento
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

        public Pule(Apostador? apostador, StatusPagamento pagamento, List<Animal> animais)
        {
            Apostador = apostador;
            _statusPagamento = pagamento;
            _date = DateTime.Now;
            _animais = animais;
        }

        //sett gett métodos
        public int Id { get { return _id; } set { _id = value; } }
        public Apostador? Apostador { get { return _apostador; } set { _apostador = value; } }
        public StatusPagamento StatusPagamento { get { return _statusPagamento; } set { _statusPagamento = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public List<Animal> Animais { get { return _animais; } set { _animais = value; } }

        internal static List<Pule> ReadPules()
        {
            using DataBase db = new DataBase();
            try
            {
                return db.Pules
                    .Include(p => p.Apostador)
                    .Include(p => p.Animais)
                    .ToList();
            }
            catch { return new List<Pule>(); }
        }

        internal static bool Save(Pule pule)
        {
            using DataBase db = new DataBase();
            try
            {
                if(pule is not null)
                {
                    Apostador? apostador = pule.Apostador;
                
                    if (apostador != null){
                        apostador.Pules.Add(pule);
                        db.Apostadors.Update(apostador);
                    }
                    foreach(Animal animal in pule.Animais)
                    {
                        if(animal is not null)
                        {
                            animal.Pules.Add(pule);
                            db.Animals.Update(animal);
                        }
                    }

                    db.Pules.Add(pule);
                }
                db.SaveChanges();
                return true;
            }
            catch {  return false; }

        }

        internal bool Update(Pule pule, List<Animal> novosAnimais, bool isEqual)
        {
            using DataBase db = new DataBase();
            try
            {
                if (pule is not null)
                {
                    db.Pules.Attach(pule);
          
                    //peque ajuste para mapear os dados novamente
                    
                        if(pule.Apostador is not null)
                        {
                            db.Apostadors.Attach(pule.Apostador);
                        }
                        if(pule.Animais is not null)
                        {
                            foreach(var aniamal in pule.Animais)
                            {
                                if(aniamal is not null)
                                {
                                    db.Animals.Attach(aniamal);
                                }
                            }
                        }
                    if (!isEqual)
                    {
                        //remove as associações do banco 
                        foreach (var animal in pule.Animais)
                        {
                            if(animal is not null)
                            {
                                db.Animals.Attach(animal);
                                if(animal.Pules.Any(p => p.Id == pule.Id))
                                {
                                    animal.Pules.Remove(pule);
                                    db.Animals.Update(animal);
                                }
                            }
                        }
                        //faz novas associações.
                        foreach(var animal in novosAnimais)
                        {
                            if(animal is not null)
                            {
                                db.Animals.Attach(animal);
                                animal.Pules.Add(pule);
                                db.Animals.Update(animal);
                            }
                        }
                        pule.Animais = novosAnimais;
                    }  
                        //marca apenas o pule que alterei
                        //db.Entry(pule).State = EntityState.Modified;
                    
                    db.Pules.Update(pule);
                }
                    db.SaveChanges();
                return true;
            }catch { return false; }
        }
    }
}
