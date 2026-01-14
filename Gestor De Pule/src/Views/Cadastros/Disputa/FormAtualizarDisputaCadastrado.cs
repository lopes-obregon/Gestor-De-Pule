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

        public FormAtualizarDisputaCadastrado()
        {
            InitializeComponent();
            InitComboBoxs();
            // InitDisputa();
        }

        private void InitDisputa()
        {
            string mensagem = DisputaController.LoadDisputa(itemSelecionadoUi);
            MessageBox.Show(mensagem);
            var disputa = DisputaController.Disputa;
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

        public FormAtualizarDisputaCadastrado(object itemSelecionadoUi)
        {
            this.itemSelecionadoUi = itemSelecionadoUi;
            InitializeComponent();
            InitComboBoxs();
            InitDisputa();
        }

        private void InitComboBoxs()
        {
            DisputaController.LoadLists();
            var animais = DisputaController.Animals;
            if (animais != null && animais.Count > 0)
                comboBoxAnimaisCadastrados.Items.AddRange(animais.ToArray());

        }

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

        private void RemoverAnimalSelecionado(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list != null)
            {
                if (list.SelectedItem != null)
                {
                    var animalSelecionadoUi = list.SelectedItem;
                    listBoxAnimaisToDisputa.Items.Remove(animalSelecionadoUi);
                    animalSelecionadoUi = DisputaController.ToAnimal(animalSelecionadoUi);
                    if(animalSelecionadoUi != null)
                        DisputaController.AddAnimalRemovido(animalSelecionadoUi);
                }
            }
        }

        private void AtualizarDados(object sender, EventArgs e)
        {
            string nomeDisputa = String.Empty;
            DateTime ? date = null;
            string mensagem = String.Empty;
            if (String.IsNullOrEmpty(textBoxNomeDaDisputa.Text)) MessageBox.Show("Por vafor Insira um nome para disputa!");
            else { date = dateTimePicker1.Value;
                nomeDisputa = textBoxNomeDaDisputa.Text;
                mensagem = DisputaController.AtualizarDados(nomeDisputa, date, listBoxAnimaisToDisputa.Items);
                MessageBox.Show(mensagem);        
            }
        }
    }
}
