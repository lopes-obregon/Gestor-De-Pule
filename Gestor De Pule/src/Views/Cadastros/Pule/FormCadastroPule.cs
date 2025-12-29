using Gestor_De_Pule.src.Controllers;


namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormCadastroPule : Form
    {
        public FormCadastroPule()
        {
            InitializeComponent();
            SetComboBox();
        }

        private void SetComboBox()
        {

            PuleController.LoadLists();
            var animaisCadastrados = PuleController.Animals;
            var ApostadoresCadastrados = PuleController.Apostadors;
            if (ApostadoresCadastrados is not null)
                comboBoxApostadores.Items.AddRange(ApostadoresCadastrados.ToArray());
            comboBoxPagamento.DataSource = Enum.GetValues(typeof(Gestor_De_Pule.src.Model.StatusPagamento));
            if (animaisCadastrados is not null)
                comboBoxAnimais.Items.AddRange(animaisCadastrados.ToArray());
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
            float valor = (float) numericUpDownValorPule.Value;
            int númeroDoPule = (int) numericUpDownNúmeroPule.Value;
            string mensagem = String.Empty;
            if (apostadorSelecionado is null)
                mensagem += "Por Favor Selecione um Apostador ";
            if (pagamento is null)
                mensagem += "Por Favor Seleciona Uma Forma De Pagamento ";
            if (animaisSelecionados.Count < 1)
                mensagem += "Por Favor Selecione Pelomenos Um Animal Para Apostar ";
            else
                mensagem = PuleController.CadastrarPule(apostadorSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule);
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
    }
}
