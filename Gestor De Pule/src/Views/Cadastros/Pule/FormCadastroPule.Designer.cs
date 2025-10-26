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
            numericUpDownValorPule = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            listBoxAnimaisSelecionados = new ListBox();
            comboBoxAnimais = new ComboBox();
            comboBoxApostadores = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboBoxPagamento = new ComboBox();
            label6 = new Label();
            numericUpDownNúmeroPule = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValorPule).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNúmeroPule).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(numericUpDownValorPule, 1, 4);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(listBoxAnimaisSelecionados, 1, 3);
            tableLayoutPanel1.Controls.Add(comboBoxAnimais, 1, 2);
            tableLayoutPanel1.Controls.Add(comboBoxApostadores, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(comboBoxPagamento, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(numericUpDownNúmeroPule, 1, 5);
            tableLayoutPanel1.Location = new Point(12, 30);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel1.Size = new Size(270, 358);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // numericUpDownValorPule
            // 
            numericUpDownValorPule.DecimalPlaces = 2;
            numericUpDownValorPule.Location = new Point(138, 271);
            numericUpDownValorPule.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericUpDownValorPule.Name = "numericUpDownValorPule";
            numericUpDownValorPule.Size = new Size(120, 23);
            numericUpDownValorPule.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 268);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 3;
            label5.Text = "Valor Do Pule R$";
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
            // listBoxAnimaisSelecionados
            // 
            listBoxAnimaisSelecionados.FormattingEnabled = true;
            listBoxAnimaisSelecionados.Location = new Point(138, 153);
            listBoxAnimaisSelecionados.Name = "listBoxAnimaisSelecionados";
            listBoxAnimaisSelecionados.Size = new Size(120, 94);
            listBoxAnimaisSelecionados.TabIndex = 1;
            listBoxAnimaisSelecionados.DoubleClick += RemoveAnimalSelecionado;
            // 
            // comboBoxAnimais
            // 
            comboBoxAnimais.FormattingEnabled = true;
            comboBoxAnimais.Location = new Point(138, 103);
            comboBoxAnimais.Name = "comboBoxAnimais";
            comboBoxAnimais.Size = new Size(121, 23);
            comboBoxAnimais.TabIndex = 1;
            comboBoxAnimais.SelectedIndexChanged += AnimalSelecionadoUi;
            // 
            // comboBoxApostadores
            // 
            comboBoxApostadores.FormattingEnabled = true;
            comboBoxApostadores.Location = new Point(138, 3);
            comboBoxApostadores.Name = "comboBoxApostadores";
            comboBoxApostadores.Size = new Size(121, 23);
            comboBoxApostadores.TabIndex = 1;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 2;
            label2.Text = "Pagamento";
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
            // comboBoxPagamento
            // 
            comboBoxPagamento.FormattingEnabled = true;
            comboBoxPagamento.Location = new Point(138, 53);
            comboBoxPagamento.Name = "comboBoxPagamento";
            comboBoxPagamento.Size = new Size(121, 23);
            comboBoxPagamento.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 304);
            label6.Name = "label6";
            label6.Size = new Size(95, 15);
            label6.TabIndex = 5;
            label6.Text = "Número Do Pule";
            // 
            // numericUpDownNúmeroPule
            // 
            numericUpDownNúmeroPule.Location = new Point(138, 307);
            numericUpDownNúmeroPule.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericUpDownNúmeroPule.Name = "numericUpDownNúmeroPule";
            numericUpDownNúmeroPule.Size = new Size(120, 23);
            numericUpDownNúmeroPule.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Location = new Point(15, 408);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(200, 100);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SalvarPule;
            // 
            // button2
            // 
            button2.Location = new Point(84, 3);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += FecharCadastros;
            // 
            // FormCadastroPule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 520);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "FormCadastroPule";
            Text = "Cadastro Do Pule";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValorPule).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNúmeroPule).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
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
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
        private Button button2;
        private NumericUpDown numericUpDownValorPule;
        private Label label5;
        private Label label6;
        private NumericUpDown numericUpDownNúmeroPule;
    }
}