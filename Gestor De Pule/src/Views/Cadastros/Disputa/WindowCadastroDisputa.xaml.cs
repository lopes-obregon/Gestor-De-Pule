using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Views.Cadastros.Disputa;
using System.Windows;

namespace Gestor_De_Pule.src.Views.Cadastros
{
    /// <summary>
    /// Lógica interna para WindowCadastroDisputa.xaml
    /// </summary>
    public partial class WindowCadastroDisputa : Window
    {
        private DisputaController _controller;
        public WindowCadastroDisputa()
        {
            InitializeComponent();
            InitList();
        }

        private void InitList()
        {
            _controller = new DisputaController();
            // DisputaController.();
            _controller.LoadListDisputa();
            var disputas = _controller.Disputas;
            listViewDisputaCadastrados.ItemsSource = null;
            //var disputas = DisputaController.Disputas;
            if (disputas != null) listViewDisputaCadastrados.ItemsSource = disputas;
        }

        private void FormCadastroDisputa(object sender, RoutedEventArgs e)
        {
            var form = new FormCadastroDisputa();
            form.ShowDialog();
           
            //ReloadList();
            InitList();
            
        }

     

        private void AtualizarDisputaSelecionado(object sender, RoutedEventArgs e)
        {
            var itemSelecionadoUi = listViewDisputaCadastrados.SelectedItem;
            var form = new FormAtualizarDisputaCadastrado(itemSelecionadoUi);
            form.ShowDialog();
            InitList();
        }

        private void ExcluirDisputa(object sender, RoutedEventArgs e)
        {
            var disputaSelecionadoUi = listViewDisputaCadastrados.SelectedItem;
            if(disputaSelecionadoUi is not null)
            {

                var resposta = System.Windows.MessageBox.Show("Deseja Realmente Remover ?", "Pergunta", MessageBoxButton.YesNoCancel);
                if(resposta == MessageBoxResult.Yes)
                {
                    bool sucess = false;
                    sucess = _controller.RemoveDisuptaSelecionado(disputaSelecionadoUi);
                    if (sucess) System.Windows.MessageBox.Show("Disputa Removida com Sucesso!");
                    else
                        System.Windows.MessageBox.Show("Desculpe houve algum problema para Remover a disputa!");
                }
            }
            InitList();
        }
    }
}
