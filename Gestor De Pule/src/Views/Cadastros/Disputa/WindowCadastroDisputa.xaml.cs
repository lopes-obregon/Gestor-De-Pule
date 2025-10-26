using Gestor_De_Pule.src.Views.Cadastros.Disputa;
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

namespace Gestor_De_Pule.src.Views.Cadastros
{
    /// <summary>
    /// Lógica interna para WindowCadastroDisputa.xaml
    /// </summary>
    public partial class WindowCadastroDisputa : Window
    {
        public WindowCadastroDisputa()
        {
            InitializeComponent();
        }

        private void FormCadastroDisputa(object sender, RoutedEventArgs e)
        {
            var form = new FormCadastroDisputa();
            form.ShowDialog();
        }
    }
}
