using Gestor_De_Pule.src.Models;

namespace Gestor_De_Pule.src.Controllers
{
    internal class FinanceiroController
    {
        public static  Caixa? Caixa {  set; get; } = null;

        internal static void InitCaixa()
        {
           if(Caixa is null)
            {
                var caixaDb = Caixa.GetCaixa();
                if (caixaDb is not null)
                    Caixa = (Caixa?)caixaDb;
            }

        }

        internal static void SaveOrAttTaxa(decimal taxa)
        {
            if(Caixa is not null && Caixa.Taxa != taxa){
                Caixa.Taxa = taxa;
                Caixa.save();
            }
        }
    }
}
