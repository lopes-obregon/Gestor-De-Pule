using Gestor_De_Pule.src.Controllers;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Financeiro.FluxoCaixa
{
    /// <summary>
    /// Lógica interna para WindowFluxoCaixa.xaml
    /// </summary>
    public partial class WindowFluxoCaixa : Window
    {
        public WindowFluxoCaixa()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            FinanceiroController.LoadCaixaInit();
            var caixa = FinanceiroController.Caixa;
            LabelFluxoDeCaixa.Content = "FlUXO DE CAIXA - DATA: " + DateTime.Now.ToString("dd/MM/yyyy");
            if (caixa != null)
            {
                LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: " + caixa.TotalEmCaixa.ToString("C");
                LabelEntradaDeAposta.Content = "(+) Entradas de Apostas: " + caixa.GetEntradaDeApostas();
            }
            else
            {
                LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: ---";
            }
        }
    }
}
