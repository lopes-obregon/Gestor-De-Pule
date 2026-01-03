

using Gestor_De_Pule.src.Controllers;
using System.Globalization;

namespace Gestor_De_Pule.src.Views.Financeiro.Taxa
{
    public partial class FormCadastroDeTaxaEAtt : Form
    {


        public FormCadastroDeTaxaEAtt()
        {
            InitializeComponent();
            FinanceiroController.InitCaixa();
            var caixa = FinanceiroController.Caixa;
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

                FinanceiroController.SaveOrAttTaxa(novoValorTaxa);
                AtualizarTaxaLabel();
            }
        }

        private void AtualizarTaxaLabel()
        {
            var caixa = FinanceiroController.Caixa;
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
