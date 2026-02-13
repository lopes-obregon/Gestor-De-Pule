using Gestor_De_Pule.src.Controllers;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Gestor_De_Pule.src.Views.Financeiro.FluxoCaixa
{
    /// <summary>
    /// Lógica interna para WindowPagamentoDeDisputa.xaml
    /// </summary>
    public partial class WindowPagamentoDeDisputa : Window
    {
        private  FinanceiroController? _financeiroController = null;
        public WindowPagamentoDeDisputa()
        {
            InitializeComponent();
            
            initCampos();

        }

        public WindowPagamentoDeDisputa(object? financeiroController)
        {
            this._financeiroController = financeiroController as FinanceiroController;
            InitializeComponent();
            initCampos();
        }

        private void initCampos()
        {
            _financeiroController = new FinanceiroController();
            

            //financeiroController.LoadCaixaLocal();
            if (_financeiroController is not null && _financeiroController.Caixa is not null && _financeiroController.Caixa.Disputs is not null)
            {
                var disputas = _financeiroController.Caixa.DisputsNãoPagos();
                if (disputas != null)
                {

                    ComboBoxDisputasCadastradas.ItemsSource = disputas;
                }

            }

        }

        private void TextBoxNumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, NumberStyles.Number,new CultureInfo("pt-BR"), out _); // só aceitua nº
        }

        private void PagarDisputa(object sender, RoutedEventArgs e)
        {
            var disputaSelecionadaUi = ComboBoxDisputasCadastradas.SelectedItem;
            string mensagem = String.Empty;
            decimal valor = Decimal.Zero;
            if (disputaSelecionadaUi != null)
            {
                if (DecimalUpDownValorPagar is not null)
                    valor = DecimalUpDownValorPagar.Value ?? 0m;
                else
                    valor = -1;

                if (valor < 0)
                {
                    System.Windows.MessageBox.Show("Valor incorreto de pagamento!");
                }
                else
                {
                    //considero um valor válido 
                    if (_financeiroController is not null && _financeiroController.Caixa is not null)
                    {
                        //só para teste
                        // financeiroController.CaixaLocal.TotalEmCaixaWithPulePago();
                        mensagem = _financeiroController.Caixa.PagaDisputa(disputaSelecionadaUi, valor);
                    }
                }
                System.Windows.MessageBox.Show(mensagem);
            }
            else
            {
                System.Windows.MessageBox.Show("Por Favor selecione uma disputa valida!");

            }
        }

        private void Sair(object sender, RoutedEventArgs e)
        {
            _financeiroController?.Dispose();

            this.Close();
        }
    }
}
