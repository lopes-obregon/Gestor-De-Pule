namespace Gestor_De_Pule.src.Views
{
    partial class WindowAnimalCadastro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxNome = new TextBox();
            label2 = new Label();
            textBoxNúmero = new TextBox();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            textBoxCidade = new TextBox();
            textBoxJockey = new TextBox();
            label6 = new Label();
            label5 = new Label();
            textBoxProprietário = new TextBox();
            label4 = new Label();
            textBoxTreinador = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonSalvar = new Button();
            buttonCancelar = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 29);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Número";
            // 
            // textBoxNome
            // 
            textBoxNome.Location = new Point(78, 3);
            textBoxNome.Name = "textBoxNome";
            textBoxNome.Size = new Size(100, 23);
            textBoxNome.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Nome";
            label2.Click += label2_Click;
            // 
            // textBoxNúmero
            // 
            textBoxNúmero.Location = new Point(78, 32);
            textBoxNúmero.Name = "textBoxNúmero";
            textBoxNúmero.Size = new Size(100, 23);
            textBoxNúmero.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 58);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 4;
            label3.Text = "Proprietário";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(textBoxCidade, 1, 5);
            tableLayoutPanel1.Controls.Add(textBoxJockey, 1, 4);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(textBoxNúmero, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(textBoxNome, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(textBoxProprietário, 1, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(textBoxTreinador, 1, 3);
            tableLayoutPanel1.Location = new Point(12, 25);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(238, 179);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // textBoxCidade
            // 
            textBoxCidade.Location = new Point(78, 148);
            textBoxCidade.Name = "textBoxCidade";
            textBoxCidade.Size = new Size(100, 23);
            textBoxCidade.TabIndex = 6;
            // 
            // textBoxJockey
            // 
            textBoxJockey.Location = new Point(78, 119);
            textBoxJockey.Name = "textBoxJockey";
            textBoxJockey.Size = new Size(100, 23);
            textBoxJockey.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 145);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 7;
            label6.Text = "Cidade";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 116);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 6;
            label5.Text = "Jockey";
            // 
            // textBoxProprietário
            // 
            textBoxProprietário.Location = new Point(78, 61);
            textBoxProprietário.Name = "textBoxProprietário";
            textBoxProprietário.Size = new Size(100, 23);
            textBoxProprietário.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 87);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 6;
            label4.Text = "Treinador";
            // 
            // textBoxTreinador
            // 
            textBoxTreinador.Location = new Point(78, 90);
            textBoxTreinador.Name = "textBoxTreinador";
            textBoxTreinador.Size = new Size(100, 23);
            textBoxTreinador.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(buttonSalvar);
            flowLayoutPanel1.Controls.Add(buttonCancelar);
            flowLayoutPanel1.Location = new Point(15, 210);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(200, 30);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // buttonSalvar
            // 
            buttonSalvar.Location = new Point(3, 3);
            buttonSalvar.Name = "buttonSalvar";
            buttonSalvar.Size = new Size(75, 23);
            buttonSalvar.TabIndex = 7;
            buttonSalvar.Text = "Salvar";
            buttonSalvar.UseVisualStyleBackColor = true;
            buttonSalvar.Click += SalvarDados;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Location = new Point(84, 3);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(75, 23);
            buttonCancelar.TabIndex = 7;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += CloseWindow;
            // 
            // WindowAnimalCadastro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(278, 261);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "WindowAnimalCadastro";
            Text = "Cadastro";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private TextBox textBoxNome;
        private Label label2;
        private TextBox textBoxNúmero;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBoxProprietário;
        private TextBox textBoxCidade;
        private TextBox textBoxJockey;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox textBoxTreinador;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonSalvar;
        private Button buttonCancelar;
    }
}