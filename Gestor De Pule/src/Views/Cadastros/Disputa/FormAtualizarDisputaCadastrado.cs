using Gestor_De_Pule.src.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_De_Pule.src.Views.Cadastros.Disputa
{
   
    public partial class FormAtualizarDisputaCadastrado : Form
    {
        private object itemSelecionadoUi = new();
        private DisputaController _disputaController = new DisputaController();

        public FormAtualizarDisputaCadastrado()
        {
            InitializeComponent();
            InitComboBoxs();
            // InitDisputa();
        }
        /// <summary>
        /// Initializes the dispute UI by loading dispute data, displaying a message, and updating relevant controls
        /// with dispute information.
        /// </summary>
        private void InitDisputa()
        {
            //string mensagem = DisputaController.LoadDisputa(itemSelecionadoUi);
            string mensagem = _disputaController.LoadDisputa(itemSelecionadoUi);
            MessageBox.Show(mensagem);
            var disputa = _disputaController.Disputa;
            if (disputa != null)
            {
                textBoxNomeDaDisputa.Text = disputa.Nome;
                if (disputa.DataEHora > dateTimePicker1.MinDate && disputa.DataEHora < dateTimePicker1.MaxDate)
                {
                    dateTimePicker1.Value = disputa.DataEHora;
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now;
                }
                listBoxAnimaisToDisputa.Items.Clear();
                var resultadoList = disputa.ResultadoList.ToList();
                if (resultadoList is not null && resultadoList.Count > 0)
                {
                    foreach (var item in resultadoList)
                    {
                        if (item != null) listBoxAnimaisToDisputa.Items.Add(item.Animal);
                    }
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the FormAtualizarDisputaCadastrado class with the specified selected item and
        /// sets up the form components.
        /// </summary>
        /// <param name="itemSelecionadoUi">The selected item from the UI to be used for updating the dispute.</param>
        public FormAtualizarDisputaCadastrado(object itemSelecionadoUi)
        {
            this.itemSelecionadoUi = itemSelecionadoUi;
            InitializeComponent();
            InitComboBoxs();
            InitDisputa();
        }
        /// <summary>
        /// Initializes the animal combo box with the list of animals loaded from the controller.
        /// </summary>
        private void InitComboBoxs()
        {
            _disputaController.LoadLists();
            var animais = _disputaController.Animals;
            if (animais != null && animais.Count > 0)
                comboBoxAnimaisCadastrados.Items.AddRange(animais.ToArray());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimalToListBox(object sender, EventArgs e)
        {
            ComboBox? combo = sender as ComboBox;
            if (combo != null)
            {
                if (combo.SelectedItem != null)
                {
                    var animalSelecionadoUi = combo.SelectedItem;
                    listBoxAnimaisToDisputa.Items.Add(animalSelecionadoUi);
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoverAnimalSelecionado(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                if (list.SelectedItem != null)
                {
                    var animalSelecionadoUi = list.SelectedItem;
                    listBoxAnimaisToDisputa.Items.Remove(animalSelecionadoUi);
                    animalSelecionadoUi = _disputaController.ToAnimal(animalSelecionadoUi);
                    if(animalSelecionadoUi != null)
                        _disputaController.AddAnimalRemovido(animalSelecionadoUi);
                }
            }
        }
       /// <summary>
       /// Atualiza os dados da disputa;
       /// </summary>
        private void AtualizarDados()
        {
            string nomeDisputa = String.Empty;
            DateTime ? date = null;
            string mensagem = String.Empty;
            if (String.IsNullOrEmpty(textBoxNomeDaDisputa.Text)) MessageBox.Show("Por vafor Insira um nome para disputa!");
            else { date = dateTimePicker1.Value;
                nomeDisputa = textBoxNomeDaDisputa.Text;
                mensagem = _disputaController.AtualizarDados(nomeDisputa, date, listBoxAnimaisToDisputa.Items);
                MessageBox.Show(mensagem);        
            }
        }
    }
}
