using Gestor_De_Pule.src.Controllers;

namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormAtualizarPule : Form
    {
        private PuleController controller { get; set; } = null;
        public FormAtualizarPule(object puleSelecionadoUi)
        {
            InitializeComponent();
            controller = new PuleController(puleSelecionadoUi);
            
            InitCampos();
        }

        private void InitCampos()
        {
            //PuleController.LoadPule(puleSelecionadoUi);
            SetComboBox();
            //var pule = PuleController.Pule;
            var pule = controller.PuleLocal;
            if (pule != null)
            {
                comboBoxApostadores.SelectedItem = pule.Apostador;
                comboBoxPagamento.SelectedItem = pule.StatusPagamento;
                listBoxAnimaisSelecionados.Items.AddRange(pule.Animais.ToArray());
                if (pule.StatusPagamento == Model.StatusPagamento.Pago)
                {
                    comboBoxAnimais.Enabled = false;
                    listBoxAnimaisSelecionados.Enabled = false;

                }
            }
        }
        private void SetComboBox()
        {

            //PuleController.LoadLists();
            controller.LoadListsLocal();
            //var animaisCadastrados = PuleController.Animals;
            var animaisCadastrados = controller.AnimalsLocal;
            //var ApostadoresCadastrados = PuleController.Apostadors;
            var ApostadoresCadastrados = controller.ApostadorsLocal;
            if (ApostadoresCadastrados is not null)
                comboBoxApostadores.Items.AddRange(ApostadoresCadastrados.ToArray());
            comboBoxPagamento.DataSource = Enum.GetValues(typeof(Gestor_De_Pule.src.Model.StatusPagamento));
            if (animaisCadastrados is not null)
                comboBoxAnimais.Items.AddRange(animaisCadastrados.ToArray());
        }

        private void RemoveAnimalSelect(object sender, EventArgs e)
        {
            var animalSelecionado = listBoxAnimaisSelecionados.SelectedItem;
            if (animalSelecionado is not null)
            {
                DialogResult res = MessageBox.Show("Deseja Realmente Excluir ?", "Confirmação", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    listBoxAnimaisSelecionados.Items.Remove(animalSelecionado);
                }
            }
            
        }

        private void AddToListBoxAnimaisSelecionados(object sender, EventArgs e)
        {
            var animalSelecionado = comboBoxAnimais.SelectedItem;
            if (animalSelecionado is not null)
                listBoxAnimaisSelecionados.Items.Add(animalSelecionado);
        }

        private void CloseAtualizarForm(object sender, EventArgs e)
        {
            Close();
        }

        private void AtualizarPule(object sender, EventArgs e)
        {
           string mensagem = String.Empty;
            //mensagem = PuleController.UpdateData(comboBoxApostadores.SelectedItem, comboBoxPagamento.SelectedItem, listBoxAnimaisSelecionados.Items);
            mensagem = controller.UpdateData(comboBoxApostadores.SelectedItem, comboBoxPagamento.SelectedItem, listBoxAnimaisSelecionados.Items);
            MessageBox.Show(mensagem);
        }
    }
}
