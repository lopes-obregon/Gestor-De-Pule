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
        private CaixaController _caixaController;
        public WindowCadastroDisputa()
        {
            InitializeComponent();
            InitList();
            var context = _controller.GetContext();
            _caixaController = new CaixaController(context.GetDataBase());
        }

        private void InitList()
        {
            _controller = new DisputaController();
            // DisputaController.();
            _controller.LoadDisputs();
            var disputas = _controller.Disputas;
            listViewDisputaCadastrados.ItemsSource = null;
            //var disputas = DisputaController.Disputas;
            if (disputas != null) listViewDisputaCadastrados.ItemsSource = disputas;
        }

        private void FormCadastroDisputa(object sender, RoutedEventArgs e)
        {
            var form = new FormDisputa();
            form.ShowDialog();
           
            //ReloadList();
            InitList();
            
        }

     

        private void AtualizarDisputaSelecionado(object sender, RoutedEventArgs e)
        {
            var itemSelecionadoUi = listViewDisputaCadastrados.SelectedValue;
           // var form = new FormAtualizarDisputaCadastrado(itemSelecionadoUi);
            var form = new FormDisputa(itemSelecionadoUi);
            form.ShowDialog();
            InitList();
        }

        private void ExcluirDisputa(object sender, RoutedEventArgs e)
        {
            var disputaSelecionadoUi = listViewDisputaCadastrados.SelectedItem;
            bool sucess = false;
            int id = 0;
            if (disputaSelecionadoUi is not null)
            {

                var resposta = System.Windows.MessageBox.Show("Deseja Realmente Remover ?", "Pergunta", MessageBoxButton.YesNoCancel);
                if(resposta == MessageBoxResult.Yes)
                {
                    
                    id = Models.Disputa.ObjectToDisputaGetId(disputaSelecionadoUi);
                    _controller.LoadDisputa(id);
                    var disputa = _controller.Disputa;
                    if (disputa != null)
                    {
                        _caixaController.LoadCaixaWithDisput(disputa.Id);
                        sucess = _caixaController.RemoveDisput(disputa.Id);
                        sucess = _controller.RemoveDisuptaSelecionado(disputa);
                    }
                    if (sucess) System.Windows.MessageBox.Show("Disputa Removida com Sucesso!");
                    else
                        System.Windows.MessageBox.Show("Desculpe houve algum problema para Remover a disputa!");
                }
            }
            InitList();
        }
    }
}
