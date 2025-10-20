using Gestor_De_Pule.src.Controllers;
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
            listViewPules.ItemsSource = null;
            if (PuleController.Pules is not null || PuleController.Pules.Count > 0)
                listViewPules.ItemsSource = PuleController.Pules;
            listViewPules.Items.Refresh();
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
    }
}
