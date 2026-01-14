using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
using System.Windows;
using System.Windows.Controls;

namespace Gestor_De_Pule.src.Views.Relatórios.Disputa
{
    /// <summary>
    /// Lógica interna para WindowRelatórioDisputa.xaml
    /// </summary>
    public partial class WindowRelatórioDisputa : Window
    {
        private DisputaController? Controller { get; set; } = null;
        public WindowRelatórioDisputa()
        {
            InitializeComponent();
            Controller = new DisputaController();
            InitComboBox();
        }

        private void InitComboBox()
        {
            var disputas = Controller.DisputasLocal;
            if (disputas != null)
            {
                ComboBoxDisputaCadastradas.ItemsSource = disputas;
            }
        }
        /// <summary>
        /// Generates and displays a report for the selected dispute, updating the UI with dispute details and results.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void GerarRelatório(object sender, RoutedEventArgs e)
        {
            if(Controller is not null)
            {
                var disputaSelecionadaUi = ComboBoxDisputaCadastradas.SelectedItem;
                if (disputaSelecionadaUi != null)
                {
                    Controller.LoadDisputaLocal(disputaSelecionadaUi);
                   
                    var disputa = Controller.DisputaLocal;
                    if (disputa != null)
                    {
                        //disputa.ReloadPulesApostador();
                        LabelDisputaNome.Content = "Disputa: " + disputa.Nome;
                        //ListViewDisputa.ItemsSource = disputa.ResultadoList;
                        GridView gridView = new GridView();
                        ListViewDisputa.View = gridView;
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header="Disputa",
                            DisplayMemberBinding = new System.Windows.Data.Binding("Disputa")
                        });
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header="Animal",
                            DisplayMemberBinding = new System.Windows.Data.Binding("Animal")
                        });
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header="Posição",
                            DisplayMemberBinding = new System.Windows.Data.Binding("Posição")
                        });
                        gridView.Columns.Add(new GridViewColumn
                        {
                            Header="Tempo",
                            DisplayMemberBinding = new System.Windows.Data.Binding("Tempo")
                        });

                        foreach(var resultado in disputa.ResultadoList)
                        {
                            if(resultado != null)
                                ListViewDisputa.Items.Add(resultado);

                        }
                        ListViewPules.ItemsSource = disputa.Pules;
                    }
                }
            }
        }

        private void ImprirDisputa(object sender, RoutedEventArgs e)
        {
            var disputaSelecionadoUi = ComboBoxDisputaCadastradas.SelectedItem;
            if (!Controller.IsDisputaValida(disputaSelecionadoUi))
            {
                System.Windows.MessageBox.Show("Error, Tente Gerar o Relatório Primeiro!");
            }
            else
            {
                PrintService.PrintRelatórioDisputa(Controller.DisputaLocal);
            }
        }
    }
}
