using Gestor_De_Pule.src.Controllers;
namespace Gestor_De_Pule.src.Views.Cadastros.Disputa
{
    public partial class FormCadastroDisputa : Form
    {
        private DisputaController _controller = new DisputaController();
        public FormCadastroDisputa()
        {
            InitializeComponent();
            //_controller.InitAnimalController();
            InitComboBoxs();
        }
        /// <summary>
        /// Initializes the combo box with a list of registered animals by loading data from the controller and adding
        /// the animals to the combo box items.
        /// </summary>
        private void InitComboBoxs()
        {
            //DisputaController.LoadLists();
            _controller.LoadLists();
            var animais = _controller.Animals;
            if (animais != null && animais.Count > 0)
                comboBoxAnimaisCadastrados.Items.AddRange(animais.ToArray());

        }
        /// <summary>
        /// Closes the current form.
        /// </summary>
        private void CancelarCadastro(object sender, EventArgs e)
        {
            _controller.Clear();
            this.Close();
        }
        /// <summary>
        /// Adds the selected animal from the ComboBox to the listBoxAnimaisToDisputa control.
        /// </summary>
        /// <param name="sender">The ComboBox control that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void AnimalToLisBoxAnimais(object sender, EventArgs e)
        {
            ComboBox? combo = sender as ComboBox;

            if (combo != null)
                if (combo.SelectedItem != null)
                {
                    var animalSelecionadoUi = combo.SelectedItem;
                    listBoxAnimaisToDisputa.Items.Add(animalSelecionadoUi);

                }
        }
        /// <summary>
        /// Removes the currently selected animal from the list.
        /// </summary>
        /// <remarks>This method is intended to be used as an event handler for a <see cref="ListBox"/>
        /// control. It removes the selected item from the list if an item is selected.</remarks>
        /// <param name="sender">The source of the event, expected to be a <see cref="ListBox"/>.</param>
        /// <param name="e">The event data associated with the removal action.</param>
        private void RemoveAnimal(object sender, EventArgs e)
        {
            ListBox? list = sender as ListBox;
            if (list != null)
                if (list.SelectedItem != null)
                {
                    list.Items.Remove(list.SelectedItem);
                }
        }
        /// <summary>
        /// Registers a new competition with the specified name, date, and list of animals.
        /// </summary>
        /// <remarks>This method collects input from the user interface, including the competition name,
        /// date, and a list of animals. It validates that a name and at least one animal are provided before proceeding
        /// with the registration. If the inputs are valid, it calls the <see
        /// cref="DisputaController.Cadastrar"/> method to register the competition and displays a message
        /// indicating the result. The input fields are cleared after registration.</remarks>
        /// <param name="sender">The source of the event, typically a button.</param>
        /// <param name="e">The event data associated with the click event.</param>
        private void CadastrarDisputa(object sender, EventArgs e)
        {
            string nomeDisputa = String.Empty;
            DateTime? date = null;
            nomeDisputa = textBoxNomeDaDisputa.Text;
            string mensagem = String.Empty;
            date = DateTime.Parse(dateTimePicker1.Text);
            int quantidadeRodadas = (int)numericUpDownQuantidadeRodadas.Value;
            //_controller.InitRodadasController();
            if (String.IsNullOrEmpty(nomeDisputa))
            {
                MessageBox.Show("Precisa colocar um nome para a disputa!");
            }
            if (listBoxAnimaisToDisputa.Items.Count == 0)
            {
                MessageBox.Show("Precisa colocar Pelomenos um ou mais animais para essa disputa!");
            }
            else
            {
                //mensagem = DisputaController.Cadastrar(nomeDisputa, date, listBoxAnimaisToDisputa.Items);

                mensagem = _controller.Cadastrar(nomeDisputa, date, listBoxAnimaisToDisputa.Items, quantidadeRodadas);

                MessageBox.Show(mensagem);
                //limpa os dados
                textBoxNomeDaDisputa.Text = String.Empty;
                dateTimePicker1.Text = String.Empty;
                listBoxAnimaisToDisputa.Items.Clear();
            }
        }

        private void FromClosed(object sender, FormClosedEventArgs e)
        {
            _controller.Clear();
        }
    }
}
