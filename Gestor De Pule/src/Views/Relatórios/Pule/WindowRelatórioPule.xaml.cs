using Gestor_De_Pule.src.Controllers;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Relatórios.Pule
{
    /// <summary>
    /// Lógica interna para WindowRelatórioPule.xaml
    /// </summary>
    public partial class WindowRelatórioPule : Window
    {
        public WindowRelatórioPule()
        {
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            comboBoxPulesCadastrados.Items.Clear();
            RelatórioPuleController.LoadLists();
            var pules = RelatórioPuleController.GetAttrNumPule();
            if(pules is not null && pules.Count > 0)
            {
                comboBoxPulesCadastrados.ItemsSource = pules;
               

               

            }
        }

        private void GerarRelatórioPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionadoUi = comboBoxPulesCadastrados.SelectedItem;
            labelNumPule.Content = "Pule Número: "+puleSelecionadoUi;
            var pules =  RelatórioPuleController.PuleSelecionado(puleSelecionadoUi);
           if(pules is not null && pules.Count > 0)
            {
                listViewPule.ItemsSource = pules;
                labelTotalPule.Content = "Total De Pules: "+pules.Count;
                float totalApostado = 0.0f;
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
    }
}
