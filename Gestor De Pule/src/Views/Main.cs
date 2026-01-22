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

        }

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

        /* private void GerarRelatórioAnimal(object sender, EventArgs e)
         {
             var animalSelecionadoUi = comboBoxAnimais.SelectedItem;
             MainController.AnimalSelecionado(animalSelecionadoUi);
             LimparCaposAnimalTab();

             var animal = MainController.Animal;
             if (animal != null)
             {
                 labelAnimalNome.Text = $"{animal.Número} - {animal.Nome}";
                 labelTotalPules.Text = $"Total De Pules {animal.Pules.Count}";
                 int totalApostador = 0;
                 float totalApostado = 0.0f;
                 foreach (var pule in animal.Pules)
                 {
                     var puleBuscado = MainController.SearchPule(pule);
                     if (puleBuscado is not null)
                     {
                         ListViewItem itemPule = new ListViewItem(puleBuscado.Número.ToString());
                         itemPule.SubItems.Add(puleBuscado.Valor.ToString());
                         if (puleBuscado.Apostador is not null)
                         {
                             itemPule.SubItems.Add(puleBuscado.Apostador.Nome);
                             totalApostador++;
                             ListViewItem item = new ListViewItem(puleBuscado.Apostador.Contato);
                             item.SubItems.Add(puleBuscado.Apostador.Nome);
                             item.SubItems.Add(puleBuscado.Número.ToString());
                             listViewApostadores.Items.Add(item);
                             totalApostado += puleBuscado.Valor;
                         }
                         listViewPulesAnimal.Items.Add(itemPule);

                     }
                 }
                 labelTotalApostadores.Text = $"Total De Apostadores {totalApostador}";
                 labelTotalApostadoAnimal.Text = $"Total Apostado {totalApostado}";
             }
         }*/

        /* private void LimparCaposAnimalTab()
         {
             listViewPulesAnimal.Items.Clear();
             listViewApostadores.Items.Clear();
         }
        */
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
            //var disputaSelecionadoDb = MainController.BuscarDisputa(disputaSelecionadaUi);
            var disputaSelecionadoDb = _disputaController.BuscarDisputa(disputaSelecionadaUi);
            if (disputaSelecionadoDb is not null)
            {
                labelDisputaNome.Text = "Disputa:" + disputaSelecionadoDb.Nome;
                foreach (var resultado in disputaSelecionadoDb.ResultadoList)
                {
                    dataGridViewDisputas.Rows.Add(resultado.Animal.Nome, resultado.Posição, resultado.Tempo);
                }
            }
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
            var disputaSelecionado = comboBoxDisputas.SelectedItem;
            if (disputaSelecionado is not null)

            {
                //DisputaController.LoadDisputa(disputaSelecionado);
                _disputaController.LoadDisputa(disputaSelecionado);
                var disputa = _disputaController.Disputa;
                if (disputa is not null)
                {
                    disputa.ajustarPosiçãoDosAnimais();
                    disputa.Atualizar();
                    dataGridViewDisputas.Rows.Clear();
                    SetDataGridDisputa(disputa);

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
