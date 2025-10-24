namespace Gestor_De_Pule
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem = new ToolStripMenuItem();
            puleToolStripMenuItem = new ToolStripMenuItem();
            relatóriosToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem1 = new ToolStripMenuItem();
            tabControlApostador = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tableLayoutPanel5 = new TableLayoutPanel();
            label7 = new Label();
            labelTotalPules = new Label();
            labelTotalApostadores = new Label();
            labelTotalApostadoAnimal = new Label();
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
            tableLayoutPanel3 = new TableLayoutPanel();
            label5 = new Label();
            comboBoxAnimais = new ComboBox();
            button2 = new Button();
            menuStrip1.SuspendLayout();
            tabControlApostador.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem, relatóriosToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1081, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { animalToolStripMenuItem, apostadorToolStripMenuItem, puleToolStripMenuItem });
            arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            arquivoToolStripMenuItem.Size = new Size(71, 20);
            arquivoToolStripMenuItem.Text = "Cadastros";
            // 
            // animalToolStripMenuItem
            // 
            animalToolStripMenuItem.Name = "animalToolStripMenuItem";
            animalToolStripMenuItem.Size = new Size(129, 22);
            animalToolStripMenuItem.Text = "Animal";
            animalToolStripMenuItem.Click += JanelaCadastro;
            // 
            // apostadorToolStripMenuItem
            // 
            apostadorToolStripMenuItem.Name = "apostadorToolStripMenuItem";
            apostadorToolStripMenuItem.Size = new Size(129, 22);
            apostadorToolStripMenuItem.Text = "Apostador";
            apostadorToolStripMenuItem.Click += OpenWindowApostadoresCadastrados;
            // 
            // puleToolStripMenuItem
            // 
            puleToolStripMenuItem.Name = "puleToolStripMenuItem";
            puleToolStripMenuItem.Size = new Size(129, 22);
            puleToolStripMenuItem.Text = "Pule";
            puleToolStripMenuItem.Click += WindowPuleCadastrados;
            // 
            // relatóriosToolStripMenuItem
            // 
            relatóriosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { apostadorToolStripMenuItem1 });
            relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            relatóriosToolStripMenuItem.Size = new Size(71, 20);
            relatóriosToolStripMenuItem.Text = "Relatórios";
            // 
            // apostadorToolStripMenuItem1
            // 
            apostadorToolStripMenuItem1.Name = "apostadorToolStripMenuItem1";
            apostadorToolStripMenuItem1.Size = new Size(180, 22);
            apostadorToolStripMenuItem1.Text = "Apostador";
            apostadorToolStripMenuItem1.Click += ApostadorWindow;
            // 
            // tabControlApostador
            // 
            tabControlApostador.Controls.Add(tabPage1);
            tabControlApostador.Controls.Add(tabPage2);
            tabControlApostador.Dock = DockStyle.Fill;
            tabControlApostador.Location = new Point(0, 24);
            tabControlApostador.Name = "tabControlApostador";
            tabControlApostador.SelectedIndex = 0;
            tabControlApostador.Size = new Size(1081, 569);
            tabControlApostador.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1073, 541);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Apostador";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel5);
            tabPage2.Controls.Add(tableLayoutPanel4);
            tabPage2.Controls.Add(tableLayoutPanel3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1073, 541);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Animal";
            tabPage2.UseVisualStyleBackColor = true;
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
            tableLayoutPanel5.Location = new Point(6, 420);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 44.28571F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 55.7142868F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel5.Size = new Size(344, 100);
            tableLayoutPanel5.TabIndex = 2;
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
            tableLayoutPanel4.Location = new Point(3, 110);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 53.9682541F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 46.0317459F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(742, 308);
            tableLayoutPanel4.TabIndex = 1;
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
            label6.Location = new Point(3, 31);
            label6.Name = "label6";
            label6.Size = new Size(97, 21);
            label6.TabIndex = 2;
            label6.Text = "Apostadores";
            // 
            // listViewApostadores
            // 
            listViewApostadores.Columns.AddRange(new ColumnHeader[] { columnHeaderContato, columnHeaderNome, columnHeaderNPule });
            listViewApostadores.Dock = DockStyle.Fill;
            listViewApostadores.Location = new Point(3, 61);
            listViewApostadores.Name = "listViewApostadores";
            listViewApostadores.Size = new Size(381, 244);
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
            label8.Location = new Point(390, 31);
            label8.Name = "label8";
            label8.Size = new Size(47, 21);
            label8.TabIndex = 3;
            label8.Text = "Pules";
            // 
            // listViewPulesAnimal
            // 
            listViewPulesAnimal.Columns.AddRange(new ColumnHeader[] { columnHeaderNúmeroPule, columnHeaderApostador, columnHeaderValorApostado });
            listViewPulesAnimal.Dock = DockStyle.Fill;
            listViewPulesAnimal.Location = new Point(390, 61);
            listViewPulesAnimal.Name = "listViewPulesAnimal";
            listViewPulesAnimal.Size = new Size(349, 244);
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
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.2352943F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.7647057F));
            tableLayoutPanel3.Controls.Add(label5, 0, 0);
            tableLayoutPanel3.Controls.Add(comboBoxAnimais, 1, 0);
            tableLayoutPanel3.Controls.Add(button2, 1, 1);
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 42.1052628F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 57.8947372F));
            tableLayoutPanel3.Size = new Size(255, 107);
            tableLayoutPanel3.TabIndex = 0;
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
            button2.Click += GerarRelatórioAnimal;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1081, 593);
            Controls.Add(tabControlApostador);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Gestor Pule";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControlApostador.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem animalToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem;
        private ToolStripMenuItem puleToolStripMenuItem;
        private TabControl tabControlApostador;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label5;
        private ComboBox comboBoxAnimais;
        private TableLayoutPanel tableLayoutPanel4;
        private Button button2;
        private Label labelAnimalNome;
        private ListView listViewApostadores;
        private ColumnHeader columnHeaderNúmero;
        private ColumnHeader columnHeaderNome;
        private Label label6;
        private ColumnHeader columnHeaderContato;
        private ColumnHeader columnHeaderNPule;
        private TableLayoutPanel tableLayoutPanel5;
        private Label labelTotalApostadores;
        private Label label7;
        private Label labelTotalPules;
        private Label labelTotalApostadoAnimal;
        private Label label8;
        private ListView listViewPulesAnimal;
        private ColumnHeader columnHeaderNúmeroPule;
        private ColumnHeader columnHeaderApostador;
        private ColumnHeader columnHeaderValorApostado;
        private ToolStripMenuItem relatóriosToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem1;
    }
}
