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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem = new ToolStripMenuItem();
            puleToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            comboBoxApostadores = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            labelApostador = new Label();
            dataGridViewPules = new DataGridView();
            label2 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            labelValorTotalApostado = new Label();
            labelTotalDePules = new Label();
            label3 = new Label();
            label4 = new Label();
            NPule = new DataGridViewTextBoxColumn();
            Data = new DataGridViewTextBoxColumn();
            Animais = new DataGridViewTextBoxColumn();
            ValorRs = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPules).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.01877F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.98123F));
            tableLayoutPanel1.Controls.Add(comboBoxApostadores, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(button1, 1, 1);
            tableLayoutPanel1.Location = new Point(113, 39);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(373, 100);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // comboBoxApostadores
            // 
            comboBoxApostadores.FormattingEnabled = true;
            comboBoxApostadores.Location = new Point(156, 3);
            comboBoxApostadores.Name = "comboBoxApostadores";
            comboBoxApostadores.Size = new Size(121, 23);
            comboBoxApostadores.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 15);
            label1.TabIndex = 2;
            label1.Text = "Selecione O Apostador";
            // 
            // button1
            // 
            button1.Location = new Point(156, 53);
            button1.Name = "button1";
            button1.Size = new Size(121, 23);
            button1.TabIndex = 3;
            button1.Text = "Gerar Relatório";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GerarRelatório;
            // 
            // labelApostador
            // 
            labelApostador.AutoSize = true;
            labelApostador.Location = new Point(113, 159);
            labelApostador.Name = "labelApostador";
            labelApostador.Size = new Size(38, 15);
            labelApostador.TabIndex = 2;
            labelApostador.Text = "label2";
            // 
            // dataGridViewPules
            // 
            dataGridViewPules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPules.Columns.AddRange(new DataGridViewColumn[] { NPule, Data, Animais, ValorRs, Status });
            dataGridViewPules.Location = new Point(113, 204);
            dataGridViewPules.Name = "dataGridViewPules";
            dataGridViewPules.Size = new Size(544, 176);
            dataGridViewPules.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(113, 398);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "RESUMO:";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.5F));
            tableLayoutPanel2.Controls.Add(labelValorTotalApostado, 1, 1);
            tableLayoutPanel2.Controls.Add(labelTotalDePules, 1, 0);
            tableLayoutPanel2.Controls.Add(label3, 0, 0);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Location = new Point(116, 416);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(249, 100);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // labelValorTotalApostado
            // 
            labelValorTotalApostado.AutoSize = true;
            labelValorTotalApostado.Location = new Point(163, 50);
            labelValorTotalApostado.Name = "labelValorTotalApostado";
            labelValorTotalApostado.Size = new Size(13, 15);
            labelValorTotalApostado.TabIndex = 8;
            labelValorTotalApostado.Text = "0";
            // 
            // labelTotalDePules
            // 
            labelTotalDePules.AutoEllipsis = true;
            labelTotalDePules.AutoSize = true;
            labelTotalDePules.Location = new Point(163, 0);
            labelTotalDePules.Name = "labelTotalDePules";
            labelTotalDePules.Size = new Size(13, 15);
            labelTotalDePules.TabIndex = 9;
            labelTotalDePules.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 6;
            label3.Text = "Total de Pules :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 50);
            label4.Name = "label4";
            label4.Size = new Size(119, 15);
            label4.TabIndex = 7;
            label4.Text = "Valor Total Apostado:";
            // 
            // NPule
            // 
            NPule.HeaderText = "Nº Pule";
            NPule.Name = "NPule";
            NPule.ReadOnly = true;
            // 
            // Data
            // 
            Data.HeaderText = "Data";
            Data.Name = "Data";
            Data.ReadOnly = true;
            // 
            // Animais
            // 
            Animais.HeaderText = "Animais";
            Animais.Name = "Animais";
            Animais.ReadOnly = true;
            // 
            // ValorRs
            // 
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            ValorRs.DefaultCellStyle = dataGridViewCellStyle1;
            ValorRs.HeaderText = "Valor R$";
            ValorRs.Name = "ValorRs";
            ValorRs.ReadOnly = true;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            Status.ReadOnly = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 533);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(label2);
            Controls.Add(dataGridViewPules);
            Controls.Add(labelApostador);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Gestor Pule";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPules).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem animalToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem;
        private ToolStripMenuItem puleToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comboBoxApostadores;
        private Label label1;
        private Button button1;
        private Label labelApostador;
        private DataGridView dataGridViewPules;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labelTotalDePules;
        private Label label3;
        private Label label4;
        private Label labelValorTotalApostado;
        private DataGridViewTextBoxColumn NPule;
        private DataGridViewTextBoxColumn Data;
        private DataGridViewTextBoxColumn Animais;
        private DataGridViewTextBoxColumn ValorRs;
        private DataGridViewTextBoxColumn Status;
    }
}
