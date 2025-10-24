namespace Gestor_De_Pule.src.Views.Relatórios.Apostador
{
    partial class FormRelatórioApostador
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
            comboBoxApostadores = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            labelApostador = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            labelValorTotalApostado = new Label();
            labelTotalDePules = new Label();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            listView1 = new ListView();
            columnHeaderNumPule = new ColumnHeader();
            columnHeaderData = new ColumnHeader();
            columnHeaderAnimais = new ColumnHeader();
            columnHeaderValor = new ColumnHeader();
            columnHeaderStatusPagamento = new ColumnHeader();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 41.01877F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.98123F));
            tableLayoutPanel1.Controls.Add(comboBoxApostadores, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(button1, 1, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(373, 100);
            tableLayoutPanel1.TabIndex = 2;
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
            labelApostador.Location = new Point(3, 0);
            labelApostador.Name = "labelApostador";
            labelApostador.Size = new Size(38, 15);
            labelApostador.TabIndex = 4;
            labelApostador.Text = "label2";
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
            tableLayoutPanel2.Location = new Point(3, 40);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(249, 100);
            tableLayoutPanel2.TabIndex = 7;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 6;
            label2.Text = "RESUMO";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(labelApostador, 0, 0);
            tableLayoutPanel3.Controls.Add(listView1, 0, 1);
            tableLayoutPanel3.Location = new Point(12, 118);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 8.076923F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 91.92308F));
            tableLayoutPanel3.Size = new Size(577, 286);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeaderNumPule, columnHeaderData, columnHeaderAnimais, columnHeaderValor, columnHeaderStatusPagamento });
            listView1.Location = new Point(3, 26);
            listView1.Name = "listView1";
            listView1.Size = new Size(571, 257);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // columnHeaderNumPule
            // 
            columnHeaderNumPule.Text = "NºPule";
            // 
            // columnHeaderData
            // 
            columnHeaderData.Text = "Data";
            // 
            // columnHeaderAnimais
            // 
            columnHeaderAnimais.Text = "Animais";
            // 
            // columnHeaderValor
            // 
            columnHeaderValor.Text = "Valor R$";
            // 
            // columnHeaderStatusPagamento
            // 
            columnHeaderStatusPagamento.Text = "Status Pagamento";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Controls.Add(label2, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel4.Location = new Point(15, 422);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel4.Size = new Size(274, 148);
            tableLayoutPanel4.TabIndex = 9;
            // 
            // FormRelatórioApostador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 582);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel1);
            Name = "FormRelatórioApostador";
            Text = "FormRelatórioApostador";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox comboBoxApostadores;
        private Label label1;
        private Button button1;
        private Label labelApostador;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labelValorTotalApostado;
        private Label labelTotalDePules;
        private Label label3;
        private Label label4;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel3;
        private ListView listView1;
        private ColumnHeader columnHeaderNumPule;
        private ColumnHeader columnHeaderData;
        private ColumnHeader columnHeaderAnimais;
        private ColumnHeader columnHeaderValor;
        private ColumnHeader columnHeaderStatusPagamento;
        private TableLayoutPanel tableLayoutPanel4;
    }
}