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
            decimal totalPulesPagos = 0;
            decimal total = 0;
            decimal totalDisputaPagos = Decimal.Zero;
            decimal retirada = Decimal.Zero;
            if (Disputs is not null && Disputs.Count > 0)
            {
                foreach (var disputa in Disputs){
                    if (disputa is not null)
                    {
                        if (disputa.Pagamento == Model.StatusPagamento.Pendente)
                            totalPulesPagos += disputa.PulesPagos();
                        else if (disputa.Pagamento == Model.StatusPagamento.Pago)
                            totalDisputaPagos += disputa.TotalPago ?? 0;


                    }
                }

            }
            total = totalPulesPagos - totalDisputaPagos;
            // se der igual não houve retirada
            if(total != TotalEmCaixa)
            {
                //quantia que retirou 
                retirada = total - TotalEmCaixa;
                total -= retirada;
                //fica igual
                if(!(total ==  TotalEmCaixa))
                    //se não for igual existe saldo anterior
                    this.TotalEmCaixa = total;

            }
           
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
                if (disputaSelecionado.TotalPago is null)
                    disputaSelecionado.TotalPago = Decimal.Zero;
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

        internal bool Update()
        {
            using DataBase db = new DataBase();
            bool sucss = false;
            try
            {
                if(this is not null)
                {
                    db.Caixas.Attach(this);
                    db.Caixas.Update(this);
                    db.SaveChanges();
                    sucss = true;
                }
            }catch { sucss = false; }
            return sucss;
        }

        internal void Associete(Disputa? disputa)
        {
            if (this.Disputs is null && disputa is not null)
                this.Disputs = new List<Disputa>() { disputa };
            else
                if(disputa is not null && Disputs is not null)
                    if(!Disputs.Contains(disputa))
                        this.Disputs?.Add(disputa);
        }
    }
}
