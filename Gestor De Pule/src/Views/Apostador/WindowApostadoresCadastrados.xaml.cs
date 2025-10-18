﻿using System.Windows;
using Gestor_De_Pule.src.Controllers;
namespace Gestor_De_Pule.src.Views.Apostador
{
    /// <summary>
    /// Lógica interna para WindowApostadoresCadastrados.xaml
    /// </summary>
    public partial class WindowApostadoresCadastrados : Window
    {
        public WindowApostadoresCadastrados()
        {
            InitializeComponent();
            UpdateListViewApostadoresCadastrados();
        }

        private void UpdateListViewApostadoresCadastrados()
        {
            ApostadorController.LoadApostadores();
            ListViewApostadores.ItemsSource = null;
            var apostadoresCadastrados = ApostadorController.Apostadors;
            if (apostadoresCadastrados is not null) 
                ListViewApostadores.ItemsSource = apostadoresCadastrados;
            ListViewApostadores.Items.Refresh();
        }

        private void CadastrarApostador(object sender, RoutedEventArgs e)
        {
            var cadastroApostador = new FormCadastro();
            cadastroApostador.ShowDialog();
            UpdateListViewApostadoresCadastrados();
        }

        private void WindowsUpdateApostador(object sender, RoutedEventArgs e)
        {
            var apostadorSelecionado = ListViewApostadores.SelectedItem;
            var alterarApostador = new FormAlterarApostador(apostadorSelecionado);
            if (alterarApostador is not null)
                alterarApostador.ShowDialog();
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
                    mensagem = ApostadorController.RemoveApostador(apostadorSelecionadoUi);

            }
            UpdateListViewApostadoresCadastrados();
            
        }
    }
}
