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
            var pules = _puleController.Pules;
            if (pules is not null && pules.Count > 0)
                listViewPules.ItemsSource = pules;
                //listViewPules.ItemsSource = PuleController.Pules;
            listViewPules.Items.Refresh();
            //if(PuleController.Animals is not null && PuleController.Animals.Count > 0)
            var animais = _puleController.AnimalController.Animals.ToList();
            if(animais is not null && animais.Count > 0)
            {
                //var animaisMaisApostados = PuleController.Animals.OrderByDescending(a=>a.Pules.Count).ToList();
                var animaisMaisApostados = animais.OrderByDescending(a=>a.Pules.Count).ToList();
                ListBoxAnimaisMaisApostados.ItemsSource = null;
                ListBoxAnimaisMaisApostados.ItemsSource = animaisMaisApostados;
            }
        }

        private void CadastrarPule(object sender, RoutedEventArgs e)
        {
            _puleController.Dispose();
            var form = new FormPule();
            form.ShowDialog();
            _puleController = new();
            UpdateListViewPules();
        }

        private void FormAtualizarPule(object sender, RoutedEventArgs e)
        {
            var puleSelecionado = listViewPules.SelectedItem;
            if (puleSelecionado != null)
            {
                _puleController.Dispose();
                var form = new FormPule(puleSelecionado);
                form.ShowDialog();
                _puleController = new();//refaz o contexto com dados atualizados
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
