using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
using System.Windows.Forms.Integration;
namespace Gestor_De_Pule.src.Views.Relatórios.Animal
{
    public partial class FormRelatórioAnimal : Form
    {
        public FormRelatórioAnimal()
        {
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            comboBoxAnimais.Items.Clear();
            RelatórioAnimalController.LoadLists();
            var animais = RelatórioAnimalController.Animals;
            if (animais != null)
            {
                comboBoxAnimais.Items.AddRange(animais.ToArray());
            }
        }

        private void GerarRelatório(object sender, EventArgs e)
        {
            var animalSelecionadoUi = comboBoxAnimais.SelectedItem;
            RelatórioAnimalController.AnimalSelecionado(animalSelecionadoUi);


            var animal = RelatórioAnimalController.Animal;
            if (animal != null)
            {
                listViewApostadores.Items.Clear();
                listViewPulesAnimal.Items.Clear();
                labelAnimalNome.Text = $"{animal.Número} - {animal.Nome}";
                labelTotalPules.Text = $"Total De Pules {animal.Pules.Count}";
                int totalApostador = 0;
                float totalApostado = 0.0f;
                foreach (var pule in animal.Pules)
                {
                    var puleBuscado = RelatórioAnimalController.SearchPule(pule);
                    if (puleBuscado is not null)
                    {
                        if (puleBuscado.Apostador is not null)
                        {
                            ListViewItem itemPule = new ListViewItem(puleBuscado.Número.ToString());
                            itemPule.SubItems.Add(puleBuscado.Apostador.Nome);
                            itemPule.SubItems.Add(puleBuscado.Valor.ToString("C"));
                            totalApostador++;
                            ListViewItem item = new ListViewItem(puleBuscado.Apostador.Contato);
                            item.SubItems.Add(puleBuscado.Apostador.Nome);
                            item.SubItems.Add(puleBuscado.Número.ToString());
                            listViewApostadores.Items.Add(item);
                            totalApostado += puleBuscado.Valor;
                            listViewPulesAnimal.Items.Add(itemPule);
                        }

                    }
                }
                labelTotalApostadores.Text = $"Total De Apostadores {totalApostador}";
                labelTotalApostadoAnimal.Text = $"Total Apostado {totalApostado.ToString("C")}";
            }
        }

        private void ImprimirRelatórioAnimal(object sender, EventArgs e)
        {
            string totalApostado = labelTotalApostadores.Text;
            string totalApostadoAnimal = labelTotalApostadoAnimal.Text;
            if (String.IsNullOrEmpty(totalApostado))
            {
                MessageBox.Show("Por Favor Gere o Relatório para imprir!");
            }
           
            else if (String.IsNullOrEmpty(totalApostadoAnimal))
            {
                MessageBox.Show("Por Favor Gere o Relatório para imprir!");
            }
            else
            {
                var animalSelecionadoUi = comboBoxAnimais.SelectedItem;
                if(animalSelecionadoUi != null)
                {
                    RelatórioAnimalController.LoadAnimal(animalSelecionadoUi);
                    
                }
                var animal = RelatórioAnimalController.Animal;
                RelatórioAnimalController.LoadLists();
                var pules = RelatórioAnimalController.Pules;
                PrintService.PrintAnimal(animal, pules);
            }
        }
    }
}
