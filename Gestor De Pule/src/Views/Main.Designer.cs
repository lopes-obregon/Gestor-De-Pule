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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            menuStrip1 = new MenuStrip();
            arquivoToolStripMenuItem1 = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            arquivoToolStripMenuItem = new ToolStripMenuItem();
            animalToolStripMenuItem = new ToolStripMenuItem();
            disputaToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem = new ToolStripMenuItem();
            puleToolStripMenuItem = new ToolStripMenuItem();
            relatóriosToolStripMenuItem = new ToolStripMenuItem();
            apostadorToolStripMenuItem1 = new ToolStripMenuItem();
            animalToolStripMenuItem1 = new ToolStripMenuItem();
            puleToolStripMenuItem1 = new ToolStripMenuItem();
            financeiroToolStripMenuItem = new ToolStripMenuItem();
            fluxoDeCaixaToolStripMenuItem = new ToolStripMenuItem();
            taxaToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanelFiltrosSelect = new TableLayoutPanel();
            dateTimePickerDisputa = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            comboBoxDisputas = new ComboBox();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelDisputaNome = new Label();
            dataGridViewDisputas = new DataGridView();
            ColumnAnimalNome = new DataGridViewTextBoxColumn();
            ColumnPosição = new DataGridViewTextBoxColumn();
            ColumnTempo = new DataGridViewTextBoxColumn();
            windowCadastroDisputaBindingSource = new BindingSource(components);
            button2 = new Button();
            button3 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button4 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            labelVitória = new Label();
            labelTotalGanhadores = new Label();
            labelPagamentoPorPule = new Label();
            disputaToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            tableLayoutPanelFiltrosSelect.SuspendLayout();
            flowLayoutPanelButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDisputas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)windowCadastroDisputaBindingSource).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { arquivoToolStripMenuItem1, arquivoToolStripMenuItem, relatóriosToolStripMenuItem, financeiroToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(933, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem1
            // 
            arquivoToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { sairToolStripMenuItem });
            arquivoToolStripMenuItem1.Name = "arquivoToolStripMenuItem1";
            arquivoToolStripMenuItem1.Size = new Size(61, 20);
            arquivoToolStripMenuItem1.Text = "Arquivo";
            // 
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(93, 22);
            sairToolStripMenuItem.Text = "Sair";
            sairToolStripMenuItem.Click += CloseSystem;
            // 
            // arquivoToolStripMenuItem
            // 
            arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { animalToolStripMenuItem, disputaToolStripMenuItem, apostadorToolStripMenuItem, puleToolStripMenuItem });
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
            // disputaToolStripMenuItem
            // 
            disputaToolStripMenuItem.Name = "disputaToolStripMenuItem";
            disputaToolStripMenuItem.Size = new Size(129, 22);
            disputaToolStripMenuItem.Text = "Disputa";
            disputaToolStripMenuItem.Click += DisputaCadastradosWindow;
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
            relatóriosToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { apostadorToolStripMenuItem1, animalToolStripMenuItem1, puleToolStripMenuItem1, disputaToolStripMenuItem1 });
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
            // animalToolStripMenuItem1
            // 
            animalToolStripMenuItem1.Name = "animalToolStripMenuItem1";
            animalToolStripMenuItem1.Size = new Size(180, 22);
            animalToolStripMenuItem1.Text = "Animal";
            animalToolStripMenuItem1.Click += AnimalForm;
            // 
            // puleToolStripMenuItem1
            // 
            puleToolStripMenuItem1.Name = "puleToolStripMenuItem1";
            puleToolStripMenuItem1.Size = new Size(180, 22);
            puleToolStripMenuItem1.Text = "Pule";
            puleToolStripMenuItem1.Click += WindowRelatórioPule;
            // 
            // financeiroToolStripMenuItem
            // 
            financeiroToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fluxoDeCaixaToolStripMenuItem, taxaToolStripMenuItem });
            financeiroToolStripMenuItem.Name = "financeiroToolStripMenuItem";
            financeiroToolStripMenuItem.Size = new Size(74, 20);
            financeiroToolStripMenuItem.Text = "Financeiro";
            // 
            // fluxoDeCaixaToolStripMenuItem
            // 
            fluxoDeCaixaToolStripMenuItem.Name = "fluxoDeCaixaToolStripMenuItem";
            fluxoDeCaixaToolStripMenuItem.Size = new Size(149, 22);
            fluxoDeCaixaToolStripMenuItem.Text = "Fluxo de Caixa";
            fluxoDeCaixaToolStripMenuItem.Click += FluxoCaixaView;
            // 
            // taxaToolStripMenuItem
            // 
            taxaToolStripMenuItem.Name = "taxaToolStripMenuItem";
            taxaToolStripMenuItem.Size = new Size(149, 22);
            taxaToolStripMenuItem.Text = "Taxa";
            taxaToolStripMenuItem.Click += TaxaView;
            // 
            // tableLayoutPanelFiltrosSelect
            // 
            tableLayoutPanelFiltrosSelect.ColumnCount = 2;
            tableLayoutPanelFiltrosSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelFiltrosSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelFiltrosSelect.Controls.Add(dateTimePickerDisputa, 1, 1);
            tableLayoutPanelFiltrosSelect.Controls.Add(label2, 0, 1);
            tableLayoutPanelFiltrosSelect.Controls.Add(label1, 0, 0);
            tableLayoutPanelFiltrosSelect.Controls.Add(comboBoxDisputas, 1, 0);
            tableLayoutPanelFiltrosSelect.Location = new Point(12, 27);
            tableLayoutPanelFiltrosSelect.Name = "tableLayoutPanelFiltrosSelect";
            tableLayoutPanelFiltrosSelect.RowCount = 2;
            tableLayoutPanelFiltrosSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelFiltrosSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelFiltrosSelect.Size = new Size(443, 100);
            tableLayoutPanelFiltrosSelect.TabIndex = 1;
            // 
            // dateTimePickerDisputa
            // 
            dateTimePickerDisputa.Location = new Point(224, 53);
            dateTimePickerDisputa.Name = "dateTimePickerDisputa";
            dateTimePickerDisputa.Size = new Size(200, 23);
            dateTimePickerDisputa.TabIndex = 2;
            dateTimePickerDisputa.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(91, 15);
            label2.TabIndex = 2;
            label2.Text = "Data Da Disputa";
            label2.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 2;
            label1.Text = "Nome Da Disputa";
            // 
            // comboBoxDisputas
            // 
            comboBoxDisputas.FormattingEnabled = true;
            comboBoxDisputas.Location = new Point(224, 3);
            comboBoxDisputas.Name = "comboBoxDisputas";
            comboBoxDisputas.Size = new Size(121, 23);
            comboBoxDisputas.TabIndex = 2;
            // 
            // flowLayoutPanelButtons
            // 
            flowLayoutPanelButtons.Controls.Add(button1);
            flowLayoutPanelButtons.Location = new Point(461, 30);
            flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            flowLayoutPanelButtons.Size = new Size(194, 100);
            flowLayoutPanelButtons.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += BuscarDisputa;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(labelDisputaNome, 0, 0);
            tableLayoutPanel1.Controls.Add(dataGridViewDisputas, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 145);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.1639347F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.83607F));
            tableLayoutPanel1.Size = new Size(646, 305);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // labelDisputaNome
            // 
            labelDisputaNome.AutoSize = true;
            labelDisputaNome.Location = new Point(3, 0);
            labelDisputaNome.Name = "labelDisputaNome";
            labelDisputaNome.Size = new Size(105, 15);
            labelDisputaNome.TabIndex = 0;
            labelDisputaNome.Text = "labelDisputaNome";
            // 
            // dataGridViewDisputas
            // 
            dataGridViewDisputas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDisputas.Columns.AddRange(new DataGridViewColumn[] { ColumnAnimalNome, ColumnPosição, ColumnTempo });
            dataGridViewDisputas.Location = new Point(3, 34);
            dataGridViewDisputas.Name = "dataGridViewDisputas";
            dataGridViewDisputas.Size = new Size(640, 268);
            dataGridViewDisputas.TabIndex = 1;
            // 
            // ColumnAnimalNome
            // 
            ColumnAnimalNome.HeaderText = "Nome Do Animal";
            ColumnAnimalNome.Name = "ColumnAnimalNome";
            ColumnAnimalNome.ReadOnly = true;
            // 
            // ColumnPosição
            // 
            ColumnPosição.HeaderText = "Posição";
            ColumnPosição.Name = "ColumnPosição";
            ColumnPosição.ReadOnly = true;
            // 
            // ColumnTempo
            // 
            dataGridViewCellStyle2.Format = "hh\\:mm\\:ss\\,ff";
            dataGridViewCellStyle2.NullValue = null;
            ColumnTempo.DefaultCellStyle = dataGridViewCellStyle2;
            ColumnTempo.HeaderText = "Tempo";
            ColumnTempo.Name = "ColumnTempo";
            // 
            // windowCadastroDisputaBindingSource
            // 
            windowCadastroDisputaBindingSource.DataSource = typeof(src.Views.Cadastros.WindowCadastroDisputa);
            // 
            // button2
            // 
            button2.Location = new Point(3, 3);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Salvar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += SalvarDados;
            // 
            // button3
            // 
            button3.Location = new Point(84, 3);
            button3.Name = "button3";
            button3.Size = new Size(126, 23);
            button3.TabIndex = 5;
            button3.Text = "Calcular Posição";
            button3.UseVisualStyleBackColor = true;
            button3.Click += CalcularPosição;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button3);
            flowLayoutPanel1.Controls.Add(button4);
            flowLayoutPanel1.Location = new Point(192, 481);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(244, 100);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // button4
            // 
            button4.Location = new Point(3, 32);
            button4.Name = "button4";
            button4.Size = new Size(118, 23);
            button4.TabIndex = 6;
            button4.Text = "Calcular Prêmio";
            button4.UseVisualStyleBackColor = true;
            button4.Click += CalcularPrêmio;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.ActiveBorder;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            tableLayoutPanel2.Controls.Add(labelVitória, 0, 0);
            tableLayoutPanel2.Controls.Add(labelTotalGanhadores, 0, 1);
            tableLayoutPanel2.Controls.Add(labelPagamentoPorPule, 0, 2);
            tableLayoutPanel2.Location = new Point(721, 145);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 129F));
            tableLayoutPanel2.Size = new Size(200, 208);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // labelVitória
            // 
            labelVitória.AutoSize = true;
            labelVitória.Location = new Point(3, 0);
            labelVitória.Name = "labelVitória";
            labelVitória.Size = new Size(52, 15);
            labelVitória.TabIndex = 0;
            labelVitória.Text = "Vitória:[]";
            // 
            // labelTotalGanhadores
            // 
            labelTotalGanhadores.AutoSize = true;
            labelTotalGanhadores.Location = new Point(3, 39);
            labelTotalGanhadores.Name = "labelTotalGanhadores";
            labelTotalGanhadores.Size = new Size(99, 15);
            labelTotalGanhadores.TabIndex = 1;
            labelTotalGanhadores.Text = "Total Ganhadores";
            // 
            // labelPagamentoPorPule
            // 
            labelPagamentoPorPule.AutoSize = true;
            labelPagamentoPorPule.Location = new Point(3, 78);
            labelPagamentoPorPule.Name = "labelPagamentoPorPule";
            labelPagamentoPorPule.Size = new Size(115, 15);
            labelPagamentoPorPule.TabIndex = 2;
            labelPagamentoPorPule.Text = "Pagamento Por Pule";
            // 
            // disputaToolStripMenuItem1
            // 
            disputaToolStripMenuItem1.Name = "disputaToolStripMenuItem1";
            disputaToolStripMenuItem1.Size = new Size(180, 22);
            disputaToolStripMenuItem1.Text = "Disputa";
            disputaToolStripMenuItem1.Click += RelatórioDisputa;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 593);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(flowLayoutPanelButtons);
            Controls.Add(tableLayoutPanelFiltrosSelect);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Gestor Pule";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanelFiltrosSelect.ResumeLayout(false);
            tableLayoutPanelFiltrosSelect.PerformLayout();
            flowLayoutPanelButtons.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDisputas).EndInit();
            ((System.ComponentModel.ISupportInitialize)windowCadastroDisputaBindingSource).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
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
        private ColumnHeader columnHeaderNúmero;
        private ToolStripMenuItem relatóriosToolStripMenuItem;
        private ToolStripMenuItem apostadorToolStripMenuItem1;
        private ToolStripMenuItem animalToolStripMenuItem1;
        private ToolStripMenuItem arquivoToolStripMenuItem1;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem puleToolStripMenuItem1;
        private ToolStripMenuItem disputaToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanelFiltrosSelect;
        private DateTimePicker dateTimePickerDisputa;
        private Label label2;
        private Label label1;
        private ComboBox comboBoxDisputas;
        private FlowLayoutPanel flowLayoutPanelButtons;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelDisputaNome;
        private DataGridView dataGridViewDisputas;
        private BindingSource windowCadastroDisputaBindingSource;
        private Button button2;
        private DataGridViewTextBoxColumn ColumnAnimalNome;
        private DataGridViewTextBoxColumn ColumnPosição;
        private DataGridViewTextBoxColumn ColumnTempo;
        private Button button3;
        private ToolStripMenuItem financeiroToolStripMenuItem;
        private ToolStripMenuItem fluxoDeCaixaToolStripMenuItem;
        private ToolStripMenuItem taxaToolStripMenuItem;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labelVitória;
        private Label labelTotalGanhadores;
        private Label labelPagamentoPorPule;
        private Button button4;
        private ToolStripMenuItem disputaToolStripMenuItem1;
    }
}
