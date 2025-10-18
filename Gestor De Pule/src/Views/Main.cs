using Gestor_De_Pule.src.Views;
using Gestor_De_Pule.src.Views.Apostador;
using Gestor_De_Pule.src.Views.Pule;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void JanelaCadastro(object sender, EventArgs e)
        {
            OpenWindowWpfCadastro();
        }

        private void OpenWindowWpfCadastro()
        {
            var window = new WindowAnimalCadastrados();
            window.ShowDialog();
        }

        private void OpenWindowApostadoresCadastrados(object sender, EventArgs e)
        {
            var window = new WindowApostadoresCadastrados();
            window.ShowDialog();
        }

        private void WindowPuleCadastrados(object sender, EventArgs e)
        {
            var window = new WindowPuleCadastrados();
            window.ShowDialog();
        }
    }
}
