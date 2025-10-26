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

namespace Gestor_De_Pule.src.Views
{
    public partial class FormAnimalAtualizar : Form
    {
        public FormAnimalAtualizar(object animalSelecionado)
        {
            InitializeComponent();
            AnimalController.AnimalSelecionado(animalSelecionado);
            SetAnimalData();
        }

        private void SetAnimalData()
        {
            if (AnimalController.Animal is not null)
            {
                textBoxNúmero.Text = AnimalController.Animal.Número.ToString();
                textBoxNome.Text = AnimalController.Animal.Nome;
                textBoxProprietário.Text = AnimalController.Animal.Proprietário;
                textBoxTreinador.Text = AnimalController.Animal.Treinador;
                textBoxJockey.Text = AnimalController.Animal.Jockey;
                textBoxCidade.Text = AnimalController.Animal.Cidade;
            }
        }

        private void Atualizar(object sender, EventArgs e)
        {
            string mensagem = "";
            int número = int.Parse(textBoxNúmero.Text);
            string nome = textBoxNome.Text;
            string proprietário = textBoxProprietário.Text;
            string treinador = textBoxTreinador.Text;
            string cidade = textBoxCidade.Text;
            string jockey = textBoxJockey.Text;
            mensagem = AnimalController.Atualizar(número,nome, proprietário, treinador, cidade, jockey);
            MessageBox.Show(mensagem);
        }

        private void CloseForm(object sender, EventArgs e)
        {
            Close();
        }
    }
}
