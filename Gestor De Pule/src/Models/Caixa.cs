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
        public enum IsOpen {Open, Close}
        public int Id { get; set; }
        public decimal Taxa { get; set; }
        public List<Disputa>? Disputs { get; set; }
        public IsOpen? Open { get; set; }
        public DateTime? DateOpen { get; set; }
        public DateTime? DateClose { get; set; }
        public decimal  TotalEmCaixa { get; set; }
        internal static  object GetCaixa()
        {
            using DataBase db = new DataBase();
            try
            {
                if(db.Caixas.Count() > 0)
                {
                    //existe um caixa registrado no sistema
                    var caixaDb = db.Caixas.Include(caix=>caix.Disputs).FirstOrDefault(caix => caix.Open == IsOpen.Open);
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

        internal decimal GetSaldoTotal()
        {
            decimal total = 0;
            if (Disputs is not null && Disputs.Count > 0)
            {
                foreach (var disputa in Disputs)
                {
                    if (disputa is not null)
                    {
                        
                        total += disputa.GetTotalValorPule();
                        
                    }
                }
            }
            return total;
        }

        internal Caixa Load(int id)
        {
            using DataBase db = new DataBase();
           Caixa caixa = new Caixa();
            try
            {
                var caixaDb = db.Caixas.Include(cai => cai.Disputs).FirstOrDefault(cai => cai.Id == cai.Id);
                if (caixaDb is not null)
                    caixa = caixaDb;
            
                return caixa;
            }catch { return caixa; }
        }

        internal static Caixa? Load()
        {
            using DataBase db = new DataBase();
            Caixa? caixa = null;
            try
            {
                var caixaDb = db.Caixas.Include(cai => cai.Disputs).ThenInclude(dis=> dis.Pules).FirstOrDefault();
                if (caixaDb is not null)
                    caixa = caixaDb;
                
            }catch (Exception ex) { return caixa; }
            return caixa;
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

        internal static Caixa? LoadInit()
        {
            using DataBase db = new DataBase();
            Caixa caixa = null;
            try
            {
                var caixaDb = db.Caixas.Include(cai=>cai.Disputs).ThenInclude(dis=>dis.Pules).FirstOrDefault(cai => cai.Open == IsOpen.Open);
                if (caixaDb != null)
                {
                    caixa = caixaDb;
                }
            }
            catch (Exception ex) { return caixa; }
            return caixa;
        }

        internal decimal GetEntradaDeApostas()
        {
            decimal result = 0;
            if(Disputs is not null && Disputs.Count > 0)
            {
                foreach(var disputa in Disputs)
                {
                    if(disputa is not null)
                    {
                        result += disputa.GetTotalValorPule();
                    }
                }
            }
            return result;
        }
    }
}
