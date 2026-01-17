using Gestor_De_Pule.src.Controllers;
using System.Windows;

namespace Gestor_De_Pule.src.Views
{
    /// <summary>
    /// Lógica interna para WindowCadastroAnimal.xaml
    /// </summary>
    public partial class WindowAnimalCadastrados : Window
    {
        private AnimalController _controller { get; set; }
        public WindowAnimalCadastrados()
        {
            InitializeComponent();
            _controller = new AnimalController();
           
            AtualizarListViewAnimaisCadastrados();
        }
        /// <summary>
        /// Updates the ListView to display the current list of registered animals from the controller.
        /// </summary>
        private void AtualizarListViewAnimaisCadastrados()
        {
            //AnimalController.LoadAnimais();
            _controller.LoadAnimais();
            ListViewAnimais.ItemsSource = null;

            // var animaisCadastrados = AnimalController.Animals;
            var animaisCadastrados = _controller.Animals;
            if(animaisCadastrados is not null)
                ListViewAnimais.ItemsSource = animaisCadastrados;
            ListViewAnimais.Items.Refresh();
        }
        /// <summary>
        /// Opens the animal registration window as a dialog and updates the list of registered animals.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data for the routed event.</param>
        private void WindowCadastro(object sender, RoutedEventArgs e)
        {
            var formWindow = new WindowAnimalCadastro();
            formWindow.ShowDialog();
            AtualizarListViewAnimaisCadastrados();

        }
        /// <summary>
        /// Opens a dialog to update the selected animal's data and refreshes the animal list view after changes.
        /// </summary>
        private void AtualizarDadosAnimal()
        {
            var animalSelecionado = ListViewAnimais.SelectedItem;
            if (animalSelecionado is not null)
            {
                var formAtualizarDados = new FormAnimalAtualizar(animalSelecionado);
                formAtualizarDados.ShowDialog();
                AtualizarListViewAnimaisCadastrados();
            }
        }
        /// <summary>
        /// Removes the selected animal from the list after user confirmation and updates the displayed list.
        /// </summary>
        private void DeleteAnimal()
        {
            var AnimalSelecionado = ListViewAnimais.SelectedItem;
            var res = System.Windows.MessageBox.Show("Tem Certeza Que Deseja Remover?", "´Pergunta", System.Windows.MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes)
            {
                string mensagem = "";
                if (AnimalSelecionado is not null)
                    mensagem = _controller.DeleteAnimal(AnimalSelecionado);
                    //mensagem = AnimalController.DeleteAnimal(AnimalSelecionado);
                System.Windows.MessageBox.Show(mensagem);
                AtualizarListViewAnimaisCadastrados();
            }
        }
    }
}
