using Gestor_De_Pule.Migrations;
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
        private FinanceiroController? _financeiroController { get; set; } = null;
        public WindowFluxoCaixa()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// Init component <see cref="_financeiroController"/> to write message in window.
        /// </summary>
        private void Init()
        {
            _financeiroController = new FinanceiroController();
            //FinanceiroController.LoadCaixaInit();
            LabelFluxoDeCaixa.Content = "FlUXO DE CAIXA - DATA: " + DateTime.Now.ToString("dd/MM/yyyy");
            if(_financeiroController is not null)
            {
                _financeiroController.InitCaixa();
                _financeiroController.LoadDisputs();
                var caixa = _financeiroController.Caixa;
                if (!_financeiroController.CaixaIsNull())
                {
                    //checar os pules pagos ou a quantidade que foram pagos
                    // caixa.TotalEmCaixaWithPulePago();
                    //método só para teste
                    //LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: " + caixa.TotalEmCaixa.ToString("C");
                    LabelSaldoTotalCaixa.Content = "SALDO TOTAL EM CAIXA: " + _financeiroController.GetTotalEmCaixa();
                    //LabelEntradaDeAposta.Content = "(+) Entradas de Apostas: " + caixa.GetEntradaDeApostas().ToString("C");
                    LabelEntradaDeAposta.Content = "(+) Entradas de Apostas: " + _financeiroController.GetEntradaDeApostas();
                    //LabelPremioApagar.Content = "(-) Prêmios a Pagar: " + caixa.GetPremioApagar().ToString("C");
                    LabelPremioApagar.Content = "(-) Prêmios a Pagar: " + _financeiroController.PremioApagar();
                    //LabelLucro.Content = " (=) TAXA DA CASA (LUCRO): " + caixa.Lucro().ToString("C");
                    LabelLucro.Content = " (=) TAXA DA CASA (LUCRO): " + _financeiroController.Lucro();
                    //LabelInfo.Content = "TAXA CALCULADA DE: " + caixa.Taxa.ToString("P");
                    LabelInfo.Content = "TAXA CALCULADA DE: " + _financeiroController.GetTaxa();
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
        /// <summary>
        /// Button call <see cref="FecharDia(object, RoutedEventArgs)"/>, with a message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FecharDia(object sender, RoutedEventArgs e)
        {
            string mensagem = String.Empty;
            if (_financeiroController != null)
            {

                mensagem = _financeiroController.FecharDia();
                System.Windows.MessageBox.Show(mensagem);
            }
            else
            {
                System.Windows.MessageBox.Show("Algo deu Errado!");
            }
           /* var caixa = _financeiroController.Caixa;
            if (caixa != null)
            {
                mensagem = caixa.FecharDia();
                System.Windows.MessageBox.Show(mensagem);
            }
            else
            {
                System.Windows.MessageBox.Show("Algo deu Errado!");
            }*/
        }


        private void RetiraLucro(object sender, RoutedEventArgs e)
        {
            string valorStr = String.Empty;
            if (_financeiroController != null)
            {
                //var caixa = _financeiroController.Caixa;
                valorStr = Interaction.InputBox("Quanto Deseja retirar?", "Retirada de Lucro", "0");

                if (decimal.TryParse(valorStr, out decimal valor))
                {
                    System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show($"Você deseja retirar {valor.ToString("C")}?", "Confirmação", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (!_financeiroController.RetirarLucro(valor))
                        {
                            System.Windows.MessageBox.Show("Saldo insuficiente, caso haja Lucro feche o caixa!");
                        }
                        /*if (!caixa.RetirarLucro(valor))
                        {
                            System.Windows.MessageBox.Show("Saldo insuficiente, caso haja Lucro feche o caixa!");
                        }*/

                    }
                }

            }
        }
        /// <summary>
        /// Create a new instance to window <see cref="WindowPagamentoDeDisputa"/>.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagarDisputa(object sender, RoutedEventArgs e)
        {
            _financeiroController?.Dispose();
            WindowPagamentoDeDisputa window = new WindowPagamentoDeDisputa();
            window.ShowDialog();
            Init();
        }
    }
}
