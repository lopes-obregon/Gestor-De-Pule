using Gestor_De_Pule.src.Controllers;

namespace Gestor_De_Pule.src.Views.Apostador
{
    public partial class FormAlterarApostador : Form
    {
        private ApostadorController _controller = new ApostadorController();
        public FormAlterarApostador(object apostadorSelecionado)
        {
            InitializeComponent();
            if (apostadorSelecionado is not null)
                _controller.LoadApostador(apostadorSelecionado);
                //ApostadorController.LoadApostador(apostadorSelecionado);
            if (_controller.Apostador is not null)
            {
                textBoxNome.Text = _controller.Apostador.Nome;
                textBoxContato.Text = _controller.Apostador.Contato;
                textBoxNome.ReadOnly = true;

            }
        }

        private void UpdateApostador(object sender, EventArgs e)
        {
            string nome = String.Empty; string contato = String.Empty;
            nome = textBoxNome.Text;
            contato = textBoxContato.Text;
            string mensagem = String.Empty;
            //mensagem = ApostadorController.AtualizarApostador(nome, contato);
            mensagem = _controller.AtualizarApostador(nome, contato);
            MessageBox.Show(mensagem);
        }

        private void CloseFormAtualizarApostador(object sender, EventArgs e)
        {
            Close();
        }
    }
}
