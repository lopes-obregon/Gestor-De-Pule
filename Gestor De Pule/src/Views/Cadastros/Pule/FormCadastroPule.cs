using Gestor_De_Pule.src.Controllers;


namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormCadastroPule : Form
    {
        private PuleController _puleController;
        public FormCadastroPule()
        {
            _puleController = new PuleController();
            InitializeComponent();
            SetComboBox();
        }

        private void SetComboBox()
        {

            //PuleController.LoadLists();
            _puleController.LoadLists();
            //var animaisCadastrados = PuleController.Animals;
            var animaisCadastrados = _puleController.Animals;
            var ApostadoresCadastrados = _puleController.Apostadors;
            var disputasCadastrados = _puleController.Disputas;
            if (ApostadoresCadastrados is not null)
                comboBoxApostadores.Items.AddRange(ApostadoresCadastrados.ToArray());
            comboBoxPagamento.DataSource = Enum.GetValues(typeof(Gestor_De_Pule.src.Model.StatusPagamento));
            if (animaisCadastrados is not null)
                comboBoxAnimais.Items.AddRange(animaisCadastrados.ToArray());
            if (disputasCadastrados is not null)
                comboBoxDisputas.Items.AddRange(disputasCadastrados.ToArray());
        }

        private void AnimalSelecionadoUi(object sender, EventArgs e)
        {
            var animalSelecionado = comboBoxAnimais.SelectedItem;
            if (listBoxAnimaisSelecionados.Items.Count == 0)
            {
                if (animalSelecionado is not null)
                    listBoxAnimaisSelecionados.Items.Add(animalSelecionado);

            }
            else
                MessageBox.Show("Só Pode Selecionar 1 Animal!");
        }

        private void RemoveAnimalSelecionado(object sender, EventArgs e)
        {
            var animalSelecionado = listBoxAnimaisSelecionados.SelectedItem;
            if (animalSelecionado is not null)
                listBoxAnimaisSelecionados.Items.Remove(animalSelecionado);
        }

        private void SalvarPule(object sender, EventArgs e)
        {
            var apostadorSelecionado = comboBoxApostadores.SelectedItem;
            var pagamento = comboBoxPagamento.SelectedItem;
            var animaisSelecionados = listBoxAnimaisSelecionados.Items;
            var disputaSelecionado = comboBoxDisputas.SelectedItem;
            float valor = (float)numericUpDownValorPule.Value;
            int númeroDoPule = (int)numericUpDownNúmeroPule.Value;
            string mensagem = String.Empty;
            if (disputaSelecionado is null)
                mensagem += "Por Favor Selecione uma Disputa ";
            if (apostadorSelecionado is null)
                mensagem += "Por Favor Selecione um Apostador ";
            if (pagamento is null)
                mensagem += "Por Favor Seleciona Uma Forma De Pagamento ";
            if (animaisSelecionados.Count < 1)
                mensagem += "Por Favor Selecione Pelomenos Um Animal Para Apostar ";
            else
                mensagem = _puleController.CadastrarPule(apostadorSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule, disputaSelecionado);
                //mensagem = PuleController.CadastrarPule(apostadorSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule, disputaSelecionado);
            MessageBox.Show(mensagem);
            //limpeza dos campos
            comboBoxAnimais.SelectedIndex = -1;
            comboBoxApostadores.SelectedIndex = -1;
            numericUpDownNúmeroPule.Value = 0;
            numericUpDownValorPule.Value = 0;
            listBoxAnimaisSelecionados.Items.Clear();

        }

        private void FecharCadastros(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxDisputa(object sender, EventArgs e)
        {
            var DisputaSelecionada = comboBoxDisputas.SelectedItem;
            if (DisputaSelecionada is not null)
            {
                //var animais = PuleController.AttComboBoxAnimais(DisputaSelecionada);
                var animais = _puleController.AttComboBoxAnimais(DisputaSelecionada);
                comboBoxAnimais.Items.Clear();
                if(animais is not null && animais.Count > 0)
                    comboBoxAnimais.Items.AddRange(animais.ToArray());
            }

        }
    }
}
