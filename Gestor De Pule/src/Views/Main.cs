using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Views;
using Gestor_De_Pule.src.Views.Apostador;
using Gestor_De_Pule.src.Views.Cadastros;
using Gestor_De_Pule.src.Views.Pule;
using Gestor_De_Pule.src.Views.Relat�rios.Animal;
using Gestor_De_Pule.src.Views.Relat�rios.Apostador;
using Gestor_De_Pule.src.Views.Relat�rios.Pule;

namespace Gestor_De_Pule
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //MainController.LoadLists();
            //InitComboBox();
            //this.Dock = DockStyle.Fill;

        }

        private void InitComboBox()
        {
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

        private void GerarRelat�rio(object sender, EventArgs e)
        {

        }

        /* private void GerarRelat�rioAnimal(object sender, EventArgs e)
         {
             var animalSelecionadoUi = comboBoxAnimais.SelectedItem;
             MainController.AnimalSelecionado(animalSelecionadoUi);
             LimparCaposAnimalTab();

             var animal = MainController.Animal;
             if (animal != null)
             {
                 labelAnimalNome.Text = $"{animal.N�mero} - {animal.Nome}";
                 labelTotalPules.Text = $"Total De Pules {animal.Pules.Count}";
                 int totalApostador = 0;
                 float totalApostado = 0.0f;
                 foreach (var pule in animal.Pules)
                 {
                     var puleBuscado = MainController.SearchPule(pule);
                     if (puleBuscado is not null)
                     {
                         ListViewItem itemPule = new ListViewItem(puleBuscado.N�mero.ToString());
                         itemPule.SubItems.Add(puleBuscado.Valor.ToString());
                         if (puleBuscado.Apostador is not null)
                         {
                             itemPule.SubItems.Add(puleBuscado.Apostador.Nome);
                             totalApostador++;
                             ListViewItem item = new ListViewItem(puleBuscado.Apostador.Contato);
                             item.SubItems.Add(puleBuscado.Apostador.Nome);
                             item.SubItems.Add(puleBuscado.N�mero.ToString());
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
            var form = new FormRelat�rioApostador();
            form.ShowDialog();
        }

        private void AnimalForm(object sender, EventArgs e)
        {
            var form = new FormRelat�rioAnimal();
            form.ShowDialog();
        }



        private void CloseSystem(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowRelat�rioPule(object sender, EventArgs e)
        {
            var window = new WindowRelat�rioPule();
            window.ShowDialog();
        }

        private void DisputaCadastradosWindow(object sender, EventArgs e)
        {
            var window = new WindowCadastroDisputa();
            window.ShowDialog();
        }
    }
}
