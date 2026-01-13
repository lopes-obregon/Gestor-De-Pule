using Gestor_De_Pule.src.Persistencias;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Gestor_De_Pule.src.Models
{
    internal   class Caixa
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
                    var caixaDb = db.Caixas.OrderByDescending(c => c.Id).FirstOrDefault();
                    if(caixaDb is not null)
                    {
                        //pega o saldo do anterior e acresenta no novo caixa;
                        this.TotalEmCaixa = caixaDb.TotalEmCaixa;
                    }
                    if(this.DateOpen is null)
                    {
                        this.DateOpen = DateTime.Now;
                    }
                    if(this.Open is null)
                    {
                        this.Open = IsOpen.Open;
                    }
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

        internal decimal GetPremioApagar()
        {
            decimal total = 0.00m;
            decimal taxa = 0.00m;
            decimal prêmioLíquido = 0.00m;
            if(Disputs is not null && Disputs.Count > 0)
            {
                foreach (var disputa in Disputs)
                {
                    if(disputa is not null)
                    {
                        total += disputa.GetTotalValorPule();
                    }
                }
                taxa = total * Taxa;
                prêmioLíquido = total - taxa;
            }
            return prêmioLíquido;
        }

        internal decimal Lucro()
        {
            decimal total = 0.00m;
            decimal taxa = 0.00m;
            decimal prêmioLíquido = 0.00m;
            if (Disputs is not null && Disputs.Count > 0)
            {
                foreach (var disputa in Disputs)
                {
                    if (disputa is not null)
                    {
                        total += disputa.GetTotalValorPule();
                    }
                }
                taxa = total * Taxa;
                prêmioLíquido = total - taxa;
            }
            return taxa;
        }

        internal string FecharDia()
        {
            this.TotalEmCaixa += this.Lucro();
            if (Caixa.Fechar(this))
            {
                return "Caixa Fechado com sucesso na Data: " + DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                return "Erro ao fechar o caixa!";
            }
        }

        private static bool Fechar(Caixa caixa)
        {
            using DataBase db = new DataBase();
            bool fechou = false;
            try
            {
                var caixaDb = db.Caixas.FirstOrDefault(cai => cai.Id == cai.Id);
                if(caixaDb != null)
                {
                    caixaDb.TotalEmCaixa = caixa.TotalEmCaixa;
                    caixaDb.DateClose = DateTime.Now;
                    caixaDb.Open = IsOpen.Close;
                    db.Caixas.Update(caixaDb);
                    caixa = caixaDb;
                    db.SaveChanges();
                    fechou = true;
                }
            }
            catch { return fechou; }
            return fechou;
        }

        internal bool RetirarLucro(decimal valor)
        {
            bool retirou = false;
            decimal total = this.TotalEmCaixa + this.Lucro();
            if (total  > 0)
            {
                total -= valor;
                this.TotalEmCaixa = total;
                retirou = true;

            }
            return retirou;
            
        }

        internal void TotalEmCaixaWithPulePago()
        {
            decimal total = 0;
            if (Disputs is not null && Disputs.Count > 0)
            {
                foreach (var disputa in Disputs)
                    if (disputa is not null && disputa.Pagamento == Model.StatusPagamento.Pendente )
                        total += disputa.PulesPagos();

            }
            this.TotalEmCaixa += total;
           
        }

        internal List<Disputa> DisputsNãoPagos()
        {
            List<Disputa> disputs = new List<Disputa>();

            if(Disputs is not null && Disputs.Count > 0)
            {
                foreach(var disputa in Disputs)
                {
                    if(disputa is not null)
                    {
                        if(disputa.Pagamento == Model.StatusPagamento.Pendente)
                        {
                            disputs.Add(disputa);
                        }
                    }
                }
            }
            return disputs;
        }

        internal string PagaDisputa(object disputaSelecionadaUi, decimal valor)
        {
            bool sucess = false;
            string mensagem = string.Empty;
            Disputa? disputaSelecionado = disputaSelecionadaUi as Disputa;
            if (disputaSelecionado != null)
            {
                if (this.TotalEmCaixa > valor)
                {
                    disputaSelecionado.Pagamento = Model.StatusPagamento.Pago;
                    disputaSelecionado.TotalPago += valor;
                    sucess = disputaSelecionado.UpdatePagamentoEpagamento();
                    if (sucess)
                        mensagem = "Disputa Pago Com Sucesso!";
                    else
                        mensagem = "Erro ao Paga A Disputa!";
                    TotalEmCaixa -= valor;
                    sucess = this.UpdateTotalEmCaixa();
                    if (sucess)
                        mensagem += " Valor Debitado Do Caixa!";
                    else
                        mensagem += " Algo Deu Errado ao Debitar do Caixa!";

                }
                else { mensagem = "Saldo Insuficiente!"; }
            }
            else
                mensagem = "Disputa Inválida!";
            return mensagem;
        }

        private bool UpdateTotalEmCaixa()
        {
            using DataBase db = new DataBase();
            bool sucess = false;
            try
            {
                db.Attach(this);
                db.Caixas.Update(this);
                db.SaveChanges();
                sucess = true;
            }
            catch { sucess = false; }
            return sucess;
        }
    }
}
