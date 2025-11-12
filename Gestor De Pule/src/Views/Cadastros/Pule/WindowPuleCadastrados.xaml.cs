using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
using System;
using System.Collections.Generic;
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

namespace Gestor_De_Pule.src.Views.Pule
{
    /// <summary>
    /// Lógica interna para WindowPuleCadastrados.xaml
    /// </summary>
    public partial class WindowPuleCadastrados : Window
    {
        public WindowPuleCadastrados()
        {
            InitializeComponent();
            
            UpdateListViewPules();
        }

        private void UpdateListViewPules()
        {
            PuleController.LoadPules();
            PuleController.LoadAnimais();
            listViewPules.ItemsSource = null;
            if (PuleController.Pules is not null && PuleController.Pules.Count > 0)
                listViewPules.ItemsSource = PuleController.Pules;
            listViewPules.Items.Refresh();
            if(PuleController.Animals is not null && PuleController.Animals.Count > 0)
            {
                var animaisMaisApostados = PuleController.Animals.OrderByDescending(a=>a.Pules.Count).ToList();
                ListBoxAnimaisMaisApostados.ItemsSource = null;
                ListBoxAnimaisMaisApostados.ItemsSource = animaisMaisApostados;
            }
        }

        private void CadastrarPule(object sender, RoutedEventArgs e)
        {
            var form = new FormCadastroPule();
            form.ShowDialog();
            UpdateListViewPules();
        }

        private void FormAtualizarPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionado = listViewPules.SelectedItem;
            if (puleSelecionado != null)
            {
                var form = new FormAtualizarPule(puleSelecionado);
                form.ShowDialog();
                UpdateListViewPules();
            }
            else
                System.Windows.MessageBox.Show("Seleção Invalida!");
        }

        private void ExcluirPule(object sender, RoutedEventArgs e)
        {
            var res = System.Windows.MessageBox.Show("Deseja Realmente remover O Pule ?", "Pergunta", System.Windows.MessageBoxButton.YesNoCancel);
            if(res == MessageBoxResult.Yes)
            {
                var puleSelecionado = listViewPules.SelectedItem;
                String mensagem = String.Empty;
                mensagem = PuleController.RemovePule(puleSelecionado);
                System.Windows.MessageBox.Show(mensagem);
            }
            UpdateListViewPules();
        }
        private void ImprimirPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionadoUi = listViewPules.SelectedItem;
            if(puleSelecionadoUi != null)
            {
                ComprovanteService comprovanteService = new ComprovanteService();
                comprovanteService.PrintPule(puleSelecionadoUi);

            }
            else
            {
                System.Windows.MessageBox.Show("Selecione um pule para imprimir!");
            }
        }
    }
}
