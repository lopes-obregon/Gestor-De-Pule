using Gestor_De_Pule.src.Controllers;
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
using System.Globalization;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
        private FinanceiroController _financeiroController;
        private DisputaController _disputaController;
        public Main()
        {
            _financeiroController = new FinanceiroController();
            _disputaController = new DisputaController();
            InitializeComponent();
            //MainController.LoadLists();
            InitComboBox();
            //this.Dock = DockStyle.Fill;
            //então carrego na memória.
            //FinanceiroController.LoadCaixaInit();
            _financeiroController.LoadCaixaInit();
            var caixa = _financeiroController.Caixa;
            if (caixa is null)
            {
                //então preciso criar um novo caixa;
                _financeiroController.OpenNewCaixa();
            }
            
            TabControlComand();
            dataGridViewDisputas.Dock = DockStyle.Fill;
            dataGridViewDisputas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void TabControlComand()
        {
            tabControl.TabPages[0].Text = "Rodada 1";
            tabControl.TabPages.RemoveAt(1);
        }
        /// <summary>
        /// Init combox in main to select Disputs
        /// </summary>
        private void InitComboBox()
        {
            comboBoxDisputas.Items.Clear();
            //var disputasCadastradas = MainController.ListarDisputas();
            var disputasCadastradas = _disputaController.ListarDisputas();
            if (disputasCadastradas is not null)
            {
                comboBoxDisputas.Items.AddRange(disputasCadastradas.ToArray());
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
            var disputaSelecionadaUi = comboBoxDisputas.SelectedItem;
            ClearDatagridDisputas();
            if (disputaSelecionadaUi is null)
            {
                //preencher o data grid
                SetDataGridDisputa();
            }
            else
            {
                SetDataGridDisputa(disputaSelecionadaUi);
            }

        }

        private void SetDataGridDisputa(object disputaSelecionadaUi)
        {
            int totalAbas = tabControl.TabPages.Count;
            //var disputaSelecionadoDb = MainController.BuscarDisputa(disputaSelecionadaUi);
            _disputaController.BuscarDisputa(disputaSelecionadaUi);
            _disputaController.LoadRodada();
            var disputaSelecionadoDb = _disputaController.Disputa;

            if (disputaSelecionadoDb is not null)
            {
                labelDisputaNome.Text = "Disputa:" + disputaSelecionadoDb.Nome;
                foreach (var resultado in disputaSelecionadoDb.ResultadoList)
                {
                    dataGridViewDisputas.Rows.Add(resultado.Animal.Nome, resultado.Posição, resultado.Tempo);
                }
                var rodadas = disputaSelecionadoDb.Rodadas;

                if (rodadas is not null && rodadas.Count > 0 && rodadas[0].Nrodadas != totalAbas)
                {
                    foreach(var rodada in rodadas)
                    {
                        if(rodada is not null)
                        {
                            for (int i = 0; i < rodada.Nrodadas + 1; i++)
                            {
                                if (i > 1)
                                {
                                    //para cada rodada nova adiciona na pagina
                                    TabPage tabPage = new TabPage($"Rodada {i}");
                                    NewDataGridPage(tabPage, rodada );
                                    tabControl.TabPages.Add(tabPage);

                                }
                            }

                        }
                    }
                }
                else
                {
                    var pages = tabControl.TabPages;
                    if(pages is not null)
                    {
                        for (int i = 0; i < pages.Count; i++)
                        {
                            var page = pages[i];
                            if(page is not null & i > 0)
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
            DataGridView dataGridView = new DataGridView();// novo data grid
            dataGridView.Dock = DockStyle.Fill; // ocupa toda a tela
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //collums
            dataGridView.Columns.Add("nome", "Nome");
            dataGridView.Columns.Add("posição", "Posição");
            dataGridView.Columns.Add("tempo", "Tempo");
            var resultados = rodada.ResultadoDestaRodada;  //lista que contem os animais da rodada
           
            if(resultados  is not null && resultados.Count > 0)
            {
                foreach(var resultado in resultados)
                {
                    if(resultado is not null)
                    {
                        
                        var animal = resultado.Animal;
                        if(resultado is not null)
                        {
                            if(animal is not null)
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

        private void SetDataGridDisputa()
        {
            //var disputaCadastrados = MainController.ListarDisputas();
            var disputaCadastrados = _disputaController.ListarDisputas();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SalvarDados(object sender, EventArgs e)
        {
            var disputaSelecionado = comboBoxDisputas.SelectedItem;
            if (disputaSelecionado is not null)
            {
               // DisputaController.LoadDisputa(disputaSelecionado);
               _disputaController.LoadDisputa(disputaSelecionado);
                var disputa = _disputaController.Disputa;
                if (disputa is not null)
                {
                    int quantiaAnimais = disputa.GetNumAnimais();
                    for (int i = 0; i < quantiaAnimais; i++)
                    {
                        //var tempo = dataGridViewDisputas.SelectedRows[i].Cells[2].Value;
                        var tempo = dataGridViewDisputas.Rows[i].Cells[2].Value;
                        var animal = GetAnimal(i);
                        if (tempo is not null && animal is not null)
                        {
                            string? tempoStr = tempo.ToString();

                            if (!String.IsNullOrEmpty(tempoStr))
                            {
                                bool valido = TimeSpan.TryParseExact(
                                    tempoStr,
                                    @"hh\:mm\:ss\,ff",
                                    CultureInfo.InvariantCulture,
                                    out TimeSpan resultado
                                    );

                                if (valido)
                                {
                                    //MainController.SalvarDisputa(tempoStr);
                                    //DisputaController.SalvarDisputa(animal, resultado);
                                    _disputaController.SalvarDisputa(animal, resultado);
                                }
                                else
                                {
                                    MessageBox.Show("Por favor tempo está no formato Errado corrija para hh:mm:ss,ff");
                                }
                            }
                        }
                    }
                    if (quantiaAnimais > 1)
                    {
                        disputa.ajustarPosiçãoDosAnimais();
                    }

                }


            }
        }
        private object? GetAnimal(int i)
        {
            var animal = dataGridViewDisputas.Rows[i].Cells[0].Value;
            if (animal is not null)
                return animal;
            else return null;
        }

        private void CalcularPosição(object sender, EventArgs e)
        {
                int indexPage = tabControl.SelectedIndex;           
                var disputa = _disputaController.Disputa;
            if (disputa is not null)
            {
                //primeira página    
                if (indexPage == 0)
                {
                    var tempoAnimal1 = dataGridViewDisputas.Rows[0].Cells[2].Value;
                    var animal1 = dataGridViewDisputas.Rows[0].Cells[0].Value.ToString();
                    var tempoAnimal2 = dataGridViewDisputas.Rows[1].Cells[2].Value;
                    var animal2 = dataGridViewDisputas.Rows[1].Cells[0].Value.ToString();

                    disputa.ajustarPosiçãoDosAnimais(tempoAnimal1, tempoAnimal2);
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
                            var tempoAnimal2 = dgv.Rows[1].Cells[2].Value;
                            var animal2 = dgv.Rows[1].Cells[0].Value.ToString();
                            disputa.ajustarPosiçãoDosAnimais(tempoAnimal1, tempoAnimal2);
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
            var disputaMemória = _disputaController.Disputa;


            if (disputaMemória is not null)
            {
                //fazer uma atualização com os dados do banco.
                //var disputa = Disputa.Reload(disputaMemória);
                var disputa = _disputaController.Reload(disputaMemória);
                if (disputa is not null)
                {
                    labelVitória.Text = "Vitória: " + disputa.GetNomeAnimalVencedor();
                    labelTotalGanhadores.Text = "Total Ganhadores: " + disputa.CntTotalGanhadoresPules().ToString();
                    labelPagamentoPorPule.Text = "Pagamento Por Pule: " + disputa.PagamentoPorPule();

                }
            }

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
    }
}
