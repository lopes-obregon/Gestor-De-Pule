namespace Gestor_De_Pule.src.Views.Cadastros.Disputa
{
    partial class FormCadastroDisputa
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
            label1 = new Label();
            textBoxNomeDaDisputa = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label3 = new Label();
            comboBoxAnimaisCadastrados = new ComboBox();
            label4 = new Label();
            listBoxAnimaisToDisputa = new ListBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            label5 = new Label();
            numericUpDownQuantidadeRodadas = new NumericUpDown();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantidadeRodadas).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.67101F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.32899F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBoxNomeDaDisputa, 1, 0);
            tableLayoutPanel1.Controls.Add(dateTimePicker1, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(278, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 0;
            label1.Text = "Nome Da Disputa";
            // 
            // textBoxNomeDaDisputa
            // 
            textBoxNomeDaDisputa.Location = new Point(121, 3);
            textBoxNomeDaDisputa.Name = "textBoxNomeDaDisputa";
            textBoxNomeDaDisputa.Size = new Size(100, 23);
            textBoxNomeDaDisputa.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(121, 53);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(127, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 3;
            label2.Text = "Data Da Disputa";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(comboBoxAnimaisCadastrados, 1, 0);
            tableLayoutPanel2.Controls.Add(label4, 1, 1);
            tableLayoutPanel2.Controls.Add(listBoxAnimaisToDisputa, 1, 2);
            tableLayoutPanel2.Location = new Point(12, 137);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 62.6666679F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 37.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 162F));
            tableLayoutPanel2.Size = new Size(438, 218);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(188, 15);
            label3.TabIndex = 2;
            label3.Text = "Selecione O Animal Para a Disputa";
            // 
            // comboBoxAnimaisCadastrados
            // 
            comboBoxAnimaisCadastrados.FormattingEnabled = true;
            comboBoxAnimaisCadastrados.Location = new Point(222, 3);
            comboBoxAnimaisCadastrados.Name = "comboBoxAnimaisCadastrados";
            comboBoxAnimaisCadastrados.Size = new Size(121, 23);
            comboBoxAnimaisCadastrados.TabIndex = 3;
            comboBoxAnimaisCadastrados.SelectedIndexChanged += AnimalToLisBoxAnimais;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(222, 35);
            label4.Name = "label4";
            label4.Size = new Size(119, 15);
            label4.TabIndex = 4;
            label4.Text = "Animais Para Disputa";
            // 
            // listBoxAnimaisToDisputa
            // 
            listBoxAnimaisToDisputa.FormattingEnabled = true;
            listBoxAnimaisToDisputa.Location = new Point(222, 58);
            listBoxAnimaisToDisputa.Name = "listBoxAnimaisToDisputa";
            listBoxAnimaisToDisputa.Size = new Size(213, 154);
            listBoxAnimaisToDisputa.TabIndex = 5;
            listBoxAnimaisToDisputa.DoubleClick += RemoveAnimal;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(button1, 0, 0);
            tableLayoutPanel3.Controls.Add(button2, 1, 0);
            tableLayoutPanel3.Location = new Point(133, 361);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(200, 100);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CadastrarDisputa;
            // 
            // button2
            // 
            button2.Location = new Point(103, 3);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += CancelarCadastro;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.67101F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.32899F));
            tableLayoutPanel4.Controls.Add(label5, 0, 0);
            tableLayoutPanel4.Controls.Add(numericUpDownQuantidadeRodadas, 1, 0);
            tableLayoutPanel4.Location = new Point(296, 12);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(255, 26);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 0;
            label5.Text = "Quantas Rodadas";
            // 
            // numericUpDownQuantidadeRodadas
            // 
            numericUpDownQuantidadeRodadas.Location = new Point(111, 3);
            numericUpDownQuantidadeRodadas.Name = "numericUpDownQuantidadeRodadas";
            numericUpDownQuantidadeRodadas.Size = new Size(120, 23);
            numericUpDownQuantidadeRodadas.TabIndex = 2;
            numericUpDownQuantidadeRodadas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // FormCadastroDisputa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 408);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Name = "FormCadastroDisputa";
            Text = "Cadastro Disputa";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownQuantidadeRodadas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox textBoxNomeDaDisputa;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label3;
        private ComboBox comboBoxAnimaisCadastrados;
        private Label label4;
        private ListBox listBoxAnimaisToDisputa;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label5;
        private NumericUpDown numericUpDownQuantidadeRodadas;
    }
}