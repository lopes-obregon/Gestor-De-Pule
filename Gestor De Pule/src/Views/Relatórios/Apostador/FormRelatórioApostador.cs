using Gestor_De_Pule.src.Controllers;

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
                        if(pule is not null)
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
    }
}
