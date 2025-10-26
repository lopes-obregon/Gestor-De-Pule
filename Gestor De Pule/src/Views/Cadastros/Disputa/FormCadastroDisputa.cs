using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gestor_De_Pule.src.Controllers;
namespace Gestor_De_Pule.src.Views.Cadastros.Disputa
{
    public partial class FormCadastroDisputa : Form
    {
        public FormCadastroDisputa()
        {
            InitializeComponent();
            InitComboBoxs();
        }

        private void InitComboBoxs()
        {
            DisputaCadastrosController.LoadLists();
            var animais = DisputaCadastrosController.Animals;
            if (animais != null && animais.Count > 0)
                comboBoxAnimaisCadastrados.Items.AddRange(animais.ToArray());

        }

        private void CancelarCadastro(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AnimalToLisBoxAnimais(object sender, EventArgs e)
        {
            ComboBox? combo = sender as ComboBox;

            if (combo != null)
                if (combo.SelectedItem != null)
                {
                    var animalSelecionadoUi = combo.SelectedItem;
                    listBoxAnimaisToDisputa.Items.Add(animalSelecionadoUi);

                }
        }

        private void RemoveAnimal(object sender, EventArgs e)
        {
            ListBox? list = sender as ListBox;
            if (list != null)
                if (list.SelectedItem != null)
                {
                    list.Items.Remove(list.SelectedItem);
                }
        }

        private void CadastrarDisputa(object sender, EventArgs e)
        {
            string nomeDisputa = String.Empty;
            DateTime? date = null;
            nomeDisputa = textBoxNomeDaDisputa.Text;
            string mensagem = String.Empty;
            date =  DateTime.Parse(dateTimePicker1.Text);
            if (String.IsNullOrEmpty(nomeDisputa))
            {
                MessageBox.Show("Precisa colocar um nome para a disputa!");
            }
            if(listBoxAnimaisToDisputa.Items.Count == 0)
            {
                MessageBox.Show("Precisa colocar Pelomenos um ou mais animais para essa disputa!");
            }
            else
            {
                mensagem = DisputaCadastrosController.Cadastrar(nomeDisputa, date, listBoxAnimaisToDisputa.Items);
            }
        }
    }
}
