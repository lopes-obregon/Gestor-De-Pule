
using Gestor_De_Pule.src.Models;

namespace Gestor_De_Pule.src.Controllers
{
     internal  class FinanceiroController
    {
        public static  Caixa? Caixa {  set; get; } = null;
        public Caixa? CaixaLocal { set; get; } = null;
        public FinanceiroController() 
        {
           
        }
        internal static void InitCaixa()
        {
           if(Caixa is null)
            {
                var caixaDb = Caixa.GetCaixa();
                if (caixaDb is not null)
                    Caixa = (Caixa?)caixaDb;
            }

        }

        internal static void LoadCaixaInit()
        {
            if (Caixa is null)
                Caixa = Caixa.LoadInit();
        }
        internal void LoadCaixaLocal()
        {
            if(CaixaLocal is null)
            {
                CaixaLocal = Caixa.LoadInit();
            }
        }
        internal static void LoadCaixa()
        {
            if(Caixa is null)
            {
                Caixa = Caixa.Load();
            }
            else
            {
                Caixa = Caixa.Load(Caixa.Id);
            }
        }

        internal static void SaveOrAttTaxa(decimal taxa)
        {
            if(Caixa is not null && Caixa.Taxa != taxa){
                Caixa.Taxa = taxa;
                Caixa.save();
            }
        }

        internal static void OpenNewCaixa()
        {
            Caixa caixa = new Caixa();
            caixa.Open = Caixa.IsOpen.Open;
            caixa.DateOpen = DateTime.Now;
            caixa.save();

            Caixa = caixa;
        }

        
    }
}
