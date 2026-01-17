using Gestor_De_Pule.src.Controllers;
using Gestor_De_Pule.src.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_De_Pule.src.Views.Apostador
{
    public partial class FormCadastro : Form
    {
        private ApostadorController _controller;
        public FormCadastro()
        {
            _controller = new ApostadorController();
            InitializeComponent();
        }

        private void Cadastrar(object sender, EventArgs e)
        {
            string nome = string.Empty;
            string contato = string.Empty;
            string mensagem = String.Empty;
            if (!String.IsNullOrEmpty(textBoxNome.Text))
                nome = textBoxNome.Text;
            if (!String.IsNullOrEmpty(textBoxContato.Text))
                contato = textBoxContato.Text;
            //mensagem = ApostadorController.SaveApostador(nome, contato);
            mensagem = _controller.SaveApostador(nome, contato);
            MessageBox.Show(mensagem);
            textBoxContato.Text = String.Empty;
            textBoxNome.Text = String.Empty;
        }

        private void Close(object sender, EventArgs e)
        {
            Close();
        }
    }
}
