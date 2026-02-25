namespace Gestor_De_Pule.src.Views.Pule
{
    partial class FormPule
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
            numericUpDownNRodada = new NumericUpDown();
            comboBoxPagamento = new ComboBox();
            numericUpDownValorPule = new NumericUpDown();
            label5 = new Label();
            label2 = new Label();
            label8 = new Label();
            numericUpDownNúmeroPule = new NumericUpDown();
            comboBoxApostadores = new ComboBox();
            label1 = new Label();
            label6 = new Label();
            label7 = new Label();
            comboBoxDisputas = new ComboBox();
            label3 = new Label();
            comboBoxAnimais = new ComboBox();
            label4 = new Label();
            listBoxAnimaisSelecionados = new ListBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNRodada).BeginInit();
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
            tableLayoutPanel1.Controls.Add(numericUpDownNRodada, 1, 4);
            tableLayoutPanel1.Controls.Add(comboBoxPagamento, 1, 7);
            tableLayoutPanel1.Controls.Add(numericUpDownValorPule, 1, 5);
            tableLayoutPanel1.Controls.Add(label5, 0, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 7);
            tableLayoutPanel1.Controls.Add(label8, 0, 4);
            tableLayoutPanel1.Controls.Add(numericUpDownNúmeroPule, 1, 0);
            tableLayoutPanel1.Controls.Add(comboBoxApostadores, 1, 6);
            tableLayoutPanel1.Controls.Add(label1, 0, 6);
            tableLayoutPanel1.Controls.Add(label6, 0, 0);
            tableLayoutPanel1.Controls.Add(label7, 0, 1);
            tableLayoutPanel1.Controls.Add(comboBoxDisputas, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(comboBoxAnimais, 1, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(listBoxAnimaisSelecionados, 1, 3);
            tableLayoutPanel1.Location = new Point(12, 30);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 97F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 11F));
            tableLayoutPanel1.Size = new Size(270, 424);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // numericUpDownNRodada
            // 
            numericUpDownNRodada.Location = new Point(138, 220);
            numericUpDownNRodada.Name = "numericUpDownNRodada";
            numericUpDownNRodada.Size = new Size(120, 23);
            numericUpDownNRodada.TabIndex = 2;
            numericUpDownNRodada.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownNRodada.ValueChanged += ValueChange;
            // 
            // comboBoxPagamento
            // 
            comboBoxPagamento.FormattingEnabled = true;
            comboBoxPagamento.Location = new Point(138, 336);
            comboBoxPagamento.Name = "comboBoxPagamento";
            comboBoxPagamento.Size = new Size(121, 23);
            comboBoxPagamento.TabIndex = 1;
            // 
            // numericUpDownValorPule
            // 
            numericUpDownValorPule.DecimalPlaces = 2;
            numericUpDownValorPule.Location = new Point(138, 268);
            numericUpDownValorPule.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericUpDownValorPule.Name = "numericUpDownValorPule";
            numericUpDownValorPule.Size = new Size(120, 23);
            numericUpDownValorPule.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 265);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 3;
            label5.Text = "Valor Do Pule R$";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 333);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 2;
            label2.Text = "Pagamento";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 217);
            label8.Name = "label8";
            label8.Size = new Size(47, 15);
            label8.TabIndex = 5;
            label8.Text = "Rodada";
            label8.Click += label8_Click;
            // 
            // numericUpDownNúmeroPule
            // 
            numericUpDownNúmeroPule.Location = new Point(138, 3);
            numericUpDownNúmeroPule.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericUpDownNúmeroPule.Name = "numericUpDownNúmeroPule";
            numericUpDownNúmeroPule.Size = new Size(120, 23);
            numericUpDownNúmeroPule.TabIndex = 6;
            // 
            // comboBoxApostadores
            // 
            comboBoxApostadores.FormattingEnabled = true;
            comboBoxApostadores.Location = new Point(138, 300);
            comboBoxApostadores.Name = "comboBoxApostadores";
            comboBoxApostadores.Size = new Size(121, 23);
            comboBoxApostadores.TabIndex = 1;
            comboBoxApostadores.SelectedIndexChanged += SetApostador;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 297);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 4;
            label1.Text = "Apostador";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 5;
            label6.Text = "N º Pule";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 50);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 7;
            label7.Text = "Disputa";
            // 
            // comboBoxDisputas
            // 
            comboBoxDisputas.FormattingEnabled = true;
            comboBoxDisputas.Location = new Point(138, 53);
            comboBoxDisputas.Name = "comboBoxDisputas";
            comboBoxDisputas.Size = new Size(121, 23);
            comboBoxDisputas.TabIndex = 8;
            comboBoxDisputas.SelectedIndexChanged += comboBoxDisputa;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 82);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 3;
            label3.Text = "Animais";
            // 
            // comboBoxAnimais
            // 
            comboBoxAnimais.FormattingEnabled = true;
            comboBoxAnimais.Location = new Point(138, 85);
            comboBoxAnimais.Name = "comboBoxAnimais";
            comboBoxAnimais.Size = new Size(121, 23);
            comboBoxAnimais.TabIndex = 1;
            comboBoxAnimais.SelectedIndexChanged += AnimalSelecionadoUi;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 120);
            label4.Name = "label4";
            label4.Size = new Size(112, 15);
            label4.TabIndex = 1;
            label4.Text = "Animal Selecionado";
            // 
            // listBoxAnimaisSelecionados
            // 
            listBoxAnimaisSelecionados.FormattingEnabled = true;
            listBoxAnimaisSelecionados.Location = new Point(138, 123);
            listBoxAnimaisSelecionados.Name = "listBoxAnimaisSelecionados";
            listBoxAnimaisSelecionados.Size = new Size(120, 79);
            listBoxAnimaisSelecionados.TabIndex = 1;
            listBoxAnimaisSelecionados.DoubleClick += RemoveAnimalSelecionado;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Location = new Point(12, 474);
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
            // FormPule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(287, 607);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Name = "FormPule";
            Text = "Cadastro Do Pule";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNRodada).EndInit();
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
        private Label label7;
        private ComboBox comboBoxDisputas;
        private Label label8;
        private NumericUpDown numericUpDownNRodada;
    }
}