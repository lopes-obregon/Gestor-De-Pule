using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;

namespace Gestor_De_Pule.src.Views.Relatórios.Apostador

{
    public partial class FormRelatórioApostador : Form
    {
        private ApostadorController _apostadorController = new ApostadorController();

        public FormRelatórioApostador()
        {
            InitializeComponent();
            _apostadorController.LoadLists();
           // RelatórioApostadorController.LoadLists();
           
            initComboBox();
        }

        private void initComboBox()
        {
            comboBoxApostadores.Items.Clear();
            var apostadores = _apostadorController.Apostadors;
            if (apostadores is not  null  && apostadores.Count >  0)
                comboBoxApostadores.Items.AddRange(_apostadorController.Apostadors.ToArray());
        }
        /// <summary>
        /// Handles the event to generate and display the report for the selected bettor, including their bets and total
        /// amounts.
        /// </summary>
        /// <remarks>This method is intended to be used as an event handler for UI actions that trigger
        /// the generation of a bettor's report. It updates the UI components to reflect the selected bettor's
        /// information and their associated bets. If no bettor is selected or no bets are found, the report fields are
        /// reset to default values.</remarks>
        
        private void GerarRelatório()
        {

            listViewApostadores.Items.Clear();
            labelValorTotalApostado.Text = "0";
            labelTotalDePules.Text = "0";
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            //RelatórioApostadorController.LoadApostador(apostadorSelecionadoUi);
            _apostadorController.LoadApostador(apostadorSelecionadoUi);
            //RelatórioApostadorController.LoadPuLesDoApostador();
            _apostadorController.LoadPulesDoApostador();
            float valorTotalApostado = 0.0f;
            if (_apostadorController.Apostador is not null)
            {
                labelApostador.Text = "APOSTADOR: " + _apostadorController.Apostador.Nome;
                if (_apostadorController.PuleController.PulesApostador is not null && _apostadorController.PuleController.PulesApostador.Count > 0)
                {
                    foreach (var pule in _apostadorController.PuleController.PulesApostador)
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
                    labelTotalDePules.Text = _apostadorController.PuleController.PulesApostador.Count.ToString();
                    labelValorTotalApostado.Text = valorTotalApostado.ToString("C");
                }

            }

        }

        private void ImprimirRelaTório()
        {
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            string totalDePules = labelTotalDePules.Text;
            string valorTotalApostado = labelValorTotalApostado.Text;
            if (_apostadorController.Apostador is null)
            {
                _apostadorController.LoadApostador(apostadorSelecionadoUi);
                _apostadorController.LoadPulesDoApostador();

            }
            if(RelatórioApostadorController.Apostador is not null)
            {
                if (String.IsNullOrEmpty(totalDePules))
                {
                    totalDePules = _apostadorController.PuleController.PulesApostador.Count.ToString();
                }
                PrintService.PrintRelatórioApostador(_apostadorController.Apostador, _apostadorController.PuleController.PulesApostador, totalDePules, valorTotalApostado);
            }
        }
    }
}
