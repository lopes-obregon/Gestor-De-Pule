using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Service;
namespace Gestor_De_Pule.src.Views.Relatórios.Animal
{
    public partial class FormRelatórioAnimal : Form
    {
        private AnimalController _animalController;
        public FormRelatórioAnimal()
        {
            _animalController = new AnimalController();
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            comboBoxAnimais.Items.Clear();
            //RelatórioAnimalController.LoadLists();
            _animalController.LoadAnimais();
            var animais = _animalController.Animals;
            if (animais != null)
            {
                //biding
                //comboBoxAnimais.Items.AddRange(animais.ToArray());
                comboBoxAnimais.DataSource = animais;
                comboBoxAnimais.DisplayMember = "Nome";
                comboBoxAnimais.ValueMember = "Id";
            }
        }

        private void GerarRelatório(object sender, EventArgs e)
        {
            int animalId = (int)comboBoxAnimais.SelectedValue;
            _animalController.AnimalSelecionado(animalId);
            _animalController.LoadPules(animalId);

            var animal = _animalController.Animal;
            if (!_animalController.IsNull())
            {
                listViewApostadores.Items.Clear();
                listViewPulesAnimal.Items.Clear();
                labelAnimalNome.Text = _animalController.GetAnimalNúmeroNome();
                labelTotalPules.Text = $"Total De Pules {_animalController.TotalPules().ToString()}";
                int totalApostador = 0;
                decimal totalApostado = 0.0m;
                foreach (var pule in animal.Pules)
                {
                    var puleBuscado = PuleController.ToPule(_animalController.SearchPule(pule));
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
                    _animalController.LoadAnimal(animalSelecionadoUi);
                    //RelatórioAnimalController.LoadAnimal(animalSelecionadoUi);
                    
                }
                var animal = _animalController.Animal;
                _animalController.LoadLists();
                //RelatórioAnimalController.LoadLists();
                var pules = _animalController._puleController.Pules;
                PrintService.PrintAnimal(animal, pules);
            }
        }
    }
}
