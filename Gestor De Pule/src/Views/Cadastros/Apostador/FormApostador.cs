using Gestor_De_Pule.src.Controllers;

namespace Gestor_De_Pule.src.Views.Apostador
{
    public partial class FormApostador : Form
    {
        private ApostadorController _controller;
        private bool isAtualizar = false;

        public FormApostador()
        {
            _controller = new ApostadorController();
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the FormApostador class with the specified selected bettor.
        /// </summary>
        /// <param name="apostadorSelecionado">The selected bettor object to be used by the form.</param>
        public FormApostador(object apostadorSelecionado)
        {
            if (_controller == null)
                _controller = new ApostadorController(apostadorSelecionado);
            InitializeComponent();
            ComponentLoad();

        }
        /// <summary>
        /// Loads the current bettor's information into the form fields and displays a success message if available.
        /// </summary>
        private void ComponentLoad()
        {
            var apostador = _controller.Apostador;
            if (apostador is not null)
            {
                textBoxNome.Text = apostador.Nome;
                textBoxContato.Text = apostador.Contato;
                MessageBox.Show($"Apostador {apostador.Nome} Carregado com sucesso!");
                isAtualizar = true;
            }

        }
        /// <summary>
        /// Handles the registration or update of user information based on the current operation mode.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Cadastrar(object sender, EventArgs e)
        {
            string nome = string.Empty;
            string contato = string.Empty;
            string mensagem = String.Empty;
            if (!isAtualizar)
            {
                if (!String.IsNullOrEmpty(textBoxNome.Text))
                    nome = textBoxNome.Text;
                if (!String.IsNullOrEmpty(textBoxContato.Text))
                    contato = textBoxContato.Text;
                //mensagem = ApostadorController.SaveApostador(nome, contato);
                mensagem = _controller.SaveApostador(nome, contato);
                MessageBox.Show(mensagem);
                textBoxContato.Text = String.Empty;
                textBoxNome.Text = String.Empty;
            }
            else
            {
                AtualizarDados();
            }
        }
        /// <summary>
        /// Updates the bettor's information using the values from the name and contact text boxes and displays the
        /// result message.
        /// </summary>
        private void AtualizarDados()
        {
            string mensagem = _controller.UpdateApostador(textBoxNome.Text, textBoxContato.Text);
            MessageBox.Show(mensagem);
        }

        private void Close(object sender, EventArgs e)
        {
            _controller.Dispose();
            Close();
        }

        private void DisposeController(object sender, FormClosedEventArgs e)
        {
            _controller.Dispose();
        }
    }
}
