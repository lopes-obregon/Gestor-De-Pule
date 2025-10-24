using Gestor_De_Pule.src.Controllers;

namespace Gestor_De_Pule.src.Views.Relatórios.Apostador

{
    public partial class FormRelatórioApostador : Form
    {
       
        public FormRelatórioApostador()
        {
            InitializeComponent();
            RelatórioApostadorController.LoadLists();
            var controller = RelatórioApostadorController.Apostadors;
            initComboBox(controller);
        }

        private void initComboBox(List<Model.Apostador>? controller)
        {
            comboBoxApostadores.Items.Clear();
            if (controller != null)
                comboBoxApostadores.Items.AddRange(controller.ToArray());
        }

        private void GerarRelatório(object sender, EventArgs e)
        {

        }
    }
}
