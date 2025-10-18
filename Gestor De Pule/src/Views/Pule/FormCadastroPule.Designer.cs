namespace Gestor_De_Pule.src.Views.Pule
{
    partial class FormCadastroPule
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            comboBoxApostadores = new ComboBox();
            comboBoxPagamento = new ComboBox();
            comboBoxAnimais = new ComboBox();
            listBoxAnimaisSelecionados = new ListBox();
            label4 = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(listBoxAnimaisSelecionados, 1, 3);
            tableLayoutPanel1.Controls.Add(comboBoxAnimais, 1, 2);
            tableLayoutPanel1.Controls.Add(comboBoxApostadores, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(comboBoxPagamento, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 30);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanel1.Size = new Size(270, 255);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 2;
            label2.Text = "Pagamento";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 100);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 3;
            label3.Text = "Animais";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 4;
            label1.Text = "Apostador";
            // 
            // comboBoxApostadores
            // 
            comboBoxApostadores.FormattingEnabled = true;
            comboBoxApostadores.Location = new Point(138, 3);
            comboBoxApostadores.Name = "comboBoxApostadores";
            comboBoxApostadores.Size = new Size(121, 23);
            comboBoxApostadores.TabIndex = 1;
            // 
            // comboBoxPagamento
            // 
            comboBoxPagamento.FormattingEnabled = true;
            comboBoxPagamento.Location = new Point(138, 53);
            comboBoxPagamento.Name = "comboBoxPagamento";
            comboBoxPagamento.Size = new Size(121, 23);
            comboBoxPagamento.TabIndex = 1;
            // 
            // comboBoxAnimais
            // 
            comboBoxAnimais.FormattingEnabled = true;
            comboBoxAnimais.Location = new Point(138, 103);
            comboBoxAnimais.Name = "comboBoxAnimais";
            comboBoxAnimais.Size = new Size(121, 23);
            comboBoxAnimais.TabIndex = 1;
            // 
            // listBoxAnimaisSelecionados
            // 
            listBoxAnimaisSelecionados.FormattingEnabled = true;
            listBoxAnimaisSelecionados.Location = new Point(138, 153);
            listBoxAnimaisSelecionados.Name = "listBoxAnimaisSelecionados";
            listBoxAnimaisSelecionados.Size = new Size(120, 94);
            listBoxAnimaisSelecionados.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 150);
            label4.Name = "label4";
            label4.Size = new Size(122, 15);
            label4.TabIndex = 1;
            label4.Text = "Animais Selecionados";
            // 
            // FormCadastroPule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "FormCadastroPule";
            Text = "FormCadastroPule";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comboBoxPagamento;
        private ComboBox comboBoxApostadores;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private ListBox listBoxAnimaisSelecionados;
        private ComboBox comboBoxAnimais;
    }
}