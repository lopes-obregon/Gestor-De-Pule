using Gestor_De_Pule.src.Controllers;
using System.Windows;

namespace Gestor_De_Pule.src.Views
{
    /// <summary>
    /// Lógica interna para WindowCadastroAnimal.xaml
    /// </summary>
    public partial class WindowAnimalCadastrados : Window
    {
        public WindowAnimalCadastrados()
        {
            InitializeComponent();
           
            AtualizarListViewAnimaisCadastrados();
        }

        private void AtualizarListViewAnimaisCadastrados()
        {
            AnimalController.LoadAnimais();
            ListViewAnimais.ItemsSource = null;

            var animaisCadastrados = AnimalController.Animals;
            if(animaisCadastrados is not null)
                ListViewAnimais.ItemsSource = animaisCadastrados;
            ListViewAnimais.Items.Refresh();
        }

        private void WindowCadastro(object sender, RoutedEventArgs e)
        {
            var windowCadastro = new WindowAnimalCadastrados();
            windowCadastro.ShowDialog();
            AtualizarListViewAnimaisCadastrados();

        }

        private void AtualizarDadosAnimal(object sender, RoutedEventArgs e)
        {
            var animalSelecionado = ListViewAnimais.SelectedItem;
            if (animalSelecionado is not null)
            {
                var formAtualizarDados = new FormAnimalAtualizar(animalSelecionado);
                formAtualizarDados.ShowDialog();
                AtualizarListViewAnimaisCadastrados();
            }
        }

        private void DeleteAnimal(object sender, RoutedEventArgs e)
        {
            var AnimalSelecionado = ListViewAnimais.SelectedItem;
            var res = System.Windows.MessageBox.Show("Tem Certeza Que Deseja Remover?", "´Pergunta", System.Windows.MessageBoxButton.YesNoCancel);
            if (res == MessageBoxResult.Yes)
            {
                string mensagem = "";
                if (AnimalSelecionado is not null)
                    mensagem = AnimalController.DeleteAnimal(AnimalSelecionado);
                System.Windows.MessageBox.Show(mensagem);
                AtualizarListViewAnimaisCadastrados();
            }
        }
    }
}
