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

namespace Gestor_De_Pule.src.Views.Pule
{
    public partial class FormAtualizarPule : Form
    {
        public FormAtualizarPule(object puleSelecionadoUi)
        {
            InitializeComponent();
            InitCampos(puleSelecionadoUi);
        }

        private void InitCampos(object puleSelecionadoUi)
        {
            PuleController.LoadPule(puleSelecionadoUi);
            SetComboBox();
            var pule = PuleController.Pule;
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

            PuleController.LoadLists();
            var animaisCadastrados = PuleController.Animals;
            var ApostadoresCadastrados = PuleController.Apostadors;
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
            mensagem = PuleController.UpdateData(comboBoxApostadores.SelectedItem, comboBoxPagamento.SelectedItem, listBoxAnimaisSelecionados.Items);
            MessageBox.Show(mensagem);
        }
    }
}
