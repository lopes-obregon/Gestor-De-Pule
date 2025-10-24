using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Views;
using Gestor_De_Pule.src.Views.Apostador;
using Gestor_De_Pule.src.Views.Pule;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            MainController.LoadLists();
            InitComboBox();
            //this.Dock = DockStyle.Fill;

        }

        private void InitComboBox()
        {
            comboBoxApostadores.Items.Clear();
            comboBoxAnimais.Items.Clear();
            if (MainController.Apostadors.Count > 0)
            {
                comboBoxApostadores.Items.AddRange(MainController.Apostadors.ToArray());
            }
            if (MainController.Animals.Count > 0)
            {
                comboBoxAnimais.Items.AddRange(MainController.Animals.ToArray());
            }

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
            dataGridViewPules.Rows.Clear();
            labelValorTotalApostado.Text = "0";
            labelTotalDePules.Text = "0";
            var apostadorSelecionadoUi = comboBoxApostadores.SelectedItem;
            MainController.LoadApostador(apostadorSelecionadoUi);
            MainController.LoadPuLesDoApostador();
            float valorTotalApostado = 0.0f;
            if (MainController.Apostador is not null)
            {
                labelApostador.Text = "APOSTADOR: " + MainController.Apostador.Nome;
                if (MainController.Pules is not null && MainController.Pules.Count > 0)
                {
                    foreach (var pule in MainController.Pules)
                    {
                        dataGridViewPules.Rows.Add(pule.Número, pule.Date.ToShortDateString(), pule.AnimaisToString(), pule.Valor, pule.StatusPagamento);
                        valorTotalApostado += pule.Valor;
                    }
                    labelTotalDePules.Text = MainController.Pules.Count.ToString();
                    labelValorTotalApostado.Text = valorTotalApostado.ToString();
                }

            }
        }

        private void GerarRelatórioAnimal(object sender, EventArgs e)
        {
            var animalSelecionadoUi = comboBoxAnimais.SelectedItem;
            MainController.AnimalSelecionado(animalSelecionadoUi);
            LimparCaposAnimalTab();
            
            var animal = MainController.Animal;
            if (animal != null)
            {
                labelAnimalNome.Text = $"{animal.Número} - {animal.Nome}";
                labelTotalPules.Text =$"Total De Pules {animal.Pules.Count}";
                int totalApostador = 0;
                float totalApostado = 0.0f;
                foreach(var pule in animal.Pules)
                {
                   var puleBuscado = MainController.SearchPule(pule);
                    if(puleBuscado is not null)
                    {
                        ListViewItem itemPule = new ListViewItem(puleBuscado.Número.ToString());
                        itemPule.SubItems.Add(puleBuscado.Valor.ToString());
                       if(puleBuscado.Apostador is not null)
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
        }

        private void LimparCaposAnimalTab()
        {
            listViewPulesAnimal.Items.Clear();
            listViewApostadores.Items.Clear();
        }

        private void listViewAnimais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
