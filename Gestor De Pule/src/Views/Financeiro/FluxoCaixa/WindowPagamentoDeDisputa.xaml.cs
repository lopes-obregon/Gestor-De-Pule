using Gestor_De_Pule.src.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gestor_De_Pule.src.Views.Financeiro.FluxoCaixa
{
    /// <summary>
    /// Lógica interna para WindowPagamentoDeDisputa.xaml
    /// </summary>
    public partial class WindowPagamentoDeDisputa : Window
    {
        private  FinanceiroController? financeiroController = null;
        public WindowPagamentoDeDisputa()
        {
            InitializeComponent();
            financeiroController = new FinanceiroController();
            initCampos();

        }

        public WindowPagamentoDeDisputa(object? financeiroController)
        {
            this.financeiroController = financeiroController as FinanceiroController;
            InitializeComponent();
            initCampos();
        }

        private void initCampos()
        {

                //financeiroController.LoadCaixaLocal();
            if(financeiroController is not null && financeiroController.CaixaLocal is not null && financeiroController.CaixaLocal.Disputs is not null)
            {
                var disputas = financeiroController.CaixaLocal.DisputsNãoPagos();
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
                    if (financeiroController is not null && financeiroController.CaixaLocal is not null)
                    {
                        //só para teste
                        // financeiroController.CaixaLocal.TotalEmCaixaWithPulePago();
                        mensagem = financeiroController.CaixaLocal.PagaDisputa(disputaSelecionadaUi, valor);
                    }
                }
                System.Windows.MessageBox.Show(mensagem);
            }
            else
            {
                System.Windows.MessageBox.Show("Por Favor selecione uma disputa valida!");

            }
        }
    }
}
