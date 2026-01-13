using Gestor_De_Pule.src.Controllers;
using Microsoft.VisualBasic;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Financeiro.FluxoCaixa
{
    /// <summary>
    /// Lógica interna para WindowFluxoCaixa.xaml
    /// </summary>
    public partial class WindowFluxoCaixa : Window
    {
        private FinanceiroController? financeiroController { get; set; } = null;
        public WindowFluxoCaixa()
        {
            InitializeComponent();
            financeiroController = new FinanceiroController();
            Init();
        }

        private void Init()
        {
            //FinanceiroController.LoadCaixaInit();
            LabelFluxoDeCaixa.Content = "FlUXO DE CAIXA - DATA: " + DateTime.Now.ToString("dd/MM/yyyy");
            if(financeiroController is not null)
            {
                financeiroController.LoadCaixaLocal();
                var caixa = financeiroController.CaixaLocal;
                if (caixa != null)
                {
                    //checar os pules pagos ou a quantidade que foram pagos
                   // caixa.TotalEmCaixaWithPulePago();
                    //método só para teste
                    LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: " + caixa.TotalEmCaixa.ToString("C");
                    LabelEntradaDeAposta.Content = "(+) Entradas de Apostas: " + caixa.GetEntradaDeApostas().ToString("C");
                    LabelPremioApagar.Content = "(-) Prêmios a Pagar: " + caixa.GetPremioApagar().ToString("C");
                    LabelLucro.Content = " (=) TAXA DA CASA (LUCRO): " + caixa.Lucro().ToString("C");
                    LabelInfo.Content = "TAXA CALCULADA DE: " + caixa.Taxa.ToString("P");
                }
                else
                {
                    LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: ---";
                    LabelEntradaDeAposta.Content = "(+) Entradas de Apostas: ";
                    LabelPremioApagar.Content = "(-) Prêmios a Pagar: ";
                    LabelLucro.Content = " (=) TAXA DA CASA (LUCRO): ";
                    LabelInfo.Content = "TAXA CALCULADA DE: ";
                }

            }
           // var caixa = FinanceiroController.Caixa;

        }

        private void FecharDia(object sender, RoutedEventArgs e)
        {
            var caixa = FinanceiroController.Caixa;
            if (caixa != null)
            {
                string mensagem = caixa.FecharDia();
                System.Windows.MessageBox.Show(mensagem);
            }
            else
            {
                System.Windows.MessageBox.Show("Algo deu Errado!");
            }
        }


        private void RetiraLucro(object sender, RoutedEventArgs e)
        {
            var caixa = FinanceiroController.Caixa;
            string valorStr = Interaction.InputBox("Quanto Deseja retirar?", "Retirada de Lucro", "0");
            if (caixa != null)
            {
                if(decimal.TryParse(valorStr, out decimal valor))
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show($"Você deseja retirar {valor.ToString("C")}?","Confirmação" ,System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        if (!caixa.RetirarLucro(valor))
                        {
                            System.Windows.MessageBox.Show("Saldo insuficiente, caso haja Lucro feche o caixa!");
                        }

                    }
                }

            }
        }

        private void PagarDisputa(object sender, RoutedEventArgs e)
        {
            WindowPagamentoDeDisputa window = new WindowPagamentoDeDisputa(financeiroController);
            window.ShowDialog();
            Init();
        }
    }
}
