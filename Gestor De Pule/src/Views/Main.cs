using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Model;
using Gestor_De_Pule.src.Models;
using Gestor_De_Pule.src.Views;
using Gestor_De_Pule.src.Views.Apostador;
using Gestor_De_Pule.src.Views.Cadastros;
using Gestor_De_Pule.src.Views.Financeiro.FluxoCaixa;
using Gestor_De_Pule.src.Views.Financeiro.Taxa;
using Gestor_De_Pule.src.Views.Pule;
using Gestor_De_Pule.src.Views.Relatórios.Animal;
using Gestor_De_Pule.src.Views.Relatórios.Apostador;
using Gestor_De_Pule.src.Views.Relatórios.Disputa;
using Gestor_De_Pule.src.Views.Relatórios.Pule;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
      
        private ViewController _viewController;
        public Main()
        {
           
            _viewController = new();

            InitializeComponent();
            //MainController.LoadLists();
            InitComboBox();
            //this.Dock = DockStyle.Fill;
            //então carrego na memória.
            //FinanceiroController.LoadCaixaInit();
           // _financeiroController.LoadCaixaInit();
            _viewController.LoadCaixa();
            var caixa = _viewController.GetCaixa();
           /* if (caixa is null)
            {
                //então preciso criar um novo caixa;
                _financeiroController.OpenNewCaixa();
            }*/

            TabControlComand();
            dataGridViewDisputas.Dock = DockStyle.Fill;
            dataGridViewDisputas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void TabControlComand()
        {
            tabControl.TabPages[0].Text = "Resumo";
            tabControl.TabPages.RemoveAt(1);
        }
        /// <summary>
        /// Init combox in main to select Disputs
        /// </summary>
        private void InitComboBox()
        {
            //comboBoxDisputas.Items.Clear();
            comboBoxDisputas.DataSource = null;
            //var disputasCadastradas = MainController.ListarDisputas();
            var disputasCadastradas = _viewController.ListarDisputas();
            //var disputasCadastradas = _disputaController.ListarDisputas();
            if (disputasCadastradas is not null)
            {
                comboBoxDisputas.DisplayMember = "Nome";
                comboBoxDisputas.ValueMember = "Id";
                comboBoxDisputas.DataSource = disputasCadastradas;
            }
            /*comboBoxApostadores.Items.Clear();
            comboBoxAnimais.Items.Clear();
            if (MainController.Apostadors.Count > 0)
            {
                comboBoxApostadores.Items.AddRange(MainController.Apostadors.ToArray());
            }
            if (MainController.Animals.Count > 0)
            {
                comboBoxAnimais.Items.AddRange(MainController.Animals.ToArray());
            }*/

        }

        private void JanelaCadastro(object sender, EventArgs e)
        {
            OpenWindowWpfCadastro();
        }

        private void OpenWindowWpfCadastro()
        {
            var window = new WindowAnimalCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void OpenWindowApostadoresCadastrados(object sender, EventArgs e)
        {
            var window = new WindowApostadoresCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void WindowPuleCadastrados(object sender, EventArgs e)
        {
            var window = new WindowPuleCadastrados();
            window.ShowDialog();
            InitComboBox();
        }

        private void GerarRelatório(object sender, EventArgs e)
        {

        }


        private void listViewAnimais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ApostadorWindow(object sender, EventArgs e)
        {
            var form = new FormRelatórioApostador();
            form.ShowDialog();
        }

        private void AnimalForm(object sender, EventArgs e)
        {
            var form = new FormRelatórioAnimal();
            form.ShowDialog();
        }



        private void CloseSystem(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowRelatórioPule(object sender, EventArgs e)
        {
            var window = new WindowRelatórioPule();
            window.ShowDialog();
        }

        private void DisputaCadastradosWindow(object sender, EventArgs e)
        {
            var window = new WindowCadastroDisputa();
            window.ShowDialog();
        }
        /// <summary>
        /// Search a selected disput
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuscarDisputa(object sender, EventArgs e)
        {
            int idDisputa = (int)comboBoxDisputas.SelectedValue;
            
            ClearDatagridDisputas();
            ClearTab();
            if (idDisputa == 0)
            {
                //preencher o data grid
                SetDataGridDisputa();
            }
            else
            {
                SetDataGridDisputa(idDisputa);
            }

        }

        private void ClearTab()
        {
            var pages = tabControl.TabPages;
            if (pages is not null)
            {
                while (pages.Count > 1)
                {
                    pages.RemoveAt(pages.Count - 1);
                }
            }
        }

        private void SetDataGridDisputa(int idDisputa)
        {
            int totalAbas = tabControl.TabPages.Count;
            int cnt = 0;
            //var disputaSelecionadoDb = MainController.BuscarDisputa(disputaSelecionadaUi);
            //_disputaController.GetById(idDisputa);
            _viewController.SetDisputa(idDisputa);
            _viewController.LoadRodadas();
            //_disputaController.LoadRodada();
            //var disputa = _disputaController.Disputa;
            var disputa = _viewController.GetDisputa() as Disputa;

            if (disputa is not null)
            {
                labelDisputaNome.Text = "Disputa:" + disputa.Nome;
                if (disputa.ResultadoList is not null && disputa.ResultadoList.Count > 0)
                {
                    foreach (var resultado in disputa.ResultadoList)
                    {
                        if (resultado is not null)
                        {
                            dataGridViewDisputas.Rows.Add(resultado.Animal.Nome, resultado.Posição, resultado.Tempo);

                        }
                    }
                }
                var rodadas = disputa.Rodadas;

                if (rodadas is not null && rodadas.Count > 0 && disputa.GetNMaiorRodada() > totalAbas - 1)
                {

                    foreach (var rodada in rodadas)
                    {
                        if (rodada is not null)
                        {
                            //para cada rodada nova adiciona na pagina
                            TabPage tabPage = new TabPage($"Rodada {++cnt}");
                            NewDataGridPage(tabPage, rodada);
                            tabControl.TabPages.Add(tabPage);


                        }
                    }
                }
                else
                {
                    var pages = tabControl.TabPages;
                    if (pages is not null)
                    {
                        for (int i = 0; i < pages.Count; i++)
                        {
                            var page = pages[i];
                            if (page is not null & i > 0)
                            {
                                tabControl.TabPages.Remove(page);
                            }
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Add data grid view in tab page
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="rodada"></param>
        private void NewDataGridPage(TabPage tabPage, Rodada rodada)
        {
            if (rodada.ResultadoDestaRodada is null)
            {
                _viewController.LoadResultados();
                //_resultadoController.LoadResultados();
                rodada.ResultadoDestaRodada = _viewController.GetResultados(rodada.Id);


                //rodada.ResultadoDestaRodada = _resultadoController.GetResultados(rodada.Id);
            }

            DataGridView dataGridView = new DataGridView();// novo data grid
            dataGridView.Dock = DockStyle.Fill; // ocupa toda a tela
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //collums
            dataGridView.Columns.Add("nome", "Nome");
            dataGridView.Columns.Add("posição", "Posição");
            dataGridView.Columns.Add("tempo", "Tempo");

            var resultados = rodada.ResultadoDestaRodada;  //lista que contem os animais da rodada

            if (resultados is not null && resultados.Count > 0)
            {
                foreach (var resultado in resultados)
                {
                    if (resultado is not null)
                    {
                        var animalId = 0;
                        if (resultado.Animal != null)
                            animalId = resultado.Animal.Id;
                        else animalId = resultado.AnimalId;
                        
                        var animal = _viewController.GetAnimalById(animalId) as Animal;
                        //var animal = _animalController.GetAnimalById(animalId);
                        if (resultado is not null)
                        {
                            if (animal is not null)
                                dataGridView.Rows.Add(animal.Nome, resultado.Posição, resultado.Tempo + ",00");

                        }


                    }
                }
            }

            tabPage.Controls.Add(dataGridView);

        }

        private void ClearDatagridDisputas()
        {
            dataGridViewDisputas.Rows.Clear();
        }

        private void SetDataGridDisputa(bool isNewRod = false)
        {
            //var disputaCadastrados = MainController.ListarDisputas();
            int cnt = 0;
            if (!isNewRod)
            {
                var disputaCadastrados = _viewController.ListarDisputas() as List<Disputa>;
                if (disputaCadastrados is not null)
                {
                    foreach (var disputa in disputaCadastrados)
                    {
                        if (disputa is not null && disputa.ResultadoList.Count > 0)
                        {
                            foreach (var resultado in disputa.ResultadoList)
                            {
                                dataGridViewDisputas.Rows.Add(resultado.Animal.Nome, resultado.Posição, resultado.Tempo);
                            }

                        }
                    }
                }
                disputaCadastrados = _viewController.ListarDisputas() as List<Disputa>;
                if (disputaCadastrados is not null)
                {
                    foreach (var disputa in disputaCadastrados)
                    {
                        if (disputa is not null && disputa.ResultadoList.Count > 0)
                        {
                            foreach (var resultado in disputa.ResultadoList)
                            {
                                dataGridViewDisputas.Rows.Add(resultado.Animal.Nome, resultado.Posição, resultado.Tempo);
                            }

                        }
                    }
                }
            }
            else
            {
                var disputa = _viewController.GetDisputa() as Disputa;
                var rodadas = disputa?.Rodadas;

                if (disputa is not null)
                {
                    if (rodadas is not null)
                    {
                        cnt = rodadas.Count;
                        foreach (var rodada in rodadas.Skip(rodadas.Count - 1))
                        {
                            if (rodada is not null)
                            {
                                var pages = tabControl.TabPages;
                                if (pages is not null)
                                {
                                    //cada rodada nova quero uma nova pag
                                    TabPage tabPage = new TabPage($"Rodada {cnt}");
                                    NewDataGridPage(tabPage, rodada);
                                    tabControl.TabPages.Add(tabPage);

                                }
                            }
                        }
                    }
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// Save data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalvarDados(object sender, EventArgs e)
        {
            string mensagem = String.Empty;
            var disputa = _viewController.GetDisputa() as Disputa;
            if (disputa is not null)
            {
                // DisputaController.LoadDisputa(disputaSelecionado);
                mensagem = _viewController.SalvarDisputa();
            }
            else
            {
                mensagem = "Erro Ao tentar salvar a Disputa";
            }
            MessageBox.Show(mensagem);
        }


        /// <summary>
        /// Calculo da posição do animanl na view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcularPosição(object sender, EventArgs e)
        {
            int indexPage = tabControl.SelectedIndex;
            var disputa = _viewController.GetDisputa() as Disputa;
            
            if (disputa is not null)
            {
                //primeira página    
                if (indexPage == 0)
                {
                    var tempoAnimal1 = dataGridViewDisputas.Rows[0].Cells[2].Value;
                    var animal1 = dataGridViewDisputas.Rows[0].Cells[0].Value.ToString();
                    var tempoAnimal2 = dataGridViewDisputas.Rows[1].Cells[2].Value;
                    var animal2 = dataGridViewDisputas.Rows[1].Cells[0].Value.ToString();

                    disputa.ajustarPosiçãoDosAnimais(tempoAnimal1, tempoAnimal2, true);
                    //disputa.Atualizar();
                    //dataGridViewDisputas.Rows.Clear();

                    //SetDataGridDisputa(disputa);
                    if (!(String.IsNullOrEmpty(animal1) && String.IsNullOrEmpty(animal2)))
                    {
                        var resultados = disputa.ResultadoList;
                        if (resultados is not null)
                        {
                            foreach (var resultado in resultados)
                            {
                                if (resultado is not null)
                                {
                                    var animal = resultado.Animal;
                                    if (animal is not null)
                                    {
                                        if (String.Equals(animal1, animal.Nome))
                                        {
                                            dataGridViewDisputas.Rows[0].Cells[1].Value = resultado.Posição;
                                        }
                                        else if (String.Equals(animal2, animal.Nome))
                                        {
                                            dataGridViewDisputas.Rows[1].Cells[1].Value = resultado.Posição;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var aba = tabControl.SelectedTab;
                    if (aba is not null)
                    {
                        DataGridView dgv = aba.Controls[0] as DataGridView;
                        if (dgv is not null)
                        {

                            var tempoAnimal1 = dgv.Rows[0].Cells[2].Value.ToString();
                            var animal1 = dgv.Rows[0].Cells[0].Value.ToString();
                            var tempoAnimal2 = dgv.Rows[1].Cells[2].Value.ToString();
                            var animal2 = dgv.Rows[1].Cells[0].Value.ToString();
                            disputa.ajustarPosiçãoDosAnimais(tempoAnimal1, tempoAnimal2, indexPage-1);
                            if (!(String.IsNullOrEmpty(animal1) && String.IsNullOrEmpty(animal2)))
                            {
                                //Parei aqui
                                //ajustar rodada para aba espeficifica
                                var rodada = disputa.Rodadas[indexPage-1];
                                if (rodada is not null)
                                {
                                   if(rodada.ResultadoDestaRodada is not null)
                                    {
                                        foreach (var resultado in rodada.ResultadoDestaRodada)
                                        {
                                            if (resultado is not null)
                                            {
                                                var animal = resultado.Animal;
                                                if (animal is not null)
                                                {
                                                    if (String.Equals(animal1, animal.Nome))
                                                    {
                                                        dgv.Rows[0].Cells[1].Value = resultado.Posição;
                                                    }
                                                    else if (String.Equals(animal2, animal.Nome))
                                                    {
                                                        dgv.Rows[1].Cells[1].Value = resultado.Posição;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                        
                                    
                                }
                            }

                        }
                    }
                }
            }

        }

        private void TaxaView(object sender, EventArgs e)
        {
            var form = new FormCadastroDeTaxaEAtt();
            form.ShowDialog();
        }

        private void CalcularPrêmio(object sender, EventArgs e)
        {
            _viewController.LoadEntToPrice();

            labelVitória.Text = _viewController.GetNomeAnimaisVencedores();
            labelTotalGanhadores.Text = _viewController.TotalGanhadoresPulesPorRodada();
            labelPagamentoPorPule.Text = _viewController.PagamentoPorPule();
                //labelTotalGanhadores.Text = "Total Ganhadores: " + disputa.CntTotalGanhadoresPules().ToString();
                /*labelTotalGanhadores.Text = "Total Ganhadores: " + disputa.CntTotalGanhadoresPulesToLists();
              
                    labelPagamentoPorPule.Text = "Pagamento Por Pule: " + disputa.PagamentoPorPule();*/

            

        }

        private void FluxoCaixaView(object sender, EventArgs e)
        {
            WindowFluxoCaixa window = new WindowFluxoCaixa();
            window.ShowDialog();
        }

        private void RelatórioDisputa(object sender, EventArgs e)
        {
            WindowRelatórioDisputa windo = new WindowRelatórioDisputa();
            windo.ShowDialog();
        }
        /// <summary>
        /// Gera nova rodada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NovaRodada(object sender, EventArgs e)
        {
            var disputa = _viewController.GetDisputa() as Disputa;
            var rodadas = disputa?.Rodadas;
            byte nRodadas = disputa?.Rodadas.Max(res => res.Nrodadas) ?? 0;
            if (disputa is not null)
            {
                //se rodada não é nula podemos criar novas rodadas

                if (_viewController is not null)
                {
                    //_rodadaController.NovaRodada();
                    //var rodadaNova = _rodadaController.Rodada;
                    var rodadaNova = _viewController.NovaRodada();
                    if (rodadaNova is not null)
                    {
                        rodadaNova.Disputa = disputa;
                        rodadaNova.Nrodadas = ++nRodadas;

                        //seleciona e devolve todos os animais da lista de resultados
                        //var animais = disputa.ResultadoList?.Select(res => res.Animal).ToList();
                        var animais = _viewController.GetAnimals(disputa.GetAnimalsRodadasIds());
                        if (animais is not null)
                        {
                            foreach (var animal in animais)
                            {
                                if (animal is not null){
                                    //_disputaController.ResultadoControlleNovoResultado();
                                     var resultado = _viewController.NovoResultado();
                                    //var resultado = _disputaController.ResultadoController.Resultado;
                                    if (resultado is not null)
                                    {
                                        resultado.Disputa = disputa;
                                        resultado.Animal = animal;
                                        animal.Resultados.Add(resultado);
                                        if (rodadaNova.ResultadoDestaRodada is null)
                                            rodadaNova.ResultadoDestaRodada = new();
                                        rodadaNova.ResultadoDestaRodada.Add(resultado);
                                    }
                                }
                            }
                            disputa.Rodadas?.Add(rodadaNova);
                        }
                    }

                }
                SetDataGridDisputa(true);
            }
            else
            {
                MessageBox.Show("Erro Disputa com uma Rodada ou erro interno, Por Favor Tente Buscar a Disputa Primeiro!");
            }
        }
    }
}
 
