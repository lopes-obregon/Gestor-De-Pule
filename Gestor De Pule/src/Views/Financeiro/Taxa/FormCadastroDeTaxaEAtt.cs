

using Gestor_De_Pule.src.Controllers;
using System.Globalization;
using System.Printing;

namespace Gestor_De_Pule.src.Views.Financeiro.Taxa
{
    public partial class FormCadastroDeTaxaEAtt : Form
    {

        private FinanceiroController _financeiroController = new FinanceiroController();
        public FormCadastroDeTaxaEAtt()
        {
            InitializeComponent();
            _financeiroController.InitCaixa();
            var caixa = _financeiroController.Caixa;
            if (caixa != null)
                labelTaxaAtual.Text = "Taxa Atual: " + caixa.Taxa.ToString("P");
        }

        private void SalvarTaxa(object sender, EventArgs e)
        {
            decimal novoValorTaxa = numericUpDown1.Value;
            if (novoValorTaxa == 0.00m)
            {
                MessageBox.Show("Por favor Digite o novo valor para Taxa!");
            }
            else
            {
                novoValorTaxa = novoValorTaxa / 100;
                _financeiroController.SaveOrAttTaxa(novoValorTaxa);
                AtualizarTaxaLabel();
            }
        }

        private void AtualizarTaxaLabel()
        {
            var caixa = _financeiroController.Caixa;
            if (caixa is not null)
                labelTaxaAtual.Text = "Taxa Atual: " + caixa.Taxa.ToString("P");
            numericUpDown1.Value = 0.00m;
        }

        private void Sair(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
