using Gestor_De_Pule.src.Controllers;
namespace Gestor_De_Pule.src.Views.Cadastros.Disputa
{
    public partial class FormDisputa : Form
    {
        private DisputaController _controllerDisputa = new DisputaController(true);
        private AnimalController _animController;
        private ResultadoController _resultadoController;
        private RodadaController _rdadaController;
        private CaixaController _caixaController;
        private bool _isAtt = false;
        public FormDisputa()
        {
            InitializeComponent();
            //_controller.InitAnimalController();
            var context = _controllerDisputa.GetContext();
            _animController = new AnimalController(context.GetDataBase());
            _resultadoController = new ResultadoController(context.GetDataBase());
            _rdadaController = new RodadaController(context.GetDataBase());
            _caixaController = new CaixaController(context.GetDataBase());
            _caixaController.LoadCaixa();
            InitComboBoxs();
        }
        public FormDisputa(object disputaUi)
        {
            var context = _controllerDisputa.GetContext().GetDataBase();
            _animController = new AnimalController(context);
            _resultadoController = new ResultadoController(context);
            InitializeComponent();
            this.Text = "Atualizar Disputa";
            int disputaId = int.Parse(disputaUi.ToString());
            _controllerDisputa.LoadDisputa(disputaId);
            InitComboBoxs();
            SetItens();
            _isAtt = true;

        }

        private void SetItens()
        {
            var disputa = _controllerDisputa.Disputa;
            if (disputa != null)
            {
                textBoxNomeDaDisputa.Text = disputa.Nome;
                dateTimePicker1.Value = disputa.DataEHora;
                disputa.ResultadoList = null;//limpar o lixo
                numericUpDownQuantidadeRodadas.Value = disputa.GetNMaiorRodada();
                if(disputa.Rodadas is not null )
                    {

                    //var list = disputa.ResultadoList.Select(_ => _.Animal).ToArray();
                    //var list = disputa.Rodadas.SelectMany(rod => rod.ResultadoDestaRodada).Select(res => res.Animal);
                    _resultadoController.LoadResultados();
                    _animController.LoadAnimais();
                    var listAnimalId = _resultadoController.Resultados.Where(res => res.DisputaId == disputa.Id).Select(res => res.AnimalId).ToList();
                    var listAnimal = _animController.Animals.Where(a => listAnimalId.Contains(a.Id)).ToList();
                   if(listAnimal != null)
                    {
                        listBoxAnimaisToDisputa.Items.AddRange(listAnimal.Cast<object>().ToArray());
                        listBoxAnimaisToDisputa.DisplayMember = "Nome";
                        listBoxAnimaisToDisputa.ValueMember = "Id";
                    }
                    
                }
                
            }
            
        }

        /// <summary>
        /// Initializes the combo box with a list of registered animals by loading data from the controller and adding
        /// the animals to the combo box items.
        /// </summary>
        private void InitComboBoxs()
        {
            //DisputaController.LoadLists();
            //_controllerDisputa.LoadLists();
            _animController.LoadAnimais();
            var animais = _animController.Animals;
            if (animais != null && animais.Count > 0)
                comboBoxAnimaisCadastrados.Items.AddRange(animais.ToArray());

        }
        /// <summary>
        /// Closes the current form.
        /// </summary>
        private void CancelarCadastro(object sender, EventArgs e)
        {
            _controllerDisputa.Clear();
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
            int i = 0;

            if (combo != null)
                if (combo.SelectedItem != null)
                {
                    var animalSelecionadoUi = combo.SelectedItem;
                    if(animalSelecionadoUi is not null)
                    {
                        /*_animController.AnimalSelecionado(animalSelecionadoUi);
                        var animal = _animController.Animal;
                        var disputa = _controllerDisputa.Disputa;
                        if(disputa is not null && _isAtt)
                        {
                            //baseado na rodada antiga
                            while(i < disputa.GetNMaiorRodada())
                            {
                                _resultadoController.NovoResultado();
                                var resultado = _resultadoController.Resultado;
                                if(resultado is not null)
                                {
                                    resultado.Animal = animal;
                                    resultado.Disputa = disputa;
                                    
                                    disputa.AddNewResultadoInRodada(resultado, i);
                                }
                                i++;

                            }

                        }*/
                        
                        listBoxAnimaisToDisputa.Items.Add(animalSelecionadoUi);

                    }

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
                    
                    if (_isAtt) {
                        var animalSelecionado = list.SelectedItem;
                       // _animController.LoadAnimalWithListResultado(list.SelectedItem);
                        //var animal = _animController.Animal;
                       /* var disputa = _controllerDisputa.Disputa;
                        
                        if (animalSelecionado != null)
                        {
                            if(disputa != null)
                            {
                                _animController.AnimalSelecionado(animalSelecionado);
                                //var resultado = animal.Resultados.FirstOrDefault(_ => _.Disputa?.Id == disputa.Id);
                                var resultados = _resultadoController.Resultados?.Where(res => res.AnimalId == _animController.Animal?.Id && res.DisputaId == disputa.Id).ToList();
                                if(resultados != null)
                                {
                                    foreach (var resultado in resultados)
                                    {
                                        resultado.Disputa = null;
                                        //disputa.RemoveResultado(resultado);
                                        disputa.RemoveResultadoRodada(resultado);

                                    }
                                }
                            }
                        }*/
                       // _controller.RemoveAnimalDisputa(animal);
                        list.Items.Remove(list.SelectedItem); 
                    }

                    else list.Items.Remove(list.SelectedItem);
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
            var idsAnimais = _animController.GetListId(listBoxAnimaisToDisputa.Items);
            int i = 0;
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
                if (_isAtt)
                    //passar uma lista com o id dos animais
                    mensagem = _controllerDisputa.AtualizarDados(nomeDisputa, date, idsAnimais, quantidadeRodadas);
                else
                {
                    var caixa = _caixaController.Caixa;
                    if (caixa != null)
                    {
                        var disputa = _controllerDisputa.NovaDisputa(nomeDisputa, date, caixa);

                        if (disputa != null)
                        {
                            if (disputa.Rodadas is null)
                            {
                                disputa.Rodadas = new();
                            }
                            var animais = _animController.Animals.Where(a => idsAnimais.Any(id => id == a.Id)).ToList();
                            while (i < quantidadeRodadas)
                            {

                                var rodada = _rdadaController.NovaRodada(disputa, i+1);
                                if(rodada != null)
                                {
                                    foreach(var animal in animais)
                                    {
                                        if(animal is not null)
                                        {
                                            var resultado = _resultadoController.NovoResultado(disputa,  animal, rodada);
                                            if(resultado != null)
                                            {
                                                if(rodada.ResultadoDestaRodada is null)
                                                    rodada.ResultadoDestaRodada = new();
                                                rodada.ResultadoDestaRodada.Add(resultado);
                                            }
                                        }
                                    }
                                }

                                i++;
                            }
                        }
                    }
                   
                    mensagem = _controllerDisputa.SaveContext();
                }
                   //mensagem = _controllerDisputa.Cadastrar(nomeDisputa, date, listBoxAnimaisToDisputa.Items, quantidadeRodadas);

                MessageBox.Show(mensagem);
                //limpa os dados
                textBoxNomeDaDisputa.Text = String.Empty;
                dateTimePicker1.Text = String.Empty;
                listBoxAnimaisToDisputa.Items.Clear();
            }
        }

        private void FromClosed(object sender, FormClosedEventArgs e)
        {
            _controllerDisputa.Clear();
            _animController.Clear();
            _resultadoController?.Clear();
            _rdadaController?.Clear();
        }
    }
}
