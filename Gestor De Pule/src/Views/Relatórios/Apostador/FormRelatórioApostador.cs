using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;

namespace Gestor_De_Pule.src.Views.Relatórios.Apostador

{
    public partial class FormRelatórioApostador : Form
    {

        public FormRelatórioApostador()
        {
            InitializeComponent();
            RelatórioApostadorController.LoadLists();
            var controller = RelatórioApostadorController.Apostadors;
            initComboBox(controller);
        }

        private void initComboBox(List<Model.Apostador>? controller)
        {
            comboBoxApostadores.Items.Clear();
            if (controller != null)
                comboBoxApostadores.Items.AddRange(controller.ToArray());
        }
        /// <summary>
        /// Handles the event to generate and display the report for the selected bettor, including their bets and total
        /// amounts.
        /// </summary>
        /// <remarks>This method is intended to be used as an event handler for UI actions that trigger
        /// the generation of a bettor's report. It updates the UI components to reflect the selected bettor's
        /// information and their associated bets. If no bettor is selected or no bets are found, the report fields are
        /// reset to default values.</remarks>
        
        private void GerarRelatório(object sender, EventArgs e)
        {

            listViewApostadores.Items.Clear();
            labelValorTotalApostado.Text = "0";
            labelTotalDePules.Text = "0";
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            RelatórioApostadorController.LoadApostador(apostadorSelecionadoUi);
            RelatórioApostadorController.LoadPuLesDoApostador();
            float valorTotalApostado = 0.0f;
            if (RelatórioApostadorController.Apostador is not null)
            {
                labelApostador.Text = "APOSTADOR: " + RelatórioApostadorController.Apostador.Nome;
                if (RelatórioApostadorController.Pules is not null && RelatórioApostadorController.Pules.Count > 0)
                {
                    foreach (var pule in RelatórioApostadorController.Pules)
                    {
                        //dataGridViewPules.Rows.Ad(pule.Número, pule.Date.ToShortDateString(), pule.AnimaisToString(), pule.Valor, pule.StatusPagamento);
                        if (pule is not null)
                        {
                            ListViewItem item = new ListViewItem(pule.Número.ToString());
                            item.SubItems.Add(pule.Date.ToShortDateString());
                            item.SubItems.Add(pule.AnimaisToString());
                            item.SubItems.Add(pule.Valor.ToString("C"));
                            item.SubItems.Add(pule.StatusPagamento.ToString());
                            listViewApostadores.Items.Add(item);
                            valorTotalApostado += pule.Valor;

                        }
                    }
                    labelTotalDePules.Text = RelatórioApostadorController.Pules.Count.ToString();
                    labelValorTotalApostado.Text = valorTotalApostado.ToString();
                }

            }

        }

        private void ImprimirRelaTório(object sender, EventArgs e)
        {
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            string totalDePules = labelTotalDePules.Text;
            string valorTotalApostado = labelValorTotalApostado.Text;
            if (RelatórioApostadorController.Apostador is null)
            {
                RelatórioApostadorController.LoadApostador(apostadorSelecionadoUi);
                RelatórioApostadorController.LoadPuLesDoApostador();

            }
            if(RelatórioApostadorController.Apostador is not null)
            {
                if (String.IsNullOrEmpty(totalDePules))
                {
                    totalDePules = RelatórioApostadorController.Pules.Count.ToString();
                }
                PrintService.PrintRelatórioApostador(RelatórioApostadorController.Apostador, RelatórioApostadorController.Pules, totalDePules, valorTotalApostado);
            }
        }
    }
}
