using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Pule
{
    /// <summary>
    /// Lógica interna para WindowPuleCadastrados.xaml
    /// </summary>
    public partial class WindowPuleCadastrados : Window
    {
        private PuleController _puleController;
        public WindowPuleCadastrados()
        {
            _puleController = new ();
            InitializeComponent();
            
            UpdateListViewPules();
        }

        private void UpdateListViewPules()
        {
            //PuleController.LoadPules();
            _puleController.LoadPules();
            _puleController.LoadAnimais();
            //PuleController.LoadAnimais();
            listViewPules.ItemsSource = null;
            //if (PuleController.Pules is not null && PuleController.Pules.Count > 0)
            if (_puleController.Pules is not null && _puleController.Pules.Count > 0)
                listViewPules.ItemsSource = _puleController.Pules;
                //listViewPules.ItemsSource = PuleController.Pules;
            listViewPules.Items.Refresh();
            //if(PuleController.Animals is not null && PuleController.Animals.Count > 0)
            if(_puleController.Animals is not null && _puleController.Animals.Count > 0)
            {
                //var animaisMaisApostados = PuleController.Animals.OrderByDescending(a=>a.Pules.Count).ToList();
                var animaisMaisApostados = _puleController.Animals.OrderByDescending(a=>a.Pules.Count).ToList();
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
                //mensagem = PuleController.RemovePule(puleSelecionado);
                mensagem = _puleController.RemovePule(puleSelecionado);
                System.Windows.MessageBox.Show(mensagem);
            }
            UpdateListViewPules();
        }
        private void ImprimirPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionadoUi = listViewPules.SelectedItems;
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
