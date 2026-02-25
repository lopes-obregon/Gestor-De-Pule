using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Relatórios.Pule
{
    /// <summary>
    /// Lógica interna para WindowRelatórioPule.xaml
    /// </summary>
    public partial class WindowRelatórioPule : Window
    {
        private PuleController _puleController = new PuleController();
        public WindowRelatórioPule()
        {
            InitializeComponent();
            InitComboBox();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitComboBox()
        {
            comboBoxPulesCadastrados.Items.Clear();
           // RelatórioPuleController.LoadLists();
            _puleController.LoadPules();
            //var pules = RelatórioPuleController.GetAttrNumPule();
            var pules = _puleController.GetAttrNumPule();
            if(pules is not null && pules.Count > 0)
            {
                comboBoxPulesCadastrados.ItemsSource = pules;
               

               

            }
        }
        /// <summary>
        /// Generates a report for the selected betting slip and updates the UI with the report details.
        /// </summary>
        /// <remarks>This method retrieves the selected betting slip from the UI, calculates totals for
        /// the number of slips, the total amount bet, and the number of paid and pending slips. It then updates the UI
        /// labels with these calculated values. Ensure that a betting slip is selected before invoking this method to
        /// avoid null references.</remarks>
        /// <param name="sender">The source of the event, typically a button or UI element.</param>
        /// <param name="e">The event data associated with the routed event.</param>
        private void GerarRelatórioPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionadoUi = comboBoxPulesCadastrados.SelectedItem;
            labelNumPule.Content = "Pule Número: "+puleSelecionadoUi;
            var pules =  _puleController.PulesSelecionados(puleSelecionadoUi);
           if(pules is not null && pules.Count > 0)
            {
                listViewPule.ItemsSource = pules;
                labelTotalPule.Content = "Total De Pules: "+pules.Count;
                decimal totalApostado = 0.0m;
                int totalPagos = 0;
                int totalPedente = 0;
                foreach (var pule in pules)
                {
                    if (pule is not null)
                    {
                        totalApostado += pule.Valor;
                        if(pule.StatusPagamento == Model.StatusPagamento.Pago)
                            totalPagos++;
                        else if (pule.StatusPagamento == Model.StatusPagamento.Pendente)
                            totalPedente++;

                    }
                }
                labelTotalApostado.Content = "Total Apostado R$: "+totalApostado;
                labelTotalPagos.Content = "Total Pagos: "+totalPagos;
                labelTotalPedente.Content = "Total Pendente: "+totalPedente;
            }
            
        }

        private void ImprimirRelatório(object sender, RoutedEventArgs e)
        {
            string numPule = labelNumPule.Content.ToString();
            string totalApostado = labelTotalApostado.Content.ToString();
            string totalPagos = labelTotalPagos.Content.ToString();
            string totalPedente = labelTotalPedente.Content.ToString();
            if (String.IsNullOrEmpty(numPule) || String.IsNullOrEmpty(totalApostado) || String.IsNullOrEmpty(totalPagos) || String.IsNullOrEmpty(totalPedente) ) 
            {
                System.Windows.MessageBox.Show("Por favor, gere o relatório!");
            }
            else
            {
                var puleSelecionadoUi = comboBoxPulesCadastrados.SelectedItem;
                var pules = _puleController.PulesSelecionados(puleSelecionadoUi);
                if (pules == null)
                {
                    System.Windows.MessageBox.Show("Pule Invalido!");
                }
                else
                {
                    PrintService.PrintRelatórioPule(pules, puleSelecionadoUi);
                }
            }
        }
    }
}
