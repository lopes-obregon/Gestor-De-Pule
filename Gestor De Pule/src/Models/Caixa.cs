using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_De_Pule.src.Models
{
    internal class Caixa
    {
        public int Id { get; set; }
        public decimal Taxa { get; set; }
        public List<Disputa>? Disputs { get; set; }
        internal static  object GetCaixa()
        {
            using DataBase db = new DataBase();
            try
            {
                if(db.Caixas.Count() > 0)
                {
                    //existe um caixa registrado no sistema
                    var caixaDb = db.Caixas.Include(caix=>caix.Disputs).FirstOrDefault();
                    if (caixaDb != null)
                        return caixaDb;
                }
                else
                {
                    return new Caixa();
                }
            }
            catch { return new Caixa(); }
            return new Caixa();
        }

        internal void save()
        {
            using DataBase db = new DataBase();
            try
            {
                //melhor forma para rastrear o caixa
                var CaixaDb = db.Caixas.FirstOrDefault(cai => cai.Id == this.Id);
                if(CaixaDb != null)
                {
                    //atualiza para o valor atual
                    CaixaDb.Taxa = this.Taxa;
                    db.Caixas.Update(CaixaDb);
                }
                else
                {
                    //novo caixa sendo criado.
                    db.Caixas.Add(this);

                }
                db.SaveChanges();
            }
            catch { return; }
        }

        internal bool SaveWithDisputa(Disputa disputa)
        {
            using DataBase db = new DataBase();
            try
            {
                var disputaDb = db.Disputas
                    .FirstOrDefault(d => d.Id == disputa.Id);
                var caixaDb = db.Caixas.Include(caixa => caixa.Disputs).FirstOrDefault(caixa => caixa.Id == this.Id);
                if(disputaDb != null)
                {
                    disputaDb.Caixa = this;
                    if(caixaDb is not null)
                    {
                        if(caixaDb.Disputs is null)
                        {
                            caixaDb.Disputs = new List<Disputa>();
                            
                        }
                        else
                        {
                            caixaDb.Disputs.Add(disputaDb);
                            db.Caixas.Update(caixaDb);
                            db.Disputas.Update(disputaDb);


                        }

                    }
                }
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
