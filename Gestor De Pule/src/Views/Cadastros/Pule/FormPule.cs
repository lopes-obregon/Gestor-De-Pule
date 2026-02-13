using Gestor_De_Pule.src.Controllers;


namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormPule : Form
    {
        private PuleController _puleController;
        private bool isAtt = false;
        public FormPule()
        {
            _puleController = new PuleController();
            InitializeComponent();
            SetComboBox();
        }

        public FormPule(object puleSelecionado)
        {
            isAtt = true;
            _puleController = new PuleController();
            InitializeComponent();
            SetComboBox();
            Text = "Atualizar Pule";
            _puleController.LoadFull(puleSelecionado);
            SetComponent();
        }

        private void SetComponent()
        {
            comboBoxApostadores.Enabled = false;
            var itemsComboBoxApostadores = comboBoxApostadores.Items;
            if (itemsComboBoxApostadores != null)
            {
                foreach (var item in itemsComboBoxApostadores)
                {
                    if (item is not null)
                    {
                        if (_puleController.ApostadorController.IsEquals(item))
                        {
                            //se for verdadeiro 
                            comboBoxApostadores.SelectedItem = item;
                        }
                    }
                }
            }
            //set disputa combobox
            comboBoxDisputas.Enabled = false;
            var itemsCoboBoxDisputas = comboBoxDisputas.Items;
            if(itemsCoboBoxDisputas != null)
            {
                foreach(var item in itemsCoboBoxDisputas)
                {
                    if (item is not null)
                    {
                        if (_puleController.DisputaController.IsEquals(item))
                        {
                            comboBoxDisputas.SelectedItem = item;
                        }
                    }
                }
            }
            var pule = _puleController.Pule;
            if(pule != null){

                var animaisPule = pule.Animais;
                if (animaisPule is not null)
                {
                    foreach (var animal in animaisPule)
                    {
                        listBoxAnimaisSelecionados.Items.Add(animal);
                    }
                }
                numericUpDownValorPule.Value = (decimal)pule.Valor;
                numericUpDownNúmeroPule.Value = pule.Número;
                comboBoxPagamento.SelectedItem = pule.StatusPagamento;
            }
        }

        private void SetComboBox()
        {

            //PuleController.LoadLists();
            //_puleController.LoadLists();
            _puleController.AnimalController.LoadAnimais();
            _puleController.ApostadorController.LoadApostadores();
            _puleController.DisputaController.LoadListDisputa();
            //var animaisCadastrados = PuleController.Animals;
            var animaisCadastrados = _puleController.AnimalController.Animals;
            var ApostadoresCadastrados = _puleController.ApostadorController.Apostadors;
            var disputasCadastrados = _puleController.DisputaController.Disputas;
            if (ApostadoresCadastrados is not null)
                comboBoxApostadores.Items.AddRange(ApostadoresCadastrados.ToArray());
            comboBoxPagamento.DataSource = Enum.GetValues(typeof(Model.StatusPagamento));
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
            else{
                if (isAtt)
                {
                    mensagem = _puleController.Update(animaisSelecionados, pagamento, valor, númeroDoPule);
                }
                else
                {
                    mensagem = _puleController.CadastrarPule(apostadorSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule, disputaSelecionado);

                }
            }
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
                if (animais is not null && animais.Count > 0)
                    comboBoxAnimais.Items.AddRange(animais.ToArray());
                _puleController.DisputaController.LoadDisputa(DisputaSelecionada);
            }

        }

        private void SetApostador(object sender, EventArgs e)
        {
            var apostadorSelecionado = comboBoxApostadores.SelectedItem;
            if(apostadorSelecionado is not null)
                _puleController.ApostadorController.LoadApostador(apostadorSelecionado);
        }
    }
}
