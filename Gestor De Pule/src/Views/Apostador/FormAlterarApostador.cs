using Gestor_De_Pule.src.Controllers;

namespace Gestor_De_Pule.src.Views.Apostador
{
    public partial class FormAlterarApostador : Form
    {
        public FormAlterarApostador(object apostadorSelecionado)
        {
            InitializeComponent();
            if (apostadorSelecionado is not null)
                ApostadorController.LoadApostador(apostadorSelecionado);
            if (ApostadorController.Apostador is not null)
            {
                textBoxNome.Text = ApostadorController.Apostador.Nome;
                textBoxContato.Text = ApostadorController.Apostador.Contato;
                textBoxNome.ReadOnly = true;

            }
        }

        private void UpdateApostador(object sender, EventArgs e)
        {
            string nome = String.Empty; string contato = String.Empty;
            nome = textBoxNome.Text;
            contato = textBoxContato.Text;
            string mensagem = String.Empty;
            mensagem = ApostadorController.AtualizarApostador(nome, contato);
            MessageBox.Show(mensagem);
        }

        private void CloseFormAtualizarApostador(object sender, EventArgs e)
        {
            Close();
        }
    }
}
