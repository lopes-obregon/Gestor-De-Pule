using System.Windows;
using Gestor_De_Pule.src.Controllers;
namespace Gestor_De_Pule.src.Views.Apostador
{
    /// <summary>
    /// Lógica interna para WindowApostadoresCadastrados.xaml
    /// </summary>
    public partial class WindowApostadoresCadastrados : Window
    {
        private ApostadorController _controller;
        public WindowApostadoresCadastrados()
        {
            InitializeComponent();
             _controller = new ApostadorController();
            UpdateListViewApostadoresCadastrados();
        }

        private void UpdateListViewApostadoresCadastrados()
        {
            //ApostadorController.LoadApostadores();
            _controller.LoadApostadores();
            ListViewApostadores.ItemsSource = null;
            //var apostadoresCadastrados = ApostadorController.Apostadors;
            var apostadoresCadastrados = _controller.Apostadors;
            if (apostadoresCadastrados is not null) 
                ListViewApostadores.ItemsSource = apostadoresCadastrados;
            ListViewApostadores.Items.Refresh();
        }
        /// <summary>
        /// Opens the apostador registration form, disposes and reinitializes the controller, and updates the list of
        /// registered apostadores.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void CadastrarApostador(object sender, RoutedEventArgs e)
        {
            _controller.Dispose();
            var cadastroApostador = new FormApostador();
            cadastroApostador.ShowDialog();
            _controller = new ApostadorController();
            UpdateListViewApostadoresCadastrados();
        }
        /// <summary>
        /// Handles the update process for a selected bettor, disposing the current controller, opening the edit form,
        /// and refreshing the list view with updated data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void WindowsUpdateApostador(object sender, RoutedEventArgs e)
        {
            var apostadorSelecionado = ListViewApostadores.SelectedItem;
            //fecha o contexto atual
            _controller.Dispose();
            var alterarApostador = new FormApostador(apostadorSelecionado);
            if (alterarApostador is not null)
                alterarApostador.ShowDialog();
            //e abre o novo contexto atualizado
            _controller = new ApostadorController();
            UpdateListViewApostadoresCadastrados();
        }

        private void ExcluirApostador(object sender, RoutedEventArgs e)
        {
            var apostadorSelecionadoUi  = ListViewApostadores.SelectedItem;
            
            var respostaSelecionado = System.Windows.MessageBox.Show("Tem Certeza Que Deseja Remover ?", "Pergunta",  System.Windows.MessageBoxButton.YesNoCancel);
            if(respostaSelecionado == System.Windows.MessageBoxResult.Yes)
            {
                string mensagem = String.Empty;
                if (apostadorSelecionadoUi is not null)
                    mensagem = _controller.RemoveApostador(apostadorSelecionadoUi);
                    //mensagem = ApostadorController.RemoveApostador(apostadorSelecionadoUi);

            }
            UpdateListViewApostadoresCadastrados();
            
        }
    }
}
