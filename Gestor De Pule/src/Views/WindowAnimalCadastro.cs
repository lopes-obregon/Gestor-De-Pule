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
    public partial class WindowAnimalCadastro : Form
    {
        public WindowAnimalCadastro()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SalvarDados(object sender, EventArgs e)
        {
            int número = 0;
            string nome, proprietário, treinador, jockey, cidade, mensagem;
            mensagem = nome = proprietário = treinador = jockey = cidade = "";
            if (!String.IsNullOrEmpty(textBoxNúmero.Text))
            {
                número = int.Parse(textBoxNúmero.Text.Trim());
            }
            if (!String.IsNullOrEmpty(textBoxNome.Text))
                nome = textBoxNome.Text;
            if (!String.IsNullOrEmpty(textBoxProprietário.Text))
                proprietário = textBoxProprietário.Text;
            if (!String.IsNullOrEmpty(textBoxTreinador.Text))
                treinador = textBoxTreinador.Text;
            if (!String.IsNullOrEmpty(textBoxJockey.Text))
                jockey = textBoxJockey.Text;
            if (!String.IsNullOrEmpty(textBoxCidade.Text))
                cidade = textBoxCidade.Text;


            mensagem = AnimalController.Salvar(número, nome, proprietário, treinador, jockey, cidade);
            MessageBox.Show(mensagem);
            textBoxNúmero.Text = textBoxNome.Text = textBoxProprietário.Text = textBoxTreinador.Text = textBoxJockey.Text = textBoxCidade.Text = "";
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
