namespace Gestor_De_Pule.src.Views.Relatórios.Animal
{
    partial class FormRelatórioAnimal
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
            tableLayoutPanel3 = new TableLayoutPanel();
            label5 = new Label();
            comboBoxAnimais = new ComboBox();
            button2 = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            labelAnimalNome = new Label();
            label6 = new Label();
            listViewApostadores = new ListView();
            columnHeaderContato = new ColumnHeader();
            columnHeaderNome = new ColumnHeader();
            columnHeaderNPule = new ColumnHeader();
            label8 = new Label();
            listViewPulesAnimal = new ListView();
            columnHeaderNúmeroPule = new ColumnHeader();
            columnHeaderApostador = new ColumnHeader();
            columnHeaderValorApostado = new ColumnHeader();
            tableLayoutPanel5 = new TableLayoutPanel();
            label7 = new Label();
            labelTotalPules = new Label();
            labelTotalApostadores = new Label();
            labelTotalApostadoAnimal = new Label();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.2352943F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.7647057F));
            tableLayoutPanel3.Controls.Add(label5, 0, 0);
            tableLayoutPanel3.Controls.Add(comboBoxAnimais, 1, 0);
            tableLayoutPanel3.Controls.Add(button2, 1, 1);
            tableLayoutPanel3.Location = new Point(12, 12);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1052628F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 57.8947372F));
            tableLayoutPanel3.Size = new Size(255, 107);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 0);
            label5.Name = "label5";
            label5.Size = new Size(108, 15);
            label5.TabIndex = 0;
            label5.Text = "Selecione o Animal";
            // 
            // comboBoxAnimais
            // 
            comboBoxAnimais.FormattingEnabled = true;
            comboBoxAnimais.Location = new Point(126, 3);
            comboBoxAnimais.Name = "comboBoxAnimais";
            comboBoxAnimais.Size = new Size(121, 23);
            comboBoxAnimais.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(126, 48);
            button2.Name = "button2";
            button2.Size = new Size(121, 23);
            button2.TabIndex = 2;
            button2.Text = "Gerar Relatório";
            button2.UseVisualStyleBackColor = true;
            button2.Click += GerarRelatório;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.1563339F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.8436661F));
            tableLayoutPanel4.Controls.Add(labelAnimalNome, 0, 0);
            tableLayoutPanel4.Controls.Add(label6, 0, 1);
            tableLayoutPanel4.Controls.Add(listViewApostadores, 0, 2);
            tableLayoutPanel4.Controls.Add(label8, 1, 1);
            tableLayoutPanel4.Controls.Add(listViewPulesAnimal, 1, 2);
            tableLayoutPanel4.Location = new Point(12, 125);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 53.9682541F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 46.0317459F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(742, 308);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // labelAnimalNome
            // 
            labelAnimalNome.AutoSize = true;
            labelAnimalNome.Font = new Font("Segoe UI", 12F);
            labelAnimalNome.Location = new Point(3, 0);
            labelAnimalNome.Name = "labelAnimalNome";
            labelAnimalNome.Size = new Size(16, 21);
            labelAnimalNome.TabIndex = 0;
            labelAnimalNome.Text = "-";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(3, 29);
            label6.Name = "label6";
            label6.Size = new Size(97, 21);
            label6.TabIndex = 2;
            label6.Text = "Apostadores";
            // 
            // listViewApostadores
            // 
            listViewApostadores.Columns.AddRange(new ColumnHeader[] { columnHeaderContato, columnHeaderNome, columnHeaderNPule });
            listViewApostadores.Dock = DockStyle.Fill;
            listViewApostadores.Location = new Point(3, 57);
            listViewApostadores.Name = "listViewApostadores";
            listViewApostadores.Size = new Size(381, 248);
            listViewApostadores.TabIndex = 1;
            listViewApostadores.UseCompatibleStateImageBehavior = false;
            listViewApostadores.View = View.Details;
            // 
            // columnHeaderContato
            // 
            columnHeaderContato.Text = "Contato";
            // 
            // columnHeaderNome
            // 
            columnHeaderNome.Text = "Nome";
            // 
            // columnHeaderNPule
            // 
            columnHeaderNPule.Text = "Nº Do Pule";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(390, 29);
            label8.Name = "label8";
            label8.Size = new Size(47, 21);
            label8.TabIndex = 3;
            label8.Text = "Pules";
            // 
            // listViewPulesAnimal
            // 
            listViewPulesAnimal.Columns.AddRange(new ColumnHeader[] { columnHeaderNúmeroPule, columnHeaderApostador, columnHeaderValorApostado });
            listViewPulesAnimal.Dock = DockStyle.Fill;
            listViewPulesAnimal.Location = new Point(390, 57);
            listViewPulesAnimal.Name = "listViewPulesAnimal";
            listViewPulesAnimal.Size = new Size(349, 248);
            listViewPulesAnimal.TabIndex = 4;
            listViewPulesAnimal.UseCompatibleStateImageBehavior = false;
            listViewPulesAnimal.View = View.Details;
            // 
            // columnHeaderNúmeroPule
            // 
            columnHeaderNúmeroPule.Text = "Nº";
            // 
            // columnHeaderApostador
            // 
            columnHeaderApostador.Text = "Apostador";
            // 
            // columnHeaderValorApostado
            // 
            columnHeaderValorApostado.Text = "Valor Apostado R$";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 167F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Controls.Add(label7, 0, 0);
            tableLayoutPanel5.Controls.Add(labelTotalPules, 0, 1);
            tableLayoutPanel5.Controls.Add(labelTotalApostadores, 0, 2);
            tableLayoutPanel5.Controls.Add(labelTotalApostadoAnimal, 1, 1);
            tableLayoutPanel5.Location = new Point(12, 463);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 44.28571F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 55.7142868F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel5.Size = new Size(344, 100);
            tableLayoutPanel5.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(3, 0);
            label7.Name = "label7";
            label7.Size = new Size(67, 21);
            label7.TabIndex = 3;
            label7.Text = "Resumo";
            // 
            // labelTotalPules
            // 
            labelTotalPules.AutoSize = true;
            labelTotalPules.Location = new Point(3, 31);
            labelTotalPules.Name = "labelTotalPules";
            labelTotalPules.Size = new Size(84, 15);
            labelTotalPules.TabIndex = 4;
            labelTotalPules.Text = "Total De Pules ";
            // 
            // labelTotalApostadores
            // 
            labelTotalApostadores.AutoSize = true;
            labelTotalApostadores.Location = new Point(3, 70);
            labelTotalApostadores.Name = "labelTotalApostadores";
            labelTotalApostadores.Size = new Size(119, 15);
            labelTotalApostadores.TabIndex = 3;
            labelTotalApostadores.Text = "Total De Apostadores";
            // 
            // labelTotalApostadoAnimal
            // 
            labelTotalApostadoAnimal.AutoSize = true;
            labelTotalApostadoAnimal.Location = new Point(170, 31);
            labelTotalApostadoAnimal.Name = "labelTotalApostadoAnimal";
            labelTotalApostadoAnimal.Size = new Size(12, 15);
            labelTotalApostadoAnimal.TabIndex = 5;
            labelTotalApostadoAnimal.Text = "-";
            // 
            // FormRelatórioAnimal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 575);
            Controls.Add(tableLayoutPanel5);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Name = "FormRelatórioAnimal";
            Text = "FormRelatórioAnimal";
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private ComboBox comboBoxAnimais;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel4;
        private Label labelAnimalNome;
        private Label label6;
        private ListView listViewApostadores;
        private ColumnHeader columnHeaderContato;
        private ColumnHeader columnHeaderNome;
        private ColumnHeader columnHeaderNPule;
        private Label label8;
        private ListView listViewPulesAnimal;
        private ColumnHeader columnHeaderNúmeroPule;
        private ColumnHeader columnHeaderApostador;
        private ColumnHeader columnHeaderValorApostado;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label7;
        private Label labelTotalPules;
        private Label labelTotalApostadores;
        private Label labelTotalApostadoAnimal;
    }
}