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
        private AnimalController _controller;
        public FormAnimalAtualizar(object animalSelecionado)
        {
            InitializeComponent();
            _controller = new AnimalController();
           //AnimalController.AnimalSelecionado(animalSelecionado);
            _controller.AnimalSelecionado(animalSelecionado);
            SetAnimalData();
        }

        private void SetAnimalData()
        {
            if (_controller.Animal is not null)
            {
                textBoxNúmero.Text = _controller.Animal.Número.ToString();
                textBoxNome.Text = _controller.Animal.Nome;
                textBoxProprietário.Text = _controller.Animal.Proprietário;
                textBoxTreinador.Text = _controller.Animal.Treinador;
                textBoxJockey.Text = _controller.Animal.Jockey;
                textBoxCidade.Text = _controller.Animal.Cidade;
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
            //mensagem = AnimalController.Atualizar(número,nome, proprietário, treinador, cidade, jockey);
            mensagem = _controller.Atualizar(número,nome, proprietário, treinador, cidade, jockey);
            MessageBox.Show(mensagem);
        }

        private void CloseForm(object sender, EventArgs e)
        {
            Close();
        }
    }
}
