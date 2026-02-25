using Gestor_De_Pule.src.Controllers;
using System.Windows.Threading;


namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormPule : Form
    {
        private PuleController _puleController;
        private DisputaController _disputaController;
        private AnimalController _animalController;
        private ApostadorController _apostadorController;
        private bool isAtt = false;
        public FormPule()
        {
            _disputaController = new DisputaController();
            var context = _disputaController.GetContext();
            _puleController = new PuleController(context.GetDataBase());
            _animalController = new AnimalController(context.GetDataBase());
            _apostadorController = new ApostadorController(context.GetDataBase());
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
            if (itemsCoboBoxDisputas != null)
            {
                foreach (var item in itemsCoboBoxDisputas)
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
            if (pule != null)
            {

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
        /// <summary>
        /// Set combox de apostadores, animais e disputas
        /// </summary>
        private void SetComboBox()
        {

            //PuleController.LoadLists();
            //_puleController.LoadLists();
            // _puleController.AnimalController.LoadAnimais();
            //_puleController.ApostadorController.LoadApostadores();
            //_puleController.DisputaController.LoadListDisputa();
            _disputaController.LoadListDisputa();
            _animalController.LoadAnimais();
            _apostadorController.LoadApostadores();
            //var animaisCadastrados = PuleController.Animals;
            var animaisCadastrados = _animalController.Animals;
            var ApostadoresCadastrados = _apostadorController.Apostadors;
            var disputasCadastrados = _disputaController.Disputas;
            if (ApostadoresCadastrados is not null)
                comboBoxApostadores.DataSource = ApostadoresCadastrados;
            //comboBoxApostadores.Items.AddRange(ApostadoresCadastrados.ToArray());
            comboBoxPagamento.DataSource = Enum.GetValues(typeof(Model.StatusPagamento));
            if (animaisCadastrados is not null)
                comboBoxAnimais.Items.AddRange(animaisCadastrados.ToArray());
            if (disputasCadastrados is not null)
                comboBoxDisputas.DataSource = disputasCadastrados;
            //comboBoxDisputas.Items.AddRange(disputasCadastrados.ToArray());
            //----------------------Configurações do combox---------------------------
            comboBoxAnimais.DisplayMember = "Nome";
            comboBoxAnimais.ValueMember = "Id";
            comboBoxAnimais.SelectedIndex = -1;

            comboBoxApostadores.DisplayMember = "Nome";
            comboBoxApostadores.ValueMember = "Id";
            comboBoxApostadores.SelectedIndex = -1;

            comboBoxDisputas.DisplayMember = "Nome";
            comboBoxDisputas.ValueMember = "Id";
            comboBoxDisputas.SelectedIndex = -1;
            //--------------------------------------------------------
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
            _animalController.LoadAnimais();
            //var apostadorSelecionado = comboBoxApostadores.SelectedItem;
            int apostadorSelecionado;
            var pagamento = comboBoxPagamento.SelectedItem;
            var animaisSelecionados = listBoxAnimaisSelecionados.Items;
            int disputaSelecionado;
            decimal valor = numericUpDownValorPule.Value;
            int númeroDoPule = (int)numericUpDownNúmeroPule.Value;
            int rodada = (int)numericUpDownNRodada.Value;
            
            string mensagem = String.Empty;

            try
            {
                disputaSelecionado =(int) comboBoxDisputas.SelectedValue;
                apostadorSelecionado = (int)comboBoxApostadores.SelectedValue;
            }
            catch (InvalidCastException)
            {
                disputaSelecionado = 0;
                apostadorSelecionado = 0;
            }
            if (disputaSelecionado == 0)
                mensagem += "Por Favor Selecione uma Disputa ";
            if (apostadorSelecionado == 0)
                mensagem += "Por Favor Selecione um Apostador ";
            if (pagamento is null)
                mensagem += "Por Favor Seleciona Uma Forma De Pagamento ";
            if (animaisSelecionados.Count < 1)
                mensagem += "Por Favor Selecione Pelomenos Um Animal Para Apostar ";
            else
            {
                if (isAtt)
                {
                    var animais = _animalController.GetAnimals(animaisSelecionados);
                    mensagem = _puleController.Update(animais, pagamento, (float)valor, númeroDoPule, rodada);
                }
                else
                {
                    var animais = _animalController.GetAnimals(animaisSelecionados);
                    _disputaController.LoadDisputa(disputaSelecionado);
                    _disputaController.LoadRodada();
                    var disputa = _disputaController.Disputa;
                    _puleController.NovoPule(apostadorSelecionado, pagamento, animais, valor, númeroDoPule, disputaSelecionado);
                    if(disputa is not null)
                    {
                        
                        var pule = _puleController.Pule;
                        if (pule != null)
                        {
                            disputa.SetPulesRodada(rodada, pule);
                            mensagem = _puleController.SaveInContext();

                        }
                    }

                    //mensagem = _puleController.CadastrarPule(apostadorSelecionado, pagamento, animaisSelecionados, valor, númeroDoPule, disputaSelecionado);

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
            //var DisputaSelecionada = comboBoxDisputas.SelectedItem;
            int DisputaSelecionada;
            try
            {
                if (comboBoxDisputas.SelectedValue != null)
                    DisputaSelecionada = (int)comboBoxDisputas.SelectedValue;
                else
                    DisputaSelecionada = 0;
            }
            catch (InvalidCastException)
            {
                DisputaSelecionada = 0;
            }
            //var animais = PuleController.AttComboBoxAnimais(DisputaSelecionada);
            if (DisputaSelecionada != 0)
            {
                _disputaController.LoadDisputa(DisputaSelecionada);
                var disputa = _disputaController.Disputa;
                if (disputa is not null)
                {
                    //usa resultado list pq ele está vindo com todos os animais associado as rodadas
                    var animais = disputa.ResultadoList.Select(res => res.Animal).DistinctBy(a => a.Id).ToList();
                    comboBoxAnimais.Items.Clear();
                    if (animais is not null && animais.Count > 0)
                        comboBoxAnimais.Items.AddRange(animais.ToArray());
                    //comboBoxAnimais.DataSource = animais;
                    //_puleController.DisputaController.LoadDisputa(DisputaSelecionada);

                    // var animais = _puleController.AttComboBoxAnimais(DisputaSelecionada);
                }
            }

        }

        private void SetApostador(object sender, EventArgs e)
        {
            var apostadorSelecionado = comboBoxApostadores.SelectedItem;
            if (apostadorSelecionado is not null) ;
            //_puleController.ApostadorController.LoadApostador(apostadorSelecionado);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void ValueChange(object sender, EventArgs e)
        {
            //número até onde rodada pode ir 
            int nRodada = (int)numericUpDownNRodada.Value;
            _disputaController.LoadRodada();
            var disputa = _disputaController.Disputa;
            int maior;
            if (disputa is not null)
            {
               
                maior = (int)disputa.GetNMaiorRodada();
                if (nRodada >= maior)
                {
                    numericUpDownNRodada.Value = maior;
                }

            }
        }
    }
}
