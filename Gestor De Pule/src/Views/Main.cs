using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Views;
using Gestor_De_Pule.src.Views.Apostador;
using Gestor_De_Pule.src.Views.Pule;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            MainController.LoadLists();
            InitComboBox();
        }

        private void InitComboBox()
        {
            comboBoxApostadores.Items.Clear();
            if (MainController.Apostadors.Count > 0)
            {
                comboBoxApostadores.Items.AddRange(MainController.Apostadors.ToArray());
            }

        }

        private void JanelaCadastro(object sender, EventArgs e)
        {
            OpenWindowWpfCadastro();
        }

        private void OpenWindowWpfCadastro()
        {
            var window = new WindowAnimalCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void OpenWindowApostadoresCadastrados(object sender, EventArgs e)
        {
            var window = new WindowApostadoresCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void WindowPuleCadastrados(object sender, EventArgs e)
        {
            var window = new WindowPuleCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void GerarRelatório(object sender, EventArgs e)
        {
            dataGridViewPules.Rows.Clear();
            labelValorTotalApostado.Text = "0";
            labelTotalDePules.Text = "0";
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            MainController.LoadApostador(apostadorSelecionadoUi);
            MainController.LoadPuLesDoApostador();
            float valorTotalApostado = 0.0f;
            if(MainController.Apostador is not null)
            {
                labelApostador.Text = "APOSTADOR: " + MainController.Apostador.Nome;
                if(MainController.Pules is not null && MainController.Pules.Count > 0)
                {
                    foreach (var pule in MainController.Pules)
                    {
                        dataGridViewPules.Rows.Add(pule.Id, pule.Date.ToShortDateString(), pule.AnimaisToString(), pule.Valor, pule.StatusPagamento);
                        valorTotalApostado += pule.Valor;
                    }
                    labelTotalDePules.Text = MainController.Pules.Count.ToString();
                    labelValorTotalApostado.Text = valorTotalApostado.ToString();
                }

            }
        }
    }
}
