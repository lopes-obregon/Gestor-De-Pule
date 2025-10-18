namespace Gestor_De_Pule.src.Views
{
    partial class FormAnimalAtualizar
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonAtualizar = new Button();
            buttonCancelar = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            textBoxCidade = new TextBox();
            textBoxJockey = new TextBox();
            label6 = new Label();
            textBoxNúmero = new TextBox();
            label5 = new Label();
            textBoxNome = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            textBoxProprietário = new TextBox();
            label4 = new Label();
            textBoxTreinador = new TextBox();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(buttonAtualizar);
            flowLayoutPanel1.Controls.Add(buttonCancelar);
            flowLayoutPanel1.Location = new Point(15, 219);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(200, 30);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // buttonAtualizar
            // 
            buttonAtualizar.Location = new Point(3, 3);
            buttonAtualizar.Name = "buttonAtualizar";
            buttonAtualizar.Size = new Size(75, 23);
            buttonAtualizar.TabIndex = 7;
            buttonAtualizar.Text = "Atualizar";
            buttonAtualizar.UseVisualStyleBackColor = true;
            buttonAtualizar.Click += Atualizar;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Location = new Point(84, 3);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(75, 23);
            buttonCancelar.TabIndex = 7;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += CloseForm;
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
            tableLayoutPanel1.Location = new Point(12, 34);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(238, 179);
            tableLayoutPanel1.TabIndex = 7;
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
            // textBoxNúmero
            // 
            textBoxNúmero.Location = new Point(78, 32);
            textBoxNúmero.Name = "textBoxNúmero";
            textBoxNúmero.Size = new Size(100, 23);
            textBoxNúmero.TabIndex = 2;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 58);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 4;
            label3.Text = "Proprietário";
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
            // FormAnimalAtualizar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(269, 254);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "FormAnimalAtualizar";
            Text = "Atualizar";
            flowLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button buttonAtualizar;
        private Button buttonCancelar;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBoxCidade;
        private TextBox textBoxJockey;
        private Label label6;
        private TextBox textBoxNúmero;
        private Label label5;
        private TextBox textBoxNome;
        private Label label2;
        private Label label1;
        private Label label3;
        private TextBox textBoxProprietário;
        private Label label4;
        private TextBox textBoxTreinador;
    }
}